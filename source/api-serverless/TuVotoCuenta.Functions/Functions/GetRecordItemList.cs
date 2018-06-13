using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Classes;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Helpers;

namespace TuVotoCuenta.Functions
{
    public static class GetRecordItemList
    {
        [FunctionName("GetRecordItemList")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, TraceWriter log, ExecutionContext context)
        {
            TelemetryClient telemetryClient = new TelemetryClient
            {
                InstrumentationKey = Settings.APPINSIGHTS_INSTRUMENTATIONKEY
            };
            telemetryClient.TrackTrace("Starting function: GetRecordItemList");

            GetRecordItemListResponse result = new GetRecordItemListResponse
            {
                IsSucceded = true,
                ResultId = (int)GetRecordItemListResultEnum.Success
            };

            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string token = data?.token;
                string entity = data?.entity;
                string municipality = data?.municipality;
                string locality = data?.locality;

                Dictionary<ParameterTypeEnum, object> parameters = new Dictionary<ParameterTypeEnum, object>
                {
                    { ParameterTypeEnum.Entity, entity },
                    { ParameterTypeEnum.Municipality, municipality },
                    { ParameterTypeEnum.Locality, locality }
                };

                telemetryClient.TrackTrace("Validating token");

                //validate token
                if (!string.IsNullOrEmpty(token))
                {
                    var decrypted_token = SecurityHelper.Decrypt(token, Settings.SECURITY_SEED);
                    byte[] token_bytes = Convert.FromBase64String(decrypted_token);
                    DateTime when = DateTime.FromBinary(BitConverter.ToInt64(token_bytes, 0));

                    if (when < DateTime.UtcNow.AddMinutes(-5))
                    {
                        result.IsSucceded = false;
                        result.ResultId = (int)GetRecordItemListResultEnum.InvalidToken;
                    }
                    else
                    {
                        telemetryClient.TrackTrace("Calling helper");
                        GetRecordItemListHelper helper = new GetRecordItemListHelper(telemetryClient, Settings.STORAGE_ACCOUNT, Settings.RPC_CLIENT, Configurations.GetMongoDbConnectionInfo());
                        result = await helper.GetRecordsAsync(parameters);
                    }
                }
                else
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)GetRecordItemListResultEnum.MissingToken;
                }
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

            //get message for result id
            string message = EnumDescription.GetEnumDescription((GetRecordItemListResultEnum)result.ResultId);

            //build json result object
            string res = JsonConvert.SerializeObject(result);
            dynamic resultParsed = JObject.Parse(res);
            dynamic jsonresult = new JObject();
            jsonresult.message = message;
            jsonresult.records = resultParsed.Records;

            telemetryClient.TrackTrace("Finishing function: GetRecordItemList");

            //send ok result or bad request
            return (result.IsSucceded) ? (ActionResult)new OkObjectResult(jsonresult) : (ActionResult)new BadRequestObjectResult(jsonresult);
        }
    }
}