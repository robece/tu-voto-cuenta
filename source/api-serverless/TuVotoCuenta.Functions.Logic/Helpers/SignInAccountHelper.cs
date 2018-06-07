using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Classes;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class SignInAccountHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;
        private string USERNAME = @"^[A-Za-z\d]{6,12}$";

        public SignInAccountHelper(string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public SignInAccountResponse SignInAccount(Dictionary<ParameterTypeEnum, object> parameters)
        {
            SignInAccountResponse result = new SignInAccountResponse
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

                //validate username length
                Regex regex = new Regex(USERNAME);
                Match match = regex.Match(username);
                bool isValidUsernameLength = match.Success;

                if (!isValidUsernameLength)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)SignInAccountResultEnum.InvalidUsernameLength;
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

                if (userAccount == null)
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)SignInAccountResultEnum.NotExists;
                    return result;
                }
                else
                {
                    if (userAccount.password == MD5Hash.CalculateMD5Hash(password))
                    {
                        result.IsSucceded = true;
                        result.ResultId = (int)SignInAccountResultEnum.Success;
                        result.Username = userAccount.username;
                        result.Image = $"{userAccount.username}.png";
                    }
                    else
                    {
                        result.IsSucceded = false;
                        result.ResultId = (int)SignInAccountResultEnum.IncorrectPassword;
                        return result;
                    }
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
    }
}