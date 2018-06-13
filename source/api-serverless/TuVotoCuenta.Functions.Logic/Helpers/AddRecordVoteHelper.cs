using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Classes;
using TuVotoCuenta.Functions.Logic.Database;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class AddRecordVoteHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;
        private TelemetryClient telemetryClient = null;

        public AddRecordVoteHelper(TelemetryClient telemetryClient, string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.telemetryClient = telemetryClient;
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<AddRecordVoteResponse> AddRecordVoteAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            telemetryClient.TrackTrace("Starting helper");

            AddRecordVoteResponse result = new AddRecordVoteResponse
            {
                IsSucceded = true,
                ResultId = (int)AddRecordVoteResultEnum.Success
            };

            try
            {
                telemetryClient.TrackTrace("Getting parameters");

                parameters.TryGetValue(ParameterTypeEnum.Username, out global::System.Object ousername);
                string username = ousername.ToString().ToLower();

                parameters.TryGetValue(ParameterTypeEnum.Hash, out global::System.Object ohash);
                string hash = ohash.ToString().ToLower();

                parameters.TryGetValue(ParameterTypeEnum.IsApproval, out global::System.Object oisApproval);
                bool isApproval = Convert.ToBoolean(oisApproval.ToString());

                parameters.TryGetValue(ParameterTypeEnum.MasterAddress, out global::System.Object omasterAddress);
                string masterAddress = omasterAddress.ToString();

                parameters.TryGetValue(ParameterTypeEnum.MasterPrivateKey, out global::System.Object omasterPrivateKey);
                string masterPrivateKey = omasterPrivateKey.ToString();

                parameters.TryGetValue(ParameterTypeEnum.ContractAddress, out global::System.Object ocontractAddress);
                string contractAddress = ocontractAddress.ToString();

                parameters.TryGetValue(ParameterTypeEnum.ContractABI, out global::System.Object ocontractABI);
                string contractABI = ocontractABI.ToString();

                //database helpers
                DBUserAccountHelper dbUserAccountHelper = new DBUserAccountHelper(DBCONNECTION_INFO);
                DBRecordItemHelper dbRecordItemHelper = new DBRecordItemHelper(DBCONNECTION_INFO);
                DBRecordVoteHelper dbRecordVoteHelper = new DBRecordVoteHelper(DBCONNECTION_INFO);

                //blockchain helper
                BlockchainHelper bh = new BlockchainHelper(telemetryClient, STORAGE_ACCOUNT, RPC_CLIENT, masterAddress, masterPrivateKey);

                telemetryClient.TrackTrace("Validating username length");

                //validate username length
                if (!RegexValidation.IsValidUsername(username))
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordVoteResultEnum.InvalidUsernameLength;
                    return result;
                }

                telemetryClient.TrackTrace("Validating username existance");

                //validate if account exists
                UserAccount userAccount = dbUserAccountHelper.GetUser(username);

                if (userAccount == null)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordVoteResultEnum.UsernameNotExists;
                    return result;
                }

                telemetryClient.TrackTrace("Validating record item existance");

                //validate if record item exists for voting
                RecordItem recordItem = dbRecordItemHelper.GetRecordItem(hash);

                if (recordItem == null)
                {
                    //there is no record item linked with this vote
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordVoteResultEnum.NotExists;
                    return result;
                }
                else
                {
                    telemetryClient.TrackTrace("Validating record vote existance");

                    //validate if user has voted submit a vote before
                    RecordVote vote = dbRecordVoteHelper.GetRecordVote(hash, username);

                    if (vote != null)
                    {
                        //the user already voted
                        result.IsSucceded = false;
                        result.ResultId = (int)AddRecordVoteResultEnum.AlreadyVoted;
                        return result;
                    }
                    else
                    {
                        telemetryClient.TrackTrace("Adding record vote to blockchain");

                        //sending to the blockchain
                        var res_IncreaseOperationAsync = string.Empty;
                        if ((bool)isApproval)
                        {
                            res_IncreaseOperationAsync = await bh.IncreaseApprovalsAsync(hash, contractAddress, contractABI);
                            System.Diagnostics.Trace.TraceInformation($"Add record for approval vote result: {res_IncreaseOperationAsync}");
                        }
                        else
                        {
                            res_IncreaseOperationAsync = await bh.IncreaseDisapprovalsAsync(hash, contractAddress, contractABI);
                            System.Diagnostics.Trace.TraceInformation($"Add record for disapproval vote result: {res_IncreaseOperationAsync }");
                        }

                        if (string.IsNullOrEmpty(res_IncreaseOperationAsync))
                        {
                            //there was an error voting the record in the blockchain
                            result.IsSucceded = false;
                            result.ResultId = (int)AddRecordVoteResultEnum.BlockchainIssue;
                            return result;
                        }

                        telemetryClient.TrackTrace("Adding record vote to database");

                        //save record in mongodb
                        vote = new RecordVote
                        {
                            username = username,
                            hash = hash,
                            isApproval = isApproval,
                            transactionId = res_IncreaseOperationAsync,
                            createdDate = Timezone.GetCustomTimeZone()
                        };

                        //perform insert in mongodb
                        await dbRecordVoteHelper.CreateRecordVote(vote);
                    }
                }
            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.Flatten().InnerExceptions)
                {
                    telemetryClient.TrackException(innerException);
                }
                result.IsSucceded = false;
                result.ResultId = (int)AddRecordVoteResultEnum.Failed;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                result.IsSucceded = false;
                result.ResultId = (int)AddRecordVoteResultEnum.Failed;
            }

            telemetryClient.TrackTrace("Finishing helper");
            return result;
        }
    }
}