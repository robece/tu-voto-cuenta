using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        INavigation navigation = null;

        public SettingsViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Configuraciones";
        }

        #region Commands

        #endregion

        #region Binding attributes

        #endregion
    }
}