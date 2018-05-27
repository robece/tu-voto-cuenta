using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class SignInPage : ContentPage
    {
		public SignInPage()
        {
            InitializeComponent();
			BindingContext = new SignInViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();          
        }
    }
}