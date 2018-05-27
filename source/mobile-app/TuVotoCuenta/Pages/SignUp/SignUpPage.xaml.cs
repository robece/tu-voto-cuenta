using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class SignUpPage : ContentPage
    {
		public SignUpPage()
        {
            InitializeComponent();
            BindingContext = new SignUpViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
