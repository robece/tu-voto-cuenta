using System;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Enums;
using TuVotoCuenta.Helpers;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class SignUpConfirmationViewModel : BaseViewModel
    {
        INavigation navigation = null;
        SignUpAccountRequest model = null;

        public SignUpConfirmationViewModel(INavigation navigation, SignUpAccountRequest model)
        {
            this.navigation = navigation;
            this.model = model;
            InitializeViewModel();
        }

        async Task InitializeViewModel()
        {
            ContinueCommand = new Command(() => Continue());
            ContinueGoBackCommand = new Command(() => ContinueGoBack());
            
            IsBusy = true;
            IsContinueEnabled = false;
            IsContinueGoBackEnabled = false;
            MessageTitle = "Por favor aguarda, estamos creando tu cuenta...";

            //clean any previous session
            SignOutPage.CleanCurrentSession();
            //launch task

            SignUpAccountResponse response = await RestHelper.SignUpAccountAsync(model);


            if (response.Status == ResponseStatus.Ok)
            {
                Settings.Profile_Username = model.username.ToLower();
                Settings.Profile_Picture = $"{Settings.ImageStorageUrl}/{Settings.AccountImageStorageUrl}/{response.Image}";
                IsBusy = false;
                IsContinueEnabled = true;
                IsContinueGoBackEnabled = false;
                MessageTitle = $"¡Gracias {Settings.Profile_Username}!";
                MessageSubTitle = "Tu cuenta ha sido creada satisfactoriamente.";
            }
            else
            {
                IsBusy = false;
                IsContinueEnabled = false;
                IsContinueGoBackEnabled = true;
                MessageTitle = "El proceso de registro no fue satisfactorio.";
                MessageSubTitle = response.Message;
            }

           
        }

        void Continue()
        {
            Application.Current.MainPage = new MasterPage() { IsPresented = true };
        }

        void ContinueGoBack()
        {
            Application.Current.MainPage = new SignUpPage();
        }

        #region Commands

        public Command ContinueCommand { get; set; }
        public Command ContinueGoBackCommand { get; set; }

        #endregion

        #region Binding attributes

        string messageTitle;
        public string MessageTitle
        {
            get { return messageTitle; }
            set { SetProperty(ref messageTitle, value); }
        }

        string messageSubTitle;
        public string MessageSubTitle
        {
            get { return messageSubTitle; }
            set { SetProperty(ref messageSubTitle, value); }
        }

        bool isContinueEnabled;
        public bool IsContinueEnabled
        {
            get { return isContinueEnabled; }
            set { SetProperty(ref isContinueEnabled, value); }
        }

        bool isContinueGoBackEnabled;
        public bool IsContinueGoBackEnabled
        {
            get { return isContinueGoBackEnabled; }
            set { SetProperty(ref isContinueGoBackEnabled, value); }
        }

        #endregion
    }
}