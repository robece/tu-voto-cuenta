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
		private const string AccountImageStorageUrlKey = "AccountImageStorageUrlKey";
        private const string RecordImageStorageUrlKey = "RecordsImageStorageUrlKey";
        private const string ImageStorageUrlKey = "ImageStorageUrlKey";
		private const string CurrentRecordItemKey = "CurrentRecordItemKey";
		private const string Profile_UsernameKey = "Profile_UsernameKey";
		private const string Profile_PictureKey = "Profile_PictureKey";
        private const string CryptographyKey = "CryptographyKey";
        private const string AppCenteriOSKey = "AppCenteriOSKey";
        private const string AppCenterDroidKey = "AppCenterDroidKey";

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


        public static string RedordImageStorageUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(RecordImageStorageUrlKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(RecordImageStorageUrlKey, value);
            }
        }

        public static string AccountImageStorageUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccountImageStorageUrlKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AccountImageStorageUrlKey, value);
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

        public static string Cryptography
        {
            get
            {
                return AppSettings.GetValueOrDefault(CryptographyKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(CryptographyKey, value);
            }
        }

        public static string AppCenterIOS
        {
            get
            {
                return AppSettings.GetValueOrDefault(AppCenteriOSKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AppCenteriOSKey, value);
            }
        }

        public static string AppCenterDroid
        {
            get
            {
                return AppSettings.GetValueOrDefault(AppCenterDroidKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AppCenterDroidKey, value);
            }
        }
    }
}