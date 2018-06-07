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
    public class GetRecordItemListHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;

        public GetRecordItemListHelper(string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<GetRecordItemListResponse> GetRecordsAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            GetRecordItemListResponse result = new GetRecordItemListResponse();
            result.IsSucceded = true;
            result.ResultId = (int)GetRecordItemListResultEnum.Success;
            try
            {
                parameters.TryGetValue(ParameterTypeEnum.Entity, out global::System.Object oentity);
                string entity = oentity.ToString();

                parameters.TryGetValue(ParameterTypeEnum.Municipality, out global::System.Object omunicipality);
                string municipality = omunicipality.ToString();

                parameters.TryGetValue(ParameterTypeEnum.Locality, out global::System.Object olocality);
                string locality = olocality.ToString();

                //connecting to mongodb
                string connectionString = DBCONNECTION_INFO.ConnectionString;
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var database = mongoClient.GetDatabase(DBCONNECTION_INFO.DatabaseId);
                var recordItemCollection = database.GetCollection<RecordItem>(DBCONNECTION_INFO.RecordItemCollection);

                //get votes
                var filter = new FilterDefinitionBuilder<RecordItem>().Eq<string>(record => record.entity, entity);
                filter = filter & new FilterDefinitionBuilder<RecordItem>().Eq<string>(record => record.municipality, municipality);
                filter = filter & new FilterDefinitionBuilder<RecordItem>().Eq<string>(record => record.locality, locality);
                result.Records = await recordItemCollection.FindSync<RecordItem>(filter).ToListAsync();
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
                result.ResultId = (int)GetRecordItemListResultEnum.Failed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                result.IsSucceded = false;
                result.ResultId = (int)GetRecordItemListResultEnum.Failed;
            }
            return result;
        }
    }
}