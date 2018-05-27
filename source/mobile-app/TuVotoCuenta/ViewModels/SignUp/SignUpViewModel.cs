using System.Collections.ObjectModel;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        INavigation navigation = null;
        public static bool EmailValidation = false;
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
                new Slide { ImageUrl ="slide1.png", Name = "" }
            };

            SignUpCommand = new Command(() => SignUp());
            SignInCommand = new Command(() => SignIn());
        }

        async void SignUp()
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
					SignUpAccountRequest model = new SignUpAccountRequest() { fullname = Fullname, email = Email, password = Password };
                    Application.Current.MainPage = new LegalConcernsPage(model);
                    IsBusy = false;
                }
            }
        }

        void SignIn()
        {
            Application.Current.MainPage = new SignInPage();
        }

        bool ValidateInformation()
        {
            if (string.IsNullOrEmpty(Fullname))
                return false;
            if (string.IsNullOrEmpty(Email))
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

        string fullname;
        public string Fullname
        {
            get { return fullname; }
            set { SetProperty(ref fullname, value); }
        }

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