using System;
namespace TuVotoCuenta
{
    public class SettingsInitializer
    {

        public static void InitSettings()
        {
            Settings.FunctionURL = "FUNCTION_URL";
            Settings.ImageStorageUrl = "STORAGE_URL";
            Settings.AccountImageStorageUrl = "ACCOUNT_IMAGE_CONTAINER";
            Settings.RedordImageStorageUrl = "RECORDS_IMAGE_CONTAINER";
            Settings.Cryptography = "CRYPTO_KEY";
            Settings.AppCenterIOS = "APP_CENTER_IOS";
            Settings.AppCenterDroid = "APP_CENTER_DROID";
        }

    }
}
