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
		const string CurrentRecordItemKey = "CurrentRecordItem";

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

        public static string CurrentRecordItem
		{
			get
            {
				return AppSettings.GetValueOrDefault(CurrentRecordItemKey, string.Empty);
            }
            set
            {
				AppSettings.AddOrUpdateValue(CurrentRecordItemKey, value);
            }
		}
    }
}