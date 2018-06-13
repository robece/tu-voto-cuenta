using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using TuVotoCuenta.Functions.Domain.Enums;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;
using TuVotoCuenta.Functions.Domain.Models.Responses;
using TuVotoCuenta.Functions.Logic.Classes;
using TuVotoCuenta.Functions.Logic.Database;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class SignInAccountHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private MongoDBConnectionInfo DBCONNECTION_INFO = null;
        private TelemetryClient telemetryClient = null;

        public SignInAccountHelper(TelemetryClient telemetryClient, string storageAccount, string rpcClient, MongoDBConnectionInfo dbConnectionInfo)
        {
            this.telemetryClient = telemetryClient;
            this.STORAGE_ACCOUNT = storageAccount;
            this.RPC_CLIENT = rpcClient;
            this.DBCONNECTION_INFO = dbConnectionInfo;
        }

        public SignInAccountResponse SignInAccount(Dictionary<ParameterTypeEnum, object> parameters)
        {
            telemetryClient.TrackTrace("Starting helper");

            SignInAccountResponse result = new SignInAccountResponse
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

                //database helpers
                DBUserAccountHelper dbUserAccountHelper = new DBUserAccountHelper(DBCONNECTION_INFO);

                telemetryClient.TrackTrace("Validating username length");

                //validate username length
                if (!RegexValidation.IsValidUsername(username))
                {
                    result.IsSucceded = false;
                    result.ResultId = (int)SignInAccountResultEnum.InvalidUsernameLength;
                    return result;
                }

                telemetryClient.TrackTrace("Validating username existance");

                //validate if account exists
                UserAccount userAccount = dbUserAccountHelper.GetUser(username);

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
    }
}