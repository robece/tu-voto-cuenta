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
    public class GetRecordItemListHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;
        private TelemetryClient telemetryClient = null;

        public GetRecordItemListHelper(TelemetryClient telemetryClient, string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.telemetryClient = telemetryClient;
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<GetRecordItemListResponse> GetRecordsAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            telemetryClient.TrackTrace("Starting helper");

            GetRecordItemListResponse result = new GetRecordItemListResponse
            {
                IsSucceded = true,
                ResultId = (int)GetRecordItemListResultEnum.Success
            };

            try
            {
                telemetryClient.TrackTrace("Getting parameters");

                parameters.TryGetValue(ParameterTypeEnum.Entity, out global::System.Object oentity);
                string entity = oentity.ToString();

                parameters.TryGetValue(ParameterTypeEnum.Municipality, out global::System.Object omunicipality);
                string municipality = omunicipality.ToString();

                parameters.TryGetValue(ParameterTypeEnum.Locality, out global::System.Object olocality);
                string locality = olocality.ToString();

                //database helpers
                DBRecordItemHelper dbRecordItemHelper = new DBRecordItemHelper(DBCONNECTION_INFO);

                telemetryClient.TrackTrace("Getting record items");

                //get votes
                result.Records = await dbRecordItemHelper.GetRecordItemListAsync(entity, municipality, locality);
            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.Flatten().InnerExceptions)
                {
                    telemetryClient.TrackException(innerException);
                }
                result.IsSucceded = false;
                result.ResultId = (int)GetRecordItemListResultEnum.Failed;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                result.IsSucceded = false;
                result.ResultId = (int)GetRecordItemListResultEnum.Failed;
            }

            telemetryClient.TrackTrace("Finishing helper");
            return result;
        }
    }
}