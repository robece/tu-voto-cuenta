using System.Threading.Tasks;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class Step3ViewModel : BaseViewModel
    {
        INavigation navigation = null;
        
        public Step3ViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Votos";

			SaveChangesCommand = new Command(async () => await Save());
            NextCommand = new Command(async () => await Next());
        }

        #region Commands

		public Command SaveChangesCommand { get; set; }
        public Command NextCommand { get; set; }

        async Task Save()
        {
        }

        async Task Next()
        {
            await navigation.PushAsync(new Step4Page());
        }

        #endregion

        #region Binding attributes

		int partyPAN = -1;
        public int PartyPAN
        {
            get { return partyPAN; }
            set { SetProperty(ref partyPAN, value); }
        }

        int partyPRI = -1;
        public int PartyPRI
        {
            get { return partyPRI; }
            set { SetProperty(ref partyPRI, value); }
        }

        int partyPRD = -1;
        public int PartyPRD
        {
            get { return partyPRD; }
            set { SetProperty(ref partyPRD, value); }
        }

        int partyVerde = -1;
        public int PartyVerde
        {
            get { return partyVerde; }
            set { SetProperty(ref partyVerde, value); }
        }

        int partyPT = -1;
        public int PartyPT
        {
            get { return partyPT; }
            set { SetProperty(ref partyPT, value); }
        }

        int partyMC = -1;
        public int PartyMC
        {
            get { return partyMC; }
            set { SetProperty(ref partyMC, value); }
        }

        int partyNA = -1;
        public int PartyNA
        {
            get { return partyNA; }
            set { SetProperty(ref partyNA, value); }
        }

        int partyMOR = -1;
        public int PartyMOR
        {
            get { return partyMOR; }
            set { SetProperty(ref partyMOR, value); }
        }

        int partyES = -1;
        public int PartyES
        {
            get { return partyES; }
            set { SetProperty(ref partyES, value); }
        }

        int partyIND = -1;
        public int PartyIND
        {
            get { return partyIND; }
            set { SetProperty(ref partyIND, value); }
        }

        #endregion
    }
}