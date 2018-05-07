using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TuVotoCuenta
{
    public static class Settings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        static readonly string SettingsDefault = string.Empty;

        const string IsLoggedInKey = "IsLoggedIn";

        #endregion

        public static bool IsLoggedIn
        {
            get
            {
                return AppSettings.GetValueOrDefault(IsLoggedInKey, false);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IsLoggedInKey, value);
            }
        }
    }
}