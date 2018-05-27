using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        INavigation navigation = null;

        public WelcomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Inicio";
        }

        #region Commands

        #endregion

        #region Binding attributes

        string userShortname;
        public string UserShortname
        {
            get { return userShortname; }
            set { SetProperty(ref userShortname, value); }
        }

        #endregion
    }
}