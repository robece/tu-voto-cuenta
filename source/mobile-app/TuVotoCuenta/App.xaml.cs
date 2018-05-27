using TuVotoCuenta.Domain;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta
{
    public partial class App : Application
    {
		public static double ScreenWidth;
		public static double ScreenHeight;
        
		public static double Latitude;
        public static double Longitude;

		public App()
        {
            InitializeComponent();
	        
			//set startup app configuration
			Settings.FunctionURL = "https://{functionURL}.azurewebsites.net";
			Settings.ImageStorageUrl = "https://{imagestorageurl}.blob.core.windows.net/accountimages/";

			//init catalogs
            Catalogs.InitEntities();

			//if logged in redirect to main page
            if (Settings.UserAccount != string.Empty)
                Application.Current.MainPage = new MasterPage();
            else
                MainPage = new SignUpPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}