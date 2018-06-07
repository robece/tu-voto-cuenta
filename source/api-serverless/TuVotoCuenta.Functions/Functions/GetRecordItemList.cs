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
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Helpers;

namespace TuVotoCuenta.Functions
{
    public static class GetRecordItemList
    {
        [FunctionName("GetRecordItemList")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, TraceWriter log, ExecutionContext context)
        {
            System.Diagnostics.Trace.TraceInformation("Initialize HttpTrigger - GetRecordItemList");
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

                MongoDBConnectionInfo dbConnectionInfo = new MongoDBConnectionInfo()
                {
                    ConnectionString = Settings.COSMOSDB_CONNECTIONSTRING,
                    DatabaseId = Settings.COSMOSDB_DATABASEID,
                    RecordItemCollection = Settings.COSMOSDB_RECORDITEMCOLLECTION
                };

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
                        GetRecordItemListHelper helper = new GetRecordItemListHelper(Settings.STORAGE_ACCOUNT, Settings.RPC_CLIENT, dbConnectionInfo);
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
                    var exception = string.Empty;
                    exception = (innerException.InnerException != null) ? innerException.InnerException.Message : innerException.Message;
                    var stackTrace = string.Empty;
                    stackTrace = (innerException.InnerException != null) ? innerException.InnerException.StackTrace : innerException.StackTrace;
                    System.Diagnostics.Trace.TraceError($"Exception: {exception}, StackTrace: {stackTrace}");
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

            //get message for result id
            string message = EnumDescription.GetEnumDescription((GetRecordItemListResultEnum)result.ResultId);

            //build json result object
            string res = JsonConvert.SerializeObject(result);
            dynamic resultParsed = JObject.Parse(res);
            dynamic jsonresult = new JObject();
            jsonresult.message = message;
            jsonresult.records = resultParsed.Records;

            //send ok result or bad request
            return (result.IsSucceded) ? (ActionResult)new OkObjectResult(jsonresult) : (ActionResult)new BadRequestObjectResult(jsonresult);
        }
    }
}