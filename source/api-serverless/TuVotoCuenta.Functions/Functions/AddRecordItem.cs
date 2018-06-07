using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;

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
                //account
                var account = data?.account;
                var password = data?.password;
                //record data
                var deviceHash = data?.deviceHash;
                var boxNumber = data?.boxNumber;
                var boxSection = data?.boxSection;
                var locationDetails = data?.locationDetails;
                var entity = data?.entity;
                var municipality = data?.municipality;
                var locality = data?.locality;
                var partyPAN = data?.partyPAN;
                var partyPRI = data?.partyPRI;
                var partyPRD = data?.partyPRD;
                var partyVerde = data?.partyVerde;
                var partyPT = data?.partyPT;
                var partyMC = data?.partyMC;
                var partyNA = data?.partyNA;
                var partyMOR = data?.partyMOR;
                var partyES = data?.partyES;
                var partyINDJai = data?.partyINDJai;
                var partyOtro = data?.partyOtro;
                var partyPANMC = data?.partyPANMC;
                var partyPANPRD = data?.partyPANPRD;
                var partyPRDPANMC = data?.partyPRDPANMC;
                var partyPRDMC = data?.partyPRDMC;
                var partyMORPT = data?.partyMORPT;
                var partyMORES = data?.partyMORES;
                var partyPTESMOR = data?.partyPTESMOR;
                var partyPRIVERNA = data?.partyPRIVERNA;
                var partyPRIVER = data?.partyPRIVER;
                var partyPRINA = data?.partyPRINA;
                var partyVERNA = data?.partyVERNA;
                var partyPTES = data?.partyPTES;
                var partyPRDPAN = data?.partyPRDPAN;
                var image = data?.image;
                var imageLatitude = data?.imageLatitude;
                var imageLongitude = data?.imageLongitude;
                var hash = data?.hash;

                RecordItem recordItem = new RecordItem
                {
                    deviceHash = deviceHash,
                    boxNumber = boxNumber,
                    boxSection = boxSection,
                    locationDetails = locationDetails,
                    entity = entity,
                    municipality = municipality,
                    locality = locality,
                    partyPAN = partyPAN,
                    partyPRI = partyPRI,
                    partyPRD = partyPRD,
                    partyVerde = partyVerde,
                    partyPT = partyPT,
                    partyMC = partyMC,
                    partyNA = partyNA,
                    partyMOR = partyMOR,
                    partyES = partyES,
                    partyINDJai = partyINDJai,
                    partyOtro = partyOtro,
                    partyPANMC = partyPANMC,
                    partyPANPRD = partyPANPRD,
                    partyPRDPANMC = partyPRDPANMC,
                    partyPRDMC = partyPRDMC,
                    partyMORPT = partyMORPT,
                    partyMORES = partyMORES,
                    partyPTESMOR = partyPTESMOR,
                    partyPRIVERNA = partyPRIVERNA,
                    partyPRIVER = partyPRIVER,
                    partyPRINA = partyPRINA,
                    partyVERNA = partyVERNA,
                    partyPTES = partyPTES,
                    partyPRDPAN = partyPRDPAN,
                    image = image,
                    imageLatitude = imageLatitude,
                    imageLongitude = imageLongitude,
                    hash = hash,
                    //username = account
                };

                //Dictionary<ParameterTypeEnum, object> parameters = new Dictionary<ParameterTypeEnum, object>
                //{
                //    { ParameterTypeEnum.RecordAccount, account },
                //    { ParameterTypeEnum.Password, password },
                //    { ParameterTypeEnum.MasterAccount, Settings.MASTER_ACCOUNT },
                //    { ParameterTypeEnum.ContractAddress, Settings.CONTRACT_ADDRESS },
                //    { ParameterTypeEnum.ContractABI, Settings.CONTRACT_ABI },
                //    { ParameterTypeEnum.RecordItem, recordItem }
                //};

                MongoDBConnectionInfo dbConnectionInfo = new MongoDBConnectionInfo()
                {
                    ConnectionString = Settings.COSMOSDB_CONNECTIONSTRING,
                    DatabaseId = Settings.COSMOSDB_DATABASEID,
                    RecordItemCollection = Settings.COSMOSDB_RECORDITEMCOLLECTION
                };

                //AddRecordItemHelper helper = new AddRecordItemHelper(Settings.STORAGE_ACCOUNT, Settings.RPC_CLIENT, dbConnectionInfo);
                //result = await helper.AddRecordItemAsync(parameters);
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
                result.ResultId = (int)AddRecordItemResultEnum.Failed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                result.IsSucceded = false;
                result.ResultId = (int)AddRecordItemResultEnum.Failed;
            }

            return (ActionResult)new OkObjectResult(result);
        }
    }
}