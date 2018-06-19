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

            SignInCommand = new Command(async () => await SignIn(), () => ValidateInformation());
            SignUpCommand = new Command(() => SignUp());
        }

        async Task SignIn()
        {

            try
            {
#if DEBUG

                if ((bool)Application.Current.Resources["LoginOk"])
                {
                    Application.Current.MainPage = new MasterPage() { IsPresented = true };
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

                    SignInAccountRequest model = new SignInAccountRequest() { username = Username, password = Password };
                    SignInAccountResponse response = await RestHelper.SignInAccountAsync(model);
                  
                    if (response.Status != ResponseStatus.Ok)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                    }
                    else
                    {
                        Settings.Profile_Username = response.Username;
                        Settings.Profile_Picture = $"{Settings.ImageStorageUrl}/{Settings.AccountImageStorageUrl}/{response.Image}";
                        IsBusy = false;
                        await Task.Delay(10);
                        Application.Current.MainPage = new MasterPage() { IsPresented = true };
                    }
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                Microsoft.AppCenter.Crashes.Crashes.TrackError(ex);
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un error inesperado", "Aceptar");
            }
        }

        void SignUp()
        {
            Application.Current.MainPage = new SignUpPage();
        }

        bool ValidateInformation()
        {
            if (Helpers.ValidationHelper.ValidateString(Helpers.ValidationType.UserName, Username) != Helpers.ValidationResult.IsValid)
                return false;
            if (Helpers.ValidationHelper.ValidateString(Helpers.ValidationType.Password, Password) != Helpers.ValidationResult.IsValid)
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
            set { SetProperty(ref username, value); SignInCommand.ChangeCanExecute(); }
        }

        string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); SignInCommand.ChangeCanExecute(); }
        }
        #endregion
    }
}