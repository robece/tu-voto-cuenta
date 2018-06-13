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
    public static class GetRecordVoteList
    {
        [FunctionName("GetRecordVoteList")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, TraceWriter log, ExecutionContext context)
        {
            TelemetryClient telemetryClient = new TelemetryClient
            {
                InstrumentationKey = Settings.APPINSIGHTS_INSTRUMENTATIONKEY
            };
            telemetryClient.TrackTrace("Starting function: GetRecordVoteList");

            GetRecordVoteListResponse result = new GetRecordVoteListResponse
            {
                IsSucceded = true,
                ResultId = (int)GetRecordVoteListResultEnum.Success
            };

            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string token = data?.token;
                string hash = data?.hash;

                Dictionary<ParameterTypeEnum, object> parameters = new Dictionary<ParameterTypeEnum, object>
                {
                    { ParameterTypeEnum.Hash, hash }
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
                        result.ResultId = (int)GetRecordVoteListResultEnum.InvalidToken;
                    }
                    else
                    {
                        telemetryClient.TrackTrace("Calling helper");
                        GetRecordVoteListHelper helper = new GetRecordVoteListHelper(telemetryClient, Settings.STORAGE_ACCOUNT, Settings.RPC_CLIENT, Configurations.GetMongoDbConnectionInfo());
                        result = await helper.GetVotesAsync(parameters);
                    }
                }
                else
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)GetRecordVoteListResultEnum.MissingToken;
                }
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

            //get message for result id
            string message = EnumDescription.GetEnumDescription((GetRecordVoteListResultEnum)result.ResultId);

            //build json result object
            string res = JsonConvert.SerializeObject(result);
            dynamic resultParsed = JObject.Parse(res);
            dynamic jsonresult = new JObject();
            jsonresult.message = message;
            jsonresult.votes = resultParsed.Votes;

            telemetryClient.TrackTrace("Finishing function: GetRecordVoteList");

            //send ok result or bad request
            return (result.IsSucceded) ? (ActionResult)new OkObjectResult(jsonresult) : (ActionResult)new BadRequestObjectResult(jsonresult);
        }
    }
}