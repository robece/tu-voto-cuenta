using TuVotoCuenta.Interfaces;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        INavigation navigation = null;

        public MenuViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
			UserFullname = $"Hola, {Settings.Profile_Username}!";;
			UserAccount = Settings.Profile_Account;
			UserPicture = Settings.Profile_Picture;
        }

        #region Commands

        #endregion

        #region Binding attributes

        string userPicture;
        public string UserPicture
        {
            get { return userPicture; }
            set { SetProperty(ref userPicture, value); }
        }

        string userFullname;
        public string UserFullname
        {
            get { return userFullname; }
            set { SetProperty(ref userFullname, value); }
        }

		string userAccount;
		public string UserAccount
        {
			get { return userAccount; }
			set { SetProperty(ref userAccount, value); }
        }

        #endregion
    }
}