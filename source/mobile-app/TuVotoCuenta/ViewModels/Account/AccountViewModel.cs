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

        async void InitializeViewModel()
        {
            Title = "Mi Cuenta";
			Account = Settings.Profile_Account;
			AccountImage = Settings.Profile_Picture;

			//launch task
			Amount = $"Tienes 0.0 créditos en tu cuenta"; 
            await Task.Run(async () =>
            {
				GetBalanceAccountRequest model = new GetBalanceAccountRequest() { account = Settings.Profile_Account };
				GetBalanceAccountResponse response = await RestHelper.GetBalanceAccountAsync(model);

                if (response == null)
                {
                    throw new AggregateException(GetBalanceAccountResultEnum.Failed.ToString());
                }
                else
                {
                    if (response.IsSucceded)
                    {
						Amount = $"Tienes {response.Amount} créditos en tu cuenta";
                    }
                }            
			});
        }

        #region Commands

        #endregion

        #region Binding attributes

        string account;
		public string Account
        {
			get { return account; }
			set { SetProperty(ref account, value); }
        }

		string accountImage;
        public string AccountImage
        {
            get { return accountImage; }
            set { SetProperty(ref accountImage, value); }
        }

		string amount;
        public string Amount
        {
			get { return amount; }
			set { SetProperty(ref amount, value); }
        }
        
        #endregion
    }
}