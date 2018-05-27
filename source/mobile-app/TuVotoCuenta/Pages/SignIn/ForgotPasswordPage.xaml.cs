using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class ForgotPasswordPage : ContentPage
    {
		public ForgotPasswordPage()
        {
            InitializeComponent();
            BindingContext = new ForgotPasswordViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}