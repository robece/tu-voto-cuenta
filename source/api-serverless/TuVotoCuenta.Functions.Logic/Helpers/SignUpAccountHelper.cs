using Jdenticon;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Classes;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class SignUpAccountHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;
        private string USERNAME = @"^[A-Za-z\d]{6,20}$";

        public SignUpAccountHelper(string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public async Task<SignUpAccountResponse> SignUpAccountAsync(Dictionary<ParameterTypeEnum, object> parameters)
        {
            SignUpAccountResponse result = new SignUpAccountResponse
            {
                IsSucceded = true,
                ResultId = (int)SignUpAccountResultEnum.Success
            };

            try
            {
                parameters.TryGetValue(ParameterTypeEnum.Username, out global::System.Object ousername);
                string username = ousername.ToString().ToLower();

                parameters.TryGetValue(ParameterTypeEnum.Password, out global::System.Object opassword);
                string password = opassword.ToString();

                parameters.TryGetValue(ParameterTypeEnum.AccountImagesContainer, out global::System.Object oaccountImagesContainer);
                string accountImagesContainer = oaccountImagesContainer.ToString();

                parameters.TryGetValue(ParameterTypeEnum.FunctionDirectory, out global::System.Object ofunctionDirectory);
                string functionDirectory = ofunctionDirectory.ToString();

                //validate username length
                Regex regex = new Regex(USERNAME);
                Match match = regex.Match(username);
                bool isValidUsernameLength = match.Success;

                if (!isValidUsernameLength)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)SignUpAccountResultEnum.InvalidUsernameLength;
                    return result;
                }
                
                //connecting to mongodb
                string connectionString = DBCONNECTION_INFO.ConnectionString;
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var database = mongoClient.GetDatabase(DBCONNECTION_INFO.DatabaseId);
                var userAccountCollection = database.GetCollection<UserAccount>(DBCONNECTION_INFO.UserAccountCollection);

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

                if (userAccount != null)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)SignUpAccountResultEnum.AlreadyExists;
                    return result;
                }

                TimeZoneInfo setTimeZoneInfo;
                DateTime currentDateTime;

                //Set the time zone information to US Mountain Standard Time 
                setTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");

                //Get date and time in US Mountain Standard Time
                currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, setTimeZoneInfo);

                //save username and account in mongodb
                userAccount = new UserAccount() {
                    username = username,
                    password = MD5Hash.CalculateMD5Hash(password),
                    createdDate = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                };

                await userAccountCollection.InsertOneAsync(userAccount);

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
                    var exception = string.Empty;
                    exception = (innerException.InnerException != null) ? innerException.InnerException.Message : innerException.Message;
                    var stackTrace = string.Empty;
                    stackTrace = (innerException.InnerException != null) ? innerException.InnerException.StackTrace : innerException.StackTrace;
                    System.Diagnostics.Trace.TraceError($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                }
                result.IsSucceded = false;
                result.ResultId = (int)SignUpAccountResultEnum.Failed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                result.IsSucceded = false;
                result.ResultId = (int)SignUpAccountResultEnum.Failed;
            }
            return result;
        }

        private async Task UploadAccountImageAsync(string containerName, string imageName, string imagePath)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(STORAGE_ACCOUNT);

            // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            cloudBlobContainer.CreateIfNotExistsAsync().Wait();
            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);
            await cloudBlockBlob.UploadFromFileAsync(imagePath);
        }
    }
}