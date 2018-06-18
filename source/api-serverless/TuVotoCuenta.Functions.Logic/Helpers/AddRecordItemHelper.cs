using Microsoft.ApplicationInsights;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Classes;
using TuVotoCuenta.Functions.Logic.Database;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class AddRecordItemHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;
        private TelemetryClient telemetryClient = null;

        public AddRecordItemHelper(TelemetryClient telemetryClient, string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.telemetryClient = telemetryClient;
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<AddRecordItemResponse> AddRecordItemAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            telemetryClient.TrackTrace("Starting helper");

            AddRecordItemResponse result = new AddRecordItemResponse
            {
                IsSucceded = true,
                ResultId = (int)AddRecordItemResultEnum.Success
            };
            try
            {
                telemetryClient.TrackTrace("Getting parameters");

                parameters.TryGetValue(ParameterTypeEnum.RecordItem, out global::System.Object orecordItemRequest);
                Domain.Models.Request.RecordItem recordItemRequest = orecordItemRequest as Domain.Models.Request.RecordItem;

                parameters.TryGetValue(ParameterTypeEnum.MasterAddress, out global::System.Object omasterAddress);
                string masterAddress = omasterAddress.ToString();

                parameters.TryGetValue(ParameterTypeEnum.MasterPrivateKey, out global::System.Object omasterPrivateKey);
                string masterPrivateKey = omasterPrivateKey.ToString();

                parameters.TryGetValue(ParameterTypeEnum.ContractAddress, out global::System.Object ocontractAddress);
                string contractAddress = ocontractAddress.ToString();

                parameters.TryGetValue(ParameterTypeEnum.ContractABI, out global::System.Object ocontractABI);
                string contractABI = ocontractABI.ToString();

                parameters.TryGetValue(ParameterTypeEnum.RecordItemImageContainer, out global::System.Object orecordItemImageContainer);
                string recordItemImageContainer = orecordItemImageContainer.ToString();

                //database helpers
                DBRecordItemHelper dbRecordItemHelper = new DBRecordItemHelper(DBCONNECTION_INFO);
                DBUserAccountHelper dbUserAccountHelper = new DBUserAccountHelper(DBCONNECTION_INFO);

                //blockchain helper
                BlockchainHelper bh = new BlockchainHelper(telemetryClient, STORAGE_ACCOUNT, RPC_CLIENT, masterAddress, masterPrivateKey);

                telemetryClient.TrackTrace("Validating username length");

                //validate username length
                if (!RegexValidation.IsValidUsername(recordItemRequest.username))
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordItemResultEnum.InvalidUsernameLength;
                    return result;
                }

                telemetryClient.TrackTrace("Validating username existance");

                //validate if account exists
                UserAccount userAccount = dbUserAccountHelper.GetUser(recordItemRequest.username);

                if (userAccount == null)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordItemResultEnum.UsernameNotExists;
                    return result;
                }

                telemetryClient.TrackTrace("Validating record item existance");

                //validate if record item exists for voting
                RecordItem recordItemExists = dbRecordItemHelper.GetRecordItem(recordItemRequest.hash);

                if (recordItemExists != null)
                {
                    //there is no record item linked with this vote
                    result.IsSucceded = false;
                    result.ResultId = (int)AddRecordItemResultEnum.AlreadyExists;
                }
                else
                {
                    telemetryClient.TrackTrace("Adding record item to blockchain");

                    var res_AddRecordAsync = await bh.AddRecordAsync(recordItemRequest.hash, recordItemRequest.username, contractAddress, contractABI);
                    telemetryClient.TrackTrace($"Add record item result: {res_AddRecordAsync}");

                    if (string.IsNullOrEmpty(res_AddRecordAsync))
                    {
                        //there was an error adding the record to the blockchain
                        result.IsSucceded = false;
                        result.ResultId = (int)AddRecordItemResultEnum.BlockchainIssue;
                        return result;
                    }

                    telemetryClient.TrackTrace("Adding record item to database");

                    RecordItem recordItem = RecordItemParser.TransformRecordItem(recordItemRequest);

                    recordItem.hash = recordItem.hash.ToLower();
                    recordItem.transactionId = res_AddRecordAsync;
                    recordItem.createdDate = Timezone.GetCustomTimeZone();
                    recordItem.image = recordItem.image + ".png";

                    //perform insert in mongodb
                    await dbRecordItemHelper.CreateRecordItem(recordItem);

                    telemetryClient.TrackTrace("Adding record item image to storage");

                    //upload image to blobstorage
                    byte[] buffer = Convert.FromBase64String(recordItemRequest.ImageBytes);
                    await UploadRecordItemImageAsync(recordItemImageContainer, recordItem.image, buffer);
                }
            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.Flatten().InnerExceptions)
                {
                    telemetryClient.TrackException(innerException);
                }
                result.IsSucceded = false;
                result.ResultId = (int)AddRecordItemResultEnum.Failed;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                result.IsSucceded = false;
                result.ResultId = (int)AddRecordItemResultEnum.Failed;
            }

            telemetryClient.TrackTrace("Finishing helper");
            return result;
        }

        private async Task UploadRecordItemImageAsync(string containerName, string imageName, byte[] imageArray)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(STORAGE_ACCOUNT);

            // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            await cloudBlobContainer.CreateIfNotExistsAsync();
            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);
            await cloudBlockBlob.UploadFromByteArrayAsync(imageArray, 0, imageArray.Length);
        }
    }
}