using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Enums;
using TuVotoCuenta.Helpers;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
	public class SignInViewModel : BaseViewModel
	{
		INavigation navigation = null;
		public static bool EmailValidation = false;
		public ObservableCollection<Slide> Slides { get; set; }

		public SignInViewModel(INavigation navigation)
		{
			this.navigation = navigation;
			InitializeViewModel();
		}

		void InitializeViewModel()
		{
			Slides = new ObservableCollection<Slide>
			{
				new Slide { ImageUrl ="master.jpg", Name = "Bienvenido a TuVotoCuenta" }
			};

			SignInCommand = new Command(async () => await SignIn());
			SignUpCommand = new Command(() => SignUp());
		}

        async Task SignIn()
		{
#if DEBUG
            if ((bool)Application.Current.Resources["LoginOk"])
            {
                Application.Current.MainPage = new MasterPage();
                return;
            }
#endif

			if (!ValidateInformation())
                await Application.Current.MainPage.DisplayAlert("Aviso", "Verifica que todos los campos se encuentren completos.", "Aceptar");
            else if (!IsBusy)
            {
                IsBusy = true;

                //clean any previous session
                SignOutPage.CleanCurrentSession();
                //launch task
                await Task.Run(async () =>
                {
                    SignInAccountRequest model = new SignInAccountRequest() { username = Username, password = Password };
                    SignInAccountResponse response = await RestHelper.SignInAccountAsync(model);

                    if (response == null)
                    {
                        throw new AggregateException(SignInAccountResultEnum.Failed.ToString());
                    }
                    else
                    {
                        if (response.IsSucceded)
                        {
                            Settings.Profile_Username = response.Username;
                            Settings.Profile_Account = response.Account;
                            Settings.Profile_Picture = $"{Settings.ImageStorageUrl}{response.Image}";
                        }
                        else
                        {
                            if (response.ResultId == (int)SignInAccountResultEnum.Failed)
                            {
                                throw new AggregateException(SignInAccountResultEnum.Failed.ToString());
                            }
                            else if (response.ResultId == (int)SignInAccountResultEnum.IncorrectPassword)
                            {
                                throw new AggregateException(SignInAccountResultEnum.IncorrectPassword.ToString());
                            }
                            else if (response.ResultId == (int)SignInAccountResultEnum.NotExists)
                            {
                                throw new AggregateException(SignInAccountResultEnum.NotExists.ToString());
                            }
                            else
                            {
                                throw new AggregateException(SignInAccountResultEnum.Failed.ToString());
                            }
                        }
                    }

                }).ContinueWith((b) =>
                {
                    if (b.Exception != null)
                    {
                        if (b.Exception.InnerExceptions[0].Message == SignInAccountResultEnum.Failed.ToString())
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Application.Current.MainPage.DisplayAlert("Aviso", "Se presentó un problema al realizar la identificación.", "Aceptar");
                            });
                        }
                        else if (b.Exception.InnerExceptions[0].Message == SignInAccountResultEnum.IncorrectPassword.ToString())
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Application.Current.MainPage.DisplayAlert("Aviso", "Contraseña incorrecta.", "Aceptar");
                            });
                        }
                        else if (b.Exception.InnerExceptions[0].Message == SignInAccountResultEnum.NotExists.ToString())
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Application.Current.MainPage.DisplayAlert("Aviso", "El usuario no se encuentra registrado.", "Aceptar");
                            });
                        }

                        IsBusy = false;
                    }
                    else
                    {
                        IsBusy = false;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current.MainPage = new MasterPage();
                        });
                    }
                });
            }
		}

		void SignUp()
		{
			Application.Current.MainPage = new SignUpPage();
		}

		bool ValidateInformation()
		{
			if (string.IsNullOrEmpty(Username))
				return false;
			if (string.IsNullOrEmpty(Password))
				return false;

			return true;
		}

		#region Commands

		public Command SignInCommand { get; set; }
		public Command SignUpCommand { get; set; }

		#endregion

		#region Binding attributes

		string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

		string password;
		public string Password
		{
			get { return password; }
			set { SetProperty(ref password, value); }
		}

		#endregion
	}
}