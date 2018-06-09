using System;
namespace TuVotoCuenta
{
    public class SettingsInitializer
    {

        public static void InitSettings()
        {
            Settings.FunctionURL = "https://{funtion}.azurewebsites.net/";
            Settings.ImageStorageUrl = "https://{storage}.blob.core.windows.net/accountimages/";
            Settings.Cryptography = "{CryptoKey}";
        }

    }
}
