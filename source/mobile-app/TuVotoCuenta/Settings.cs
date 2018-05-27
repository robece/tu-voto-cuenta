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
		private const string UserAccountKey = "UserAccountKey";
		private const string UserEmailKey = "UserEmail";
        private const string UserPictureKey = "UserPicture";
        private const string UserFullnameKey = "UserFullname";


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

		public static string UserAccount
        {
            get
            {
				return AppSettings.GetValueOrDefault(UserAccountKey, SettingsDefault);
            }
            set
            {
				AppSettings.AddOrUpdateValue(UserAccountKey, value);
            }
        }

		public static string UserEmail
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserEmailKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserEmailKey, value);
            }
        }

        public static string UserPicture
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserPictureKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserPictureKey, value);
            }
        }

        public static string UserFullname
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserFullnameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserFullnameKey, value);
            }
        }
    }
}