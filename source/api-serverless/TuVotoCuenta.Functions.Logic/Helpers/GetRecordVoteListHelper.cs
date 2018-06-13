using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Database;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class GetRecordVoteListHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;
        private TelemetryClient telemetryClient = null;

        public GetRecordVoteListHelper(TelemetryClient telemetryClient, string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.telemetryClient = telemetryClient;
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<GetRecordVoteListResponse> GetVotesAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            telemetryClient.TrackTrace("Starting helper");

            GetRecordVoteListResponse result = new GetRecordVoteListResponse
            {
                IsSucceded = true,
                ResultId = (int)GetRecordVoteListResultEnum.Success
            };

            try
            {
                telemetryClient.TrackTrace("Getting parameters");

                parameters.TryGetValue(ParameterTypeEnum.Hash, out global::System.Object ohash);
                string hash = ohash.ToString().ToLower();

                //database helpers
                DBRecordVoteHelper dbRecordVoteHelper = new DBRecordVoteHelper(DBCONNECTION_INFO);

                telemetryClient.TrackTrace("Getting record votes");

                //get votes
                result.Votes = await dbRecordVoteHelper.GetRecordVoteList(hash);
            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.Flatten().InnerExceptions)
                {
                    telemetryClient.TrackException(innerException);
                }
                result.IsSucceded = false;
                result.ResultId = (int)GetRecordVoteListResultEnum.Failed;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                result.IsSucceded = false;
                result.ResultId = (int)GetRecordVoteListResultEnum.Failed;
            }

            telemetryClient.TrackTrace("Finishing helper");
            return result;
        }
    }
}