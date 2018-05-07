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
            MainPage = new MasterPage();

			//init catalogs
            Catalogs.InitVotingValues();
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