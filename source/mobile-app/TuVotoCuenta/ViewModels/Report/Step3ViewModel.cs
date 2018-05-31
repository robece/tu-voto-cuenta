using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TuVotoCuenta.Domain;
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
            Title = "Candidatos";
            
			if (!String.IsNullOrEmpty(Settings.CurrentRecordItem))
            {
                RecordItem item = JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem);
				PartyPAN = item.PartyPAN;
				PartyPRI = item.PartyPRI;
				PartyPRD = item.PartyPRD;
				PartyVerde = item.PartyVerde;
				PartyPT = item.PartyPT;
				PartyMC = item.PartyMC;
				PartyNA = item.PartyNA;
				PartyMOR = item.PartyMOR;
				PartyES = item.PartyES;
				PartyINDJai = item.PartyINDJai;
				PartyOtro = item.PartyOtro;
            }

            NextCommand = new Command(async () => await Next());
        }

		bool ValidateInformation()
        {
            return true;
        }

		public void Save()
        {
            RecordItem item = JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem);

			item.PartyPAN = PartyPAN;
			item.PartyPRI = PartyPRI;
			item.PartyPRD= PartyPRD;
			item.PartyVerde = PartyVerde;
			item.PartyPT = PartyPT;
			item.PartyMC = PartyMC;
			item.PartyNA = PartyNA;
			item.PartyMOR = PartyMOR;
			item.PartyES = PartyES;
			item.PartyINDJai = PartyINDJai;
			item.PartyOtro = PartyOtro;

            Settings.CurrentRecordItem = JsonConvert.SerializeObject(item);
        }

        #region Commands
        
        public Command NextCommand { get; set; }

		async Task Next()
        {
            if (!ValidateInformation())
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresa la información correcta.", "Aceptar");
            else if (!IsBusy)
            {
                await navigation.PushAsync(new Step4Page());
            }
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

        int partyINDMar = -1;
        public int PartyINDMar
        {
            get { return partyINDMar; }
            set { SetProperty(ref partyINDMar, value); }
        }

		int partyINDJai = -1;
        public int PartyINDJai
        {
            get { return partyINDJai; }
            set { SetProperty(ref partyINDJai, value); }
        }

		int partyOtro = -1;
        public int PartyOtro
        {
			get { return partyOtro; }
            set { SetProperty(ref partyOtro, value); }
        }

        #endregion
    }
}