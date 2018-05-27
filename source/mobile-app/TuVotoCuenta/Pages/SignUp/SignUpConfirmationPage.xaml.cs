using TuVotoCuenta.Domain;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class SignUpConfirmationPage : ContentPage
    {
		public SignUpConfirmationPage(SignUpAccountRequest model)
        {
            InitializeComponent();
			BindingContext = new SignUpConfirmationViewModel(this.Navigation, model);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}