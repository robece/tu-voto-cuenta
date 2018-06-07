using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class AddRecordItemHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;

        public AddRecordItemHelper(string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<AddRecordItemResponse> AddRecordItemAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            AddRecordItemResponse result = new AddRecordItemResponse();
            result.IsSucceded = true;
            result.ResultId = (int)AddRecordItemResultEnum.Success;
            try
            {
                parameters.TryGetValue(ParameterTypeEnum.Username, out global::System.Object ousername);
                string username = ousername.ToString().ToLower();

                parameters.TryGetValue(ParameterTypeEnum.Password, out global::System.Object opassword);
                string password = opassword.ToString();

                //parameters.TryGetValue(ParameterTypeEnum.MasterAccount, out global::System.Object omasterAccount);
                //string masterAccount = omasterAccount.ToString();

                parameters.TryGetValue(ParameterTypeEnum.ContractAddress, out global::System.Object ocontractAddress);
                string contractAddress = ocontractAddress.ToString();

                parameters.TryGetValue(ParameterTypeEnum.ContractABI, out global::System.Object ocontractABI);
                string contractABI = ocontractABI.ToString();

                parameters.TryGetValue(ParameterTypeEnum.RecordItem, out object orecordItem);
                RecordItem recordItem = orecordItem as RecordItem;

                //save record in blockchain
                //BlockchainHelper bh = new BlockchainHelper(STORAGE_ACCOUNT, RPC_CLIENT);

                //var res_UnlockAccountAsync = await bh.UnlockAccountAsync(recordItem.account, password);
                //System.Diagnostics.Trace.TraceInformation($"Account: {recordItem.account} unlock result: {res_UnlockAccountAsync}");

                //var res_AddItemToContractAsync = await bh.AddItemToContractAsync(recordItem.account, recordItem.hash, contractAddress, contractABI);
                //System.Diagnostics.Trace.TraceInformation($"Add record item result: {res_AddItemToContractAsync}");

                //save record in MongoDB
                //recordItem.blockchainTransaction = res_AddItemToContractAsync;
                //recordItem.createdDate = DateTime.Now.ToString();

                string connectionString = DBCONNECTION_INFO.ConnectionString;
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var database = mongoClient.GetDatabase(DBCONNECTION_INFO.DatabaseId);
                var recordItemCollection = database.GetCollection<RecordItem>(DBCONNECTION_INFO.RecordItemCollection);

                //perform insert in MongoDB
                await recordItemCollection.InsertOneAsync(recordItem);
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
                result.ResultId = (int)AddRecordItemResultEnum.Failed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                result.IsSucceded = false;
                result.ResultId = (int)AddRecordItemResultEnum.Failed;
            }
            return result;
        }
    }
}