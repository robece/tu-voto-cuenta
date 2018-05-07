using TuVotoCuenta.Interfaces;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        INavigation navigation = null;

        public MenuViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            UserFullname = "TuVotoCuenta";
            UserPicture = "master.png";
            UserEmail = "mail@tuvotocuenta.com.mx";
        }

        #region Commands

        #endregion

        #region Binding attributes

        string userPicture;
        public string UserPicture
        {
            get { return userPicture; }
            set { SetProperty(ref userPicture, value); }
        }

        string userFullname;
        public string UserFullname
        {
            get { return userFullname; }
            set { SetProperty(ref userFullname, value); }
        }

        string userEmail;
        public string UserEmail
        {
            get { return userEmail; }
            set { SetProperty(ref userEmail, value); }
        }

        #endregion
    }
}