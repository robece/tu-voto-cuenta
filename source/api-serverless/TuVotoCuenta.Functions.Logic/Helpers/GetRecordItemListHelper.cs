using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Database;

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
            GetRecordItemListResponse result = new GetRecordItemListResponse
            {
                IsSucceded = true,
                ResultId = (int)GetRecordItemListResultEnum.Success
            };

            try
            {
                parameters.TryGetValue(ParameterTypeEnum.Entity, out global::System.Object oentity);
                string entity = oentity.ToString();

                parameters.TryGetValue(ParameterTypeEnum.Municipality, out global::System.Object omunicipality);
                string municipality = omunicipality.ToString();

                parameters.TryGetValue(ParameterTypeEnum.Locality, out global::System.Object olocality);
                string locality = olocality.ToString();

                //database helpers
                DBRecordItemHelper dbRecordItemHelper = new DBRecordItemHelper(DBCONNECTION_INFO);

                //get votes
                result.Records = await dbRecordItemHelper.GetRecordItemListAsync(entity, municipality, locality);
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