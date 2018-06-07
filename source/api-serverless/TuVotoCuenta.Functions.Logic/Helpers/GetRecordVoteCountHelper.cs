using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class GetRecordVoteCountHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;

        public GetRecordVoteCountHelper(string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<GetRecordVoteCountResponse> GetCountersAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            GetRecordVoteCountResponse result = new GetRecordVoteCountResponse();
            result.IsSucceded = true;
            result.ResultId = (int)GetRecordVoteCountResultEnum.Success;
            try
            {
                parameters.TryGetValue(ParameterTypeEnum.Hash, out global::System.Object ohash);
                string hash = ohash.ToString().ToLower();

                //connecting to mongodb
                string connectionString = DBCONNECTION_INFO.ConnectionString;
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var database = mongoClient.GetDatabase(DBCONNECTION_INFO.DatabaseId);
                var recordVoteCollection = database.GetCollection<RecordVote>(DBCONNECTION_INFO.RecordVoteCollection);

                //get votes
                var filter = new FilterDefinitionBuilder<RecordVote>().Eq<string>(vote => vote.hash, hash);
                var votes = await recordVoteCollection.FindSync<RecordVote>(filter).ToListAsync();

                result.Approvals = votes.FindAll(x => x.isApproval == true).Count;
                result.Disapprovals = votes.FindAll(x => x.isApproval == false).Count;
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
                result.ResultId = (int)GetRecordVoteCountResultEnum.Failed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                result.IsSucceded = false;
                result.ResultId = (int)GetRecordVoteCountResultEnum.Failed;
            }
            return result;
        }
    }
}