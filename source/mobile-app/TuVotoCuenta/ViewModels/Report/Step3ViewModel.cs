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
                PartyMorEs = item.PartyMORES;
                PartyMorPt = item.PartyMORPT;
                PartyPtEs = item.PartyPTES;
                PartyPtEsMor = item.PartyPTESMOR;
                PartyPrdMc = item.PartyPRDMC;
                PartyPrdPan = item.PartyPRDPAN;
                PartyPanMc = item.PartyPANMC;
                PartyPrdPanMc = item.PartyPRDPANMC;
                PartyPriVer = item.PartyPRIVER;
                PartyPriNa = item.PartyPRINA;
                PartyVerNa = item.PartyVERNA;
                PartyPriVerNa = item.PartyPRIVERNA;

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
            item.PartyPRD = PartyPRD;
            item.PartyVerde = PartyVerde;
            item.PartyPT = PartyPT;
            item.PartyMC = PartyMC;
            item.PartyNA = PartyNA;
            item.PartyMOR = PartyMOR;
            item.PartyES = PartyES;
            item.PartyINDJai = PartyINDJai;
            item.PartyOtro = PartyOtro;
            item.PartyMORES = PartyMorEs;
            item.PartyMORPT = PartyMorPt;
            item.PartyPTES = PartyPtEs;
            item.PartyPTESMOR = PartyPtEsMor;
            item.PartyPRDMC = PartyPrdMc;
            item.PartyPRDPAN = PartyPrdPan;
            item.PartyPANMC = PartyPanMc;
            item.PartyPRDPANMC = PartyPrdPanMc;
            item.PartyPRIVER = PartyPriVer;
            item.PartyPRINA = PartyPriNa;
            item.PartyVERNA = PartyVerNa;
            item.PartyPRIVERNA = PartyPriVerNa;

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
                Save();
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

        private int partyPriVerNa = -1;

        public int PartyPriVerNa
        {
            get => partyPriVerNa;
            set => SetProperty(ref partyPriVerNa, value);
        }

        private int partyPriVer = -1;

        public int PartyPriVer
        {
            get => partyPriVer;
            set => SetProperty(ref partyPriVer, value);
        }

        private int partyVerNa = -1;

        public int PartyVerNa
        {
            get => partyVerNa;
            set => SetProperty(ref partyVerNa, value);
        }

        private int partyPriNa = -1;

        public int PartyPriNa
        {
            get => partyPriNa;
            set => SetProperty(ref partyPriNa, value);
        }

        private int partyPrdPanMc = -1;

        public int PartyPrdPanMc
        {
            get => partyPrdPanMc;
            set => SetProperty(ref partyPrdPanMc, value);
        }

        private int partyPrdPan = -1;

        public int PartyPrdPan
        {
            get => partyPrdPan;
            set => SetProperty(ref partyPrdPan, value);
        }

        private int partyPrdMc = -1;

        public int PartyPrdMc
        {
            get => partyPrdMc;
            set => SetProperty(ref partyPrdMc, value);
        }

        private int partyPanMc = -1;

        public int PartyPanMc
        {
            get => partyPanMc;
            set => SetProperty(ref partyPanMc, value);
        }

        private int partyPtEsMor = -1;

        public int PartyPtEsMor
        {
            get => partyPtEsMor;
            set => SetProperty(ref partyPtEsMor, value);
        }

        private int partyPtEs = -1;

        public int PartyPtEs
        {
            get => partyPtEs;
            set => SetProperty(ref partyPtEs, value);
        }

        private int partyMorEs = -1;

        public int PartyMorEs
        {
            get => partyMorEs;
            set => SetProperty(ref partyMorEs, value);
        }

        private int partyMorPt = -1;

        public int PartyMorPt
        {
            get => partyMorPt;
            set => SetProperty(ref partyMorPt, value);
        }

        #endregion
    }
}