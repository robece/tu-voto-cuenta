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
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Helpers;

namespace TuVotoCuenta.Functions
{
    public static class AddRecordItem
    {
        [FunctionName("AddRecordItem")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, TraceWriter log, ExecutionContext context)
        {
            System.Diagnostics.Trace.TraceInformation("Initialize HttpTrigger - AddRecordItem");
            AddRecordItemResponse result = new AddRecordItemResponse
            {
                IsSucceded = true,
                ResultId = (int)AddRecordItemResultEnum.Success
            };

            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string token = data?.token;
                string tempRecordItem = JsonConvert.SerializeObject(data?.recordItem);
                Domain.Models.Request.RecordItem recordItem = JsonConvert.DeserializeObject<Domain.Models.Request.RecordItem>(tempRecordItem);

                Dictionary<ParameterTypeEnum, object> parameters = new Dictionary<ParameterTypeEnum, object>
                {
                    { ParameterTypeEnum.RecordItem, recordItem },
                    { ParameterTypeEnum.MasterAddress, Settings.MASTER_ADDRESS },
                    { ParameterTypeEnum.MasterPrivateKey, Settings.MASTER_PRIVATEKEY },
                    { ParameterTypeEnum.ContractAddress, Settings.CONTRACT_ADDRESS },
                    { ParameterTypeEnum.ContractABI, Settings.CONTRACT_ABI },
                    { ParameterTypeEnum.RecordItemImageContainer, Settings.CONTAINER_NAME_RECORDITEMIMAGES },
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
                        result.ResultId = (int)AddRecordItemResultEnum.InvalidToken;
                    }
                    else
                    {
                        AddRecordItemHelper recordItemHelper = new AddRecordItemHelper(Settings.STORAGE_ACCOUNT, Settings.RPC_CLIENT, Configurations.GetMongoDbConnectionInfo());
                        result = await recordItemHelper.AddRecordItemAsync(parameters);
                    }
                }
                else
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordItemResultEnum.MissingToken;
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
                    System.Diagnostics.Trace.TraceError($"EXCEPTION: {exception}. STACKTRACE: {stackTrace}");
                }
                result.IsSucceded = false;
                result.ResultId = (int)AddRecordItemResultEnum.Failed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                result.IsSucceded = false;
                result.ResultId = (int)AddRecordItemResultEnum.Failed;
            }

            //get message for result id
            string message = EnumDescription.GetEnumDescription((AddRecordItemResultEnum)result.ResultId);

            //build json result object
            dynamic jsonresult = new JObject();
            jsonresult.message = message;

            //send ok result or bad request
            return (result.IsSucceded) ? (ActionResult)new OkObjectResult(jsonresult) : (ActionResult)new BadRequestObjectResult(jsonresult);
        }
    }
}