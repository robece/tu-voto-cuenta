using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Helpers;

namespace TuVotoCuenta.Functions
{
    public static class AddRecordItemUnit
    {
        private static string USERNAME = @"^[A-Za-z\d]{6,12}$";

        [FunctionName("AddRecordItemUnit")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, TraceWriter log, ExecutionContext context)
        {
            System.Diagnostics.Trace.TraceInformation("Initialize HttpTrigger - AddRecordItemUnit");
            AddRecordItemResponse result = new AddRecordItemResponse
            {
                IsSucceded = true,
                ResultId = (int)AddRecordItemResultEnum.Success
            };

            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(requestBody);

                //
                //
                // DISCLAIMER: ESTE CODIGO ESTA MUY FEO, ES TEMPORAL Y SERA INTEGRADO EL CORE A LA FUNCTION DE AddRecordItem
                //             ESTA FUNCTION DESAPARECERA EN CUANTO SE TENGA LISTA LA FUNCION FINAL.
                //

                //blockchain data
                string username = data?.username;
                string hash = data?.hash;
                hash = hash.ToLower();
                MongoDBConnectionInfo dbConnectionInfo = new MongoDBConnectionInfo()
                {
                    ConnectionString = Settings.COSMOSDB_CONNECTIONSTRING,
                    DatabaseId = Settings.COSMOSDB_DATABASEID,
                    RecordItemCollection = Settings.COSMOSDB_RECORDITEMCOLLECTION,
                    UserAccountCollection = Settings.COSMOSDB_USERACCOUNTCOLLECTION
                };

                //validate username length
                Regex regex = new Regex(USERNAME);
                Match match = regex.Match(username);
                bool isValidUsernameLength = match.Success;

                if (!isValidUsernameLength)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordItemResultEnum.InvalidUsernameLength;

                    //get message for result id
                    string message1 = EnumDescription.GetEnumDescription((AddRecordItemResultEnum)result.ResultId);

                    //build json result object
                    dynamic jsonresult1 = new JObject();
                    jsonresult1.message = message1;

                    //send ok result or bad request
                    return (result.IsSucceded) ? (ActionResult)new OkObjectResult(jsonresult1) : (ActionResult)new BadRequestObjectResult(jsonresult1);
                }

                //connecting to mongodb
                string connectionString = dbConnectionInfo.ConnectionString;
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var database = mongoClient.GetDatabase(dbConnectionInfo.DatabaseId);
                var recordItemCollection = database.GetCollection<RecordItem>(dbConnectionInfo.RecordItemCollection);
                var userAccountCollection = database.GetCollection<UserAccount>(dbConnectionInfo.UserAccountCollection);

                //validate if account exists
                UserAccount userAccount = null;
                try
                {
                    userAccount = userAccountCollection.AsQueryable<UserAccount>().Where<UserAccount>(sb => sb.username == username).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.TraceWarning($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                }

                if (userAccount == null)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordItemResultEnum.UsernameNotExists;

                    //get message for result id
                    string message1 = EnumDescription.GetEnumDescription((AddRecordItemResultEnum)result.ResultId);

                    //build json result object
                    dynamic jsonresult1 = new JObject();
                    jsonresult1.message = message1;

                    //send ok result or bad request
                    return (result.IsSucceded) ? (ActionResult)new OkObjectResult(jsonresult1) : (ActionResult)new BadRequestObjectResult(jsonresult1);

                }

                //validate if record item exists for voting
                RecordItem recordItem = null;
                try
                {
                    recordItem = recordItemCollection.AsQueryable<RecordItem>().Where<RecordItem>(sb => sb.hash == hash).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.TraceWarning($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                }

                if (recordItem != null)
                {
                    //there is no record item linked with this vote
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordItemResultEnum.AlreadyExists;

                    //get message for result id
                    string message1 = EnumDescription.GetEnumDescription((AddRecordItemResultEnum)result.ResultId);

                    //build json result object
                    dynamic jsonresult1 = new JObject();
                    jsonresult1.message = message1;

                    //send ok result or bad request
                    return (result.IsSucceded) ? (ActionResult)new OkObjectResult(jsonresult1) : (ActionResult)new BadRequestObjectResult(jsonresult1);
                }
                else
                {
                    BlockchainHelper bh = new BlockchainHelper(Settings.STORAGE_ACCOUNT, Settings.RPC_CLIENT, Settings.MASTER_ADDRESS, Settings.MASTER_PRIVATEKEY);

                    var res_AddRecordAsync = await bh.AddRecordAsync(hash.ToString(), username.ToString(), Settings.CONTRACT_ADDRESS, Settings.CONTRACT_ABI);
                    System.Diagnostics.Trace.TraceInformation($"Add record item result: {res_AddRecordAsync}");

                    if (string.IsNullOrEmpty(res_AddRecordAsync))
                    {
                        //there was an error adding the record to the blockchain
                        result.IsSucceded = false;
                        result.ResultId = (int)AddRecordItemResultEnum.BlockchainIssue;

                        //get message for result id
                        string message1 = EnumDescription.GetEnumDescription((AddRecordItemResultEnum)result.ResultId);

                        //build json result object
                        dynamic jsonresult1 = new JObject();
                        jsonresult1.message = message1;

                        //send ok result or bad request
                        return (result.IsSucceded) ? (ActionResult)new OkObjectResult(jsonresult1) : (ActionResult)new BadRequestObjectResult(jsonresult1);
                    }

                    TimeZoneInfo setTimeZoneInfo;
                    DateTime currentDateTime;

                    //Set the time zone information to US Mountain Standard Time 
                    setTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");

                    //Get date and time in US Mountain Standard Time
                    currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, setTimeZoneInfo);

                    //save record item
                    RecordItem item = new RecordItem
                    {
                        hash = hash,
                        transactionId = res_AddRecordAsync,
                        username = username,
                        createdDate = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    //perform insert in mongodb
                    await recordItemCollection.InsertOneAsync(item);
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