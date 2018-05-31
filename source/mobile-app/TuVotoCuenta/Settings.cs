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

		private const string FunctionURLKey = "FunctionURLKey";
		private const string ImageStorageUrlKey = "ImageStorageUrlKey";
		private const string CurrentRecordItemKey = "CurrentRecordItemKey";
		private const string Profile_AccountKey = "Profile_AccountKey";
		private const string Profile_UsernameKey = "Profile_UsernameKey";
		private const string Profile_PictureKey = "Profile_PictureKey";


        #endregion

		public static string FunctionURL
        {
            get
            {
                return AppSettings.GetValueOrDefault(FunctionURLKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FunctionURLKey, value);
            }
        }

		public static string ImageStorageUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(ImageStorageUrlKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ImageStorageUrlKey, value);
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

		public static string Profile_Account
        {
            get
            {
				return AppSettings.GetValueOrDefault(Profile_AccountKey, SettingsDefault);
            }
            set
            {
				AppSettings.AddOrUpdateValue(Profile_AccountKey, value);
            }
        }

		public static string Profile_Username
        {
            get
            {
				return AppSettings.GetValueOrDefault(Profile_UsernameKey, SettingsDefault);
            }
            set
            {
				AppSettings.AddOrUpdateValue(Profile_UsernameKey, value);
            }
        }

		public static string Profile_Picture
		{
			get
			{
				return AppSettings.GetValueOrDefault(Profile_PictureKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(Profile_PictureKey, value);
			}
		}
    }
}