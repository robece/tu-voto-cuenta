using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class SignOutPage : ContentPage
    {
		public SignOutPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
			CleanCurrentSession();
			Device.BeginInvokeOnMainThread(() => { 
				Application.Current.MainPage = new SignUpPage();
			});
        }   

        public static void CleanCurrentSession()
		{
			Settings.UserAccount = string.Empty;
            Settings.UserEmail = string.Empty;
            Settings.UserFullname = string.Empty;
            Settings.UserPicture = string.Empty;
			Settings.CurrentRecordItem = string.Empty;         
		}
    }
}