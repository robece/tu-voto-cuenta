using System.Collections.ObjectModel;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
	public class SignUpViewModel : BaseViewModel
	{
		INavigation navigation = null;
		public ObservableCollection<Slide> Slides { get; set; }

		public SignUpViewModel(INavigation navigation)
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

			SignUpCommand = new Command(() => SignUp());
			SignInCommand = new Command(() => SignIn());
		}

		async void SignUp()
		{
			if (!ValidatePasswordMatch())
			{
				await Application.Current.MainPage.DisplayAlert("Aviso", "Verifica que la contraseña sea la misma.", "Aceptar");
			}
			else
			{
				if (!ValidateInformation())
					await Application.Current.MainPage.DisplayAlert("Aviso", "Verifica que todos los campos se encuentren completos.", "Aceptar");
				else if (!IsBusy)
				{
					await Application.Current.MainPage.DisplayAlert("Aviso", "TuVotoCuenta se apoya en el uso de una cadena de bloques (blockchain) donde se responsabiliza a cada usuario por sus claves, nosotros no tenemos acceso a tu contraseña y no podemos ayudarte restablecerla por lo que es importante que la guardes y no la pierdas u olvides, anótala en un lugar seguro ya que la necesitarás para validar el envio de datos que hagas a la blockchain, recuerda que sólo tú podrás acceder con ese usuario y dar autenticidad a la información que envíes, si pierdes la contraseña tendrás que crear un usuario y contraseña nueva, los anteriores quedarán inutilizados.", "Aceptar");
					IsBusy = true;
					SignUpAccountRequest model = new SignUpAccountRequest() { username = Username, password = Password };
					Application.Current.MainPage = new LegalConcernsPage(model);
					IsBusy = false;
				}
			}
		}

		void SignIn()
		{
			Application.Current.MainPage = new SignInPage();
		}

		bool ValidatePasswordMatch()
		{
			if (Password != ConfirmPassword)
				return false;

			return true;
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

		public Command SignUpCommand { get; set; }
		public Command SignInCommand { get; set; }

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

		string confirmPassword;
		public string ConfirmPassword
		{
			get { return confirmPassword; }
			set { SetProperty(ref confirmPassword, value); }
		}

		#endregion
	}
}