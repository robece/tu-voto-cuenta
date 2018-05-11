using System.Threading.Tasks;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class Step2ViewModel : BaseViewModel
    {
        INavigation navigation = null;

        public Step2ViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Ubicación";
            
			NextCommand = new Command(async () => await Next());
        }

        #region Commands
        
		public Command NextCommand { get; set; }

        async Task Next()
        {
            await navigation.PushAsync(new Step3Page());
        }

        #endregion

        #region Binding attributes

		string locationDetails;
        public string LocationDetails
        {
            get { return locationDetails; }
            set { SetProperty(ref locationDetails, value); }
        }

        string state;
        public string State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
        }

        string city;
        public string City
        {
            get { return city; }
            set { SetProperty(ref city, value); }
        }

        string municipality;
        public string Municipality
        {
            get { return municipality; }
            set { SetProperty(ref municipality, value); }
        }

        string town;
        public string Town
        {
            get { return town; }
            set { SetProperty(ref town, value); }
        }

        #endregion
    }
}