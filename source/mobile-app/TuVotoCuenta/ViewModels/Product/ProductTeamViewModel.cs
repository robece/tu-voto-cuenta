using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class ProductTeamViewModel : BaseViewModel
    {
        INavigation navigation = null;

        public ProductTeamViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "¿Quiénes Somos?";
        }

        #region Commands

        public Command ProfileCommand { get; set; }

        #endregion

        #region Binding attributes

        #endregion
    }
}