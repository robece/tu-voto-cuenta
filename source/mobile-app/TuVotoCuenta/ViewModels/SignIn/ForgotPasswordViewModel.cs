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
	public class ForgotPasswordViewModel : BaseViewModel
	{
		INavigation navigation = null;
		public static bool EmailValidation = false;
		public ObservableCollection<Slide> Slides { get; set; }

		public ForgotPasswordViewModel(INavigation navigation)
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
			ForgotPasswordCommand = new Command(() => ForgotPassword());
			ReturnCommand = new Command(() => Return());
		}

		async void ForgotPassword()
		{
			if (!EmailValidation)
				await Application.Current.MainPage.DisplayAlert("Aviso", "Verifica tu correo electrónico", "Aceptar");
			else
			{
				IsBusy = true;
				MessageTitle = "Por favor aguarda, te estamos enviando un correo electrónico...";
				MessageSubTitle = string.Empty;
				await Task.Run(async () =>
				{
					ForgotPasswordAccountResponse response = await RestHelper.ForgotPasswordAccountAsync(new ForgotPasswordAccountRequest() { email= Email });

					if (response == null)
					{
						throw new AggregateException(ForgotPasswordAccountResultEnum.Failed.ToString());
					}
					else
					{
						if (!response.IsSucceded)
						{
							if (response.ResultId == (int)ForgotPasswordAccountResultEnum.Failed)
                            {
								throw new AggregateException(ForgotPasswordAccountResultEnum.Failed.ToString());
                            }
							else if (response.ResultId == (int)ForgotPasswordAccountResultEnum.NotExists)
                            {
								throw new AggregateException(ForgotPasswordAccountResultEnum.NotExists.ToString());
                            }
                            else
                            {
								throw new AggregateException(ForgotPasswordAccountResultEnum.Failed.ToString());
                            }
						}
					}               
				}).ContinueWith((b) =>
				{
					if (b.Exception != null)
                    {
						if (b.Exception.InnerExceptions[0].Message == ForgotPasswordAccountResultEnum.Failed.ToString())
                        {
                            MessageTitle = "Se presentó un problema al realizar la recuperación de contraseña.";
                        }
						else if (b.Exception.InnerExceptions[0].Message == ForgotPasswordAccountResultEnum.NotExists.ToString())
                        {
							MessageTitle = "El correo electrónico no es válido, verifica que tu correo electrónico sea el mismo con el que te diste de alta.";
                        }

                        IsBusy = false;
                        MessageSubTitle = "El proceso de recuperación de contraseña no fue satisfactorio.";
                    }
					else
					{
						IsBusy = false;
                        MessageTitle = $"Listo!";
						MessageSubTitle = "Revisa tu bandeja de correo, ya debes tener un correo de nuestro soporte automático.";
					}               
				});
			}
		}

		void Return()
		{
			Application.Current.MainPage = new SignInPage();
		}

		#region Commands

		public Command ForgotPasswordCommand { get; set; }
		public Command ReturnCommand { get; set; }

		#endregion

		#region Binding attributes

		string email;
        public string Email
        {
			get { return email; }
			set { SetProperty(ref email, value); }
        }

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

		#endregion
	}
}