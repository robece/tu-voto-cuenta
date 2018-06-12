using System;
namespace TuVotoCuenta
{
    public class SettingsInitializer
    {

        public static void InitSettings()
        {
            Settings.FunctionURL = "FUNCTION_URL";
            Settings.ImageStorageUrl = "STORAGE_URL";
            Settings.Cryptography = "CRYPTO_KEY";
        }

    }
}
