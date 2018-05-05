using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        INavigation navigation = null;

        public ProductViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Información del producto";
        }

        #region Commands

        public Command ProfileCommand { get; set; }

        #endregion

        #region Binding attributes

        #endregion
    }
}