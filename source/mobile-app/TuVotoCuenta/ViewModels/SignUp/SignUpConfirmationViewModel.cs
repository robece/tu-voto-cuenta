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

        void InitializeViewModel()
        {
            IsBusy = true;
            IsContinueEnabled = false;
			IsContinueGoBackEnabled = false;
            MessageTitle = "Por favor aguarda, estamos creando tu cuenta...";
            
			//clean any previous session
			SignOutPage.CleanCurrentSession();
            //launch task
			Task.Run(async () => {
                SignUpAccountResponse response = await RestHelper.SignUpAccountAsync(model);

                if (response == null)
                {
					throw new AggregateException(SignUpAccountResultEnum.Failed.ToString());
                }
                else
                {
                    if (response.IsSucceded)
                    {
                        Settings.Profile_Username = model.username.ToLower();
                        Settings.Profile_Account = response.Account;
                        Settings.Profile_Picture = $"{Settings.ImageStorageUrl}{response.Image}";
                    }
                    else
                    {
						if (response.ResultId == (int)SignUpAccountResultEnum.Failed)                     
                        {
							throw new AggregateException(SignUpAccountResultEnum.Failed.ToString());
                        }
						else if (response.ResultId == (int)SignUpAccountResultEnum.AlreadyExists)
                        {
							throw new AggregateException(SignUpAccountResultEnum.AlreadyExists.ToString());
                        }
                        else
                        {
							throw new AggregateException(SignUpAccountResultEnum.Failed.ToString());
                        }
                    }
                }            
            }).ContinueWith((b) => {
                if (b.Exception != null)
                {
					if (b.Exception.InnerExceptions[0].Message == SignUpAccountResultEnum.Failed.ToString())
                    {
                        MessageTitle = "Se presentó un problema al realizar el registro.";
                    }
					else if (b.Exception.InnerExceptions[0].Message == SignUpAccountResultEnum.AlreadyExists.ToString())
                    {
                        MessageTitle = "Es posible que ya te encuentres registrado con ese correo electrónico.";
                    }

					IsBusy = false;
					IsContinueEnabled = false;
					IsContinueGoBackEnabled = true;
					MessageSubTitle = "El proceso de registro no fue satisfactorio.";
                }
                else
                {
                    IsBusy = false;
                    IsContinueEnabled = true;
					IsContinueGoBackEnabled = false;
                    MessageTitle = $"Gracias {Settings.Profile_Username}!";
                    MessageSubTitle = "Tu cuenta ha sido creada satisfactoriamente.";
                }
            });

            ContinueCommand = new Command(() => Continue());
			ContinueGoBackCommand = new Command(() => ContinueGoBack());
        }

        void Continue()
        {
            Application.Current.MainPage = new MasterPage();
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