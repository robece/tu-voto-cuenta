using System;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Enums;
using TuVotoCuenta.Helpers;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        INavigation navigation = null;

		public AccountViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Mi Cuenta";
			Username = Settings.Profile_Username;
			AccountImage = Settings.Profile_Picture;
        }

        #region Commands

        #endregion

        #region Binding attributes

        string username;
		public string Username
        {
			get { return username; }
			set { SetProperty(ref username, value); }
        }

		string accountImage;
        public string AccountImage
        {
            get { return accountImage; }
            set { SetProperty(ref accountImage, value); }
        }
        
        #endregion
    }
}