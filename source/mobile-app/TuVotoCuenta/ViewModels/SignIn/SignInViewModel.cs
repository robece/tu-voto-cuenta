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
				new Slide { ImageUrl ="slide1.png", Name = "" }
			};

			SignInCommand = new Command(() => SignIn());
			ForgotPasswordCommand = new Command(() => ForgotPassword());
			SignUpCommand = new Command(() => SignUp());
		}

		async void SignIn()
		{
			if (!EmailValidation)
				await Application.Current.MainPage.DisplayAlert("Aviso", "Verifica tu correo electrónico", "Aceptar");
			else
			{
				if (!ValidateInformation())
					await Application.Current.MainPage.DisplayAlert("Aviso", "Verifica que todos los campos se encuentren completos", "Aceptar");
				else if (!IsBusy)
				{
					IsBusy = true;

					//clean any previous session
                    SignOutPage.CleanCurrentSession();
                    //launch task
					await Task.Run(async () =>
					{
						SignInAccountRequest model = new SignInAccountRequest() { email = Email, password = Password };
						SignInAccountResponse response = await RestHelper.SignInAccountAsync(model);

						if (response == null)
						{
							throw new AggregateException(SignInAccountResultEnum.Failed.ToString());
						}
						else
						{
							if (response.IsSucceded)
							{
								Settings.UserEmail = response.Email;
								Settings.UserFullname = response.Fullname;
								Settings.UserAccount = response.Account;
								Settings.UserPicture = $"{Settings.ImageStorageUrl}{response.Image}";
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
									await Application.Current.MainPage.DisplayAlert("TuVotoCuenta", "Se presentó un problema al realizar la identificación.", "Aceptar");
                                });
                            }
							else if (b.Exception.InnerExceptions[0].Message == SignInAccountResultEnum.IncorrectPassword.ToString())
                            {
								Device.BeginInvokeOnMainThread(async () =>
                                {
									await Application.Current.MainPage.DisplayAlert("TuVotoCuenta", "Contraseña incorrecta.", "Aceptar");
                                });
                            }
							else if (b.Exception.InnerExceptions[0].Message == SignInAccountResultEnum.NotExists.ToString())
                            {                        
                                Device.BeginInvokeOnMainThread(async () =>
                                {
									await Application.Current.MainPage.DisplayAlert("TuVotoCuenta", "El usuario no se encuentra registrado.", "Aceptar");
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
		}

		void ForgotPassword()
		{
			Application.Current.MainPage = new ForgotPasswordPage();
		}

		void SignUp()
		{
			Application.Current.MainPage = new SignUpPage();
		}

		bool ValidateInformation()
		{
			if (string.IsNullOrEmpty(Email))
				return false;
			if (string.IsNullOrEmpty(Password))
				return false;

			return true;
		}

		#region Commands

		public Command SignInCommand { get; set; }
		public Command ForgotPasswordCommand { get; set; }
		public Command SignUpCommand { get; set; }

		#endregion

		#region Binding attributes

		string email;
		public string Email
		{
			get { return email; }
			set { SetProperty(ref email, value); }
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