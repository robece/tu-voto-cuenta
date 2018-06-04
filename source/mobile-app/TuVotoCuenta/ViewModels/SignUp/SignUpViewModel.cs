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

            SignUpCommand = new Command(() => SignUp(), () => ValidateInformation() && ValidatePasswordMatch());
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
					await Application.Current.MainPage.DisplayAlert("Aviso", "Importante: dado que no recopilamos información como el correo electrónico no podemos ayudarte restablecer tu contraseña en caso de extravío, por lo que es importante que guardes bien tu nombre de usuario y contraseña, anótalos en un lugar seguro ya que los necesitarás cada que desees iniciar sesión desde un dispositivo móvil.", "Aceptar");
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
            if (Helpers.ValidationHelper.ValidateString(Helpers.ValidationType.UserName, Username)!= Helpers.ValidationResult.IsValid)
				return false;
            if (Helpers.ValidationHelper.ValidateString(Helpers.ValidationType.Password, Password) != Helpers.ValidationResult.IsValid)
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
            set { SetProperty(ref username, value); SignUpCommand.ChangeCanExecute(); }
        }

        string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); SignUpCommand.ChangeCanExecute(); }
        }

        string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { SetProperty(ref confirmPassword, value); SignUpCommand.ChangeCanExecute(); }
        }

        #endregion
    }
}