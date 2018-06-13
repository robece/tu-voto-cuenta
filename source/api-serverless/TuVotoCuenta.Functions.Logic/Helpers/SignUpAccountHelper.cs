using Jdenticon;
using Microsoft.ApplicationInsights;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Classes;
using TuVotoCuenta.Functions.Logic.Database;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class SignUpAccountHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;
        private TelemetryClient telemetryClient = null;

        public SignUpAccountHelper(TelemetryClient telemetryClient, string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.telemetryClient = telemetryClient;
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<SignUpAccountResponse> SignUpAccountAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            telemetryClient.TrackTrace("Starting helper");

            SignUpAccountResponse result = new SignUpAccountResponse
            {
                IsSucceded = true,
                ResultId = (int)SignUpAccountResultEnum.Success
            };

            try
            {
                telemetryClient.TrackTrace("Getting parameters");

                parameters.TryGetValue(ParameterTypeEnum.Username, out global::System.Object ousername);
                string username = ousername.ToString().ToLower();

                parameters.TryGetValue(ParameterTypeEnum.Password, out global::System.Object opassword);
                string password = opassword.ToString();

                parameters.TryGetValue(ParameterTypeEnum.AccountImagesContainer, out global::System.Object oaccountImagesContainer);
                string accountImagesContainer = oaccountImagesContainer.ToString();

                parameters.TryGetValue(ParameterTypeEnum.FunctionDirectory, out global::System.Object ofunctionDirectory);
                string functionDirectory = ofunctionDirectory.ToString();

                //database helpers
                DBUserAccountHelper dbUserAccountHelper = new DBUserAccountHelper(DBCONNECTION_INFO);

                telemetryClient.TrackTrace("Validating username length");

                //validate username length
                if (!RegexValidation.IsValidUsername(username))
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)SignUpAccountResultEnum.InvalidUsernameLength;
                    return result;
                }

                telemetryClient.TrackTrace("Validating username existance");

                //validate if account exists
                UserAccount userAccount = dbUserAccountHelper.GetUser(username);

                if (userAccount != null)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)SignUpAccountResultEnum.AlreadyExists;
                    return result;
                }

                telemetryClient.TrackTrace("Adding account to database");

                //save username and account in mongodb
                userAccount = new UserAccount()
                {
                    username = username,
                    password = MD5Hash.CalculateMD5Hash(password),
                    createdDate = Timezone.GetCustomTimeZone()
                };

                //perform insert in mongodb
                await dbUserAccountHelper.CreateUserAccount(userAccount);

                telemetryClient.TrackTrace("Adding account image to storage");

                //create unique icon, upload and delete it
                var imageName = $"{username}.png";
                try
                {
                    var directory = Path.Combine(functionDirectory, "images");
                    var filePath = Path.Combine(directory, imageName);

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    Identicon.FromValue(username, size: 160).SaveAsPng(filePath);
                    await UploadAccountImageAsync(accountImagesContainer, imageName, filePath);
                    File.Delete(filePath);
                }
                catch
                {
                    //it doesn't matter if for some reason the icon generation fails, then ignore it and proceed as success
                }

                result.Image = imageName;
            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.Flatten().InnerExceptions)
                {
                    telemetryClient.TrackException(innerException);
                }
                result.IsSucceded = false;
                result.ResultId = (int)SignUpAccountResultEnum.Failed;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                result.IsSucceded = false;
                result.ResultId = (int)SignUpAccountResultEnum.Failed;
            }

            telemetryClient.TrackTrace("Finishing helper");
            return result;
        }

        private async Task UploadAccountImageAsync(string containerName, string imageName, string imagePath)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(STORAGE_ACCOUNT);

            // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            await cloudBlobContainer.CreateIfNotExistsAsync();
            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);
            await cloudBlockBlob.UploadFromFileAsync(imagePath);
        }
    }
}