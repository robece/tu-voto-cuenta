using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class AccountPage : ContentPage
    {
		public AccountPage()
        {
            InitializeComponent();
            BindingContext = new AccountViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}