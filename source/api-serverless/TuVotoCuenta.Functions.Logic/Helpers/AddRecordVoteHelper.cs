using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class AddRecordVoteHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;
        private string USERNAME = @"^[A-Za-z\d]{6,20}$";

        public AddRecordVoteHelper(string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<AddRecordVoteResponse> AddVoteAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            AddRecordVoteResponse result = new AddRecordVoteResponse();
            result.IsSucceded = true;
            result.ResultId = (int)AddRecordVoteResultEnum.Success;
            try
            {
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

                //validate username length
                Regex regex = new Regex(USERNAME);
                Match match = regex.Match(username);
                bool isValidUsernameLength = match.Success;

                if (!isValidUsernameLength)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordVoteResultEnum.InvalidUsernameLength;
                    return result;
                }

                //connecting to mongodb
                string connectionString = DBCONNECTION_INFO.ConnectionString;
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var database = mongoClient.GetDatabase(DBCONNECTION_INFO.DatabaseId);
                var recordItemCollection = database.GetCollection<RecordItem>(DBCONNECTION_INFO.RecordItemCollection);
                var recordVoteCollection = database.GetCollection<RecordVote>(DBCONNECTION_INFO.RecordVoteCollection);
                var userAccountCollection = database.GetCollection<UserAccount>(DBCONNECTION_INFO.UserAccountCollection);

                //validate if account exists
                UserAccount userAccount = null;
                try
                {
                    userAccount = userAccountCollection.AsQueryable<UserAccount>().Where<UserAccount>(sb => sb.username == username).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.TraceWarning($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                }

                if (userAccount == null)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordVoteResultEnum.UsernameNotExists;
                    return result;
                }

                //validate if record item exists for voting
                RecordItem recordItem = null;
                try
                {
                    recordItem = recordItemCollection.AsQueryable<RecordItem>().Where<RecordItem>(sb => sb.hash == hash).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.TraceWarning($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                }

                if (recordItem == null)
                {
                    //there is no record item linked with this vote
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordVoteResultEnum.NotExists;
                    return result;
                }
                else
                {
                    //validate if user has voted submit a vote before
                    RecordVote recordVote = null;
                    try
                    {
                        recordVote = recordVoteCollection.AsQueryable<RecordVote>().Where<RecordVote>(sb => sb.hash == hash && sb.username == username).SingleOrDefault();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.TraceWarning($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                    }

                    if (recordVote != null)
                    {
                        //the user already voted
                        result.IsSucceded = false;
                        result.ResultId = (int)AddRecordVoteResultEnum.AlreadyVoted;
                        return result;
                    }
                    else
                    {
                        //save record in blockchain
                        BlockchainHelper bh = new BlockchainHelper(STORAGE_ACCOUNT, RPC_CLIENT, masterAddress, masterPrivateKey);

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

                        TimeZoneInfo setTimeZoneInfo;
                        DateTime currentDateTime;

                        //Set the time zone information to US Mountain Standard Time 
                        setTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");

                        //Get date and time in US Mountain Standard Time
                        currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, setTimeZoneInfo);

                        //save record in mongodb
                        RecordVote vote = new RecordVote
                        {
                            username = username,
                            hash = hash,
                            isApproval = isApproval,
                            transactionId = res_IncreaseOperationAsync,
                            createdDate = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                        };

                        //perform insert in mongodb
                        await recordVoteCollection.InsertOneAsync(vote);
                    }
                }
            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.Flatten().InnerExceptions)
                {
                    var exception = string.Empty;
                    exception = (innerException.InnerException != null) ? innerException.InnerException.Message : innerException.Message;
                    var stackTrace = string.Empty;
                    stackTrace = (innerException.InnerException != null) ? innerException.InnerException.StackTrace : innerException.StackTrace;
                    System.Diagnostics.Trace.TraceError($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                }
                result.IsSucceded = false;
                result.ResultId = (int)AddRecordVoteResultEnum.Failed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                result.IsSucceded = false;
                result.ResultId = (int)AddRecordVoteResultEnum.Failed;
            }
            return result;
        }
    }
}