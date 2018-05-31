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
			Device.BeginInvokeOnMainThread(() => {
				CleanCurrentSession();
				Application.Current.MainPage = new SignUpPage();
			});
        }   

        public static void CleanCurrentSession()
		{
			Settings.Profile_Username = string.Empty;
            Settings.Profile_Account = string.Empty;
            Settings.Profile_Picture = string.Empty;
			Settings.CurrentRecordItem = string.Empty;         
		}
    }
}