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

        public GetRecordVoteListHelper(string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<GetRecordVoteListResponse> GetVotesAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            GetRecordVoteListResponse result = new GetRecordVoteListResponse
            {
                IsSucceded = true,
                ResultId = (int)GetRecordVoteListResultEnum.Success
            };

            try
            {
                parameters.TryGetValue(ParameterTypeEnum.Hash, out global::System.Object ohash);
                string hash = ohash.ToString().ToLower();

                //database helpers
                DBRecordVoteHelper dbRecordVoteHelper = new DBRecordVoteHelper(DBCONNECTION_INFO);

                //get votes
                result.Votes = await dbRecordVoteHelper.GetRecordVoteList(hash);
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
                result.ResultId = (int)GetRecordVoteListResultEnum.Failed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                result.IsSucceded = false;
                result.ResultId = (int)GetRecordVoteListResultEnum.Failed;
            }
            return result;
        }
    }
}