using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Plugin.DeviceInfo;
using TuVotoCuenta.Domain;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class AddReportStep1ViewModel : BaseViewModel
    {
        INavigation navigation = null;

        public AddReportStep1ViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Registro de casilla";

            Task.Run(() =>
            {
                using(var sha256 = SHA256.Create())  
                {
                    var dh = sha256.ComputeHash(Encoding.UTF8.GetBytes(CrossDeviceInfo.Current.Id));
                    DeviceHash = System.BitConverter.ToString(dh).Replace("-", "").ToLower();  
                }  
            });           
        }

        #region Commands

        #endregion

        #region Binding attributes

		int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        string boxNumber;
        public string BoxNumber
        {
            get { return boxNumber; }
            set { SetProperty(ref boxNumber, value); }
        }

        string boxSection;
        public string BoxSection
        {
            get { return boxSection; }
            set { SetProperty(ref boxSection, value); }
        }
              
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

		string photoTimestamp;
        public string PhotoTimestamp
        {
            get { return photoTimestamp; }
            set { SetProperty(ref photoTimestamp, value); }
        }

        string deviceHash;
        public string DeviceHash
        {
            get { return deviceHash; }
            set { SetProperty(ref deviceHash, value); }
        }
        
        /* Votes */

		List<string> votingValues = Catalogs.GetVotingValues();
		public List<string> VotingValues => votingValues;

        /* PAN */
         
        string selectedPANVote;
		public string SelectedPANVote
        {
			get { return selectedPANVote; }
			set { SetProperty(ref selectedPANVote, value); }
        }

        string selectedPANVoteText;
		public string SelectedPANVoteText
        {
			get { return selectedPANVoteText; }
			set { SetProperty(ref selectedPANVoteText, value); }
        }

        int panVotesSelectedIndex;
        public int PANVotesSelectedIndex
		{
			get
			{
				return panVotesSelectedIndex;
			}
			set
			{
				panVotesSelectedIndex = value;

				// trigger some action to take such as updating other labels or fields
				OnPropertyChanged(nameof(PANVotesSelectedIndex));

				SelectedPANVoteText = votingValues[PANVotesSelectedIndex];
				SelectedPANVote = Catalogs.GetVotingValueKey(SelectedPANVoteText);
			}
		}

		/* PRI */

        string selectedPRIVote;
        public string SelectedPRIVote
        {
            get { return selectedPRIVote; }
            set { SetProperty(ref selectedPRIVote, value); }
        }

        string selectedPRIVoteText;
        public string SelectedPRIVoteText
        {
            get { return selectedPRIVoteText; }
            set { SetProperty(ref selectedPRIVoteText, value); }
        }

        int priVotesSelectedIndex;
        public int PRIVotesSelectedIndex
        {
            get
            {
                return priVotesSelectedIndex;
            }
            set
            {
                priVotesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(PRIVotesSelectedIndex));
                
                SelectedPRIVoteText = votingValues[PRIVotesSelectedIndex];
                SelectedPRIVote = Catalogs.GetVotingValueKey(SelectedPRIVoteText);
            }
        }

		/* PRD */

		string selectedPRDVote;
        public string SelectedPRDVote
        {
            get { return selectedPRDVote; }
            set { SetProperty(ref selectedPRDVote, value); }
        }

        string selectedPRDVoteText;
        public string SelectedPRDVoteText
        {
            get { return selectedPRDVoteText; }
            set { SetProperty(ref selectedPRDVoteText, value); }
        }

        int prdVotesSelectedIndex;
        public int PRDVotesSelectedIndex
        {
            get
            {
                return prdVotesSelectedIndex;
            }
            set
            {
                prdVotesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(PRDVotesSelectedIndex));

                SelectedPRDVoteText = votingValues[PRDVotesSelectedIndex];
                SelectedPRDVote = Catalogs.GetVotingValueKey(SelectedPRDVoteText);
            }
        }

        /* Verde */

		string selectedVerdeVote;
        public string SelectedVerdeVote
        {
            get { return selectedVerdeVote; }
            set { SetProperty(ref selectedVerdeVote, value); }
        }

        string selectedVerdeVoteText;
        public string SelectedVerdeVoteText
        {
            get { return selectedVerdeVoteText; }
            set { SetProperty(ref selectedVerdeVoteText, value); }
        }

        int verdeVotesSelectedIndex;
        public int VerdeVotesSelectedIndex
        {
            get
            {
                return verdeVotesSelectedIndex;
            }
            set
            {
                verdeVotesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(VerdeVotesSelectedIndex));

                SelectedVerdeVoteText = votingValues[VerdeVotesSelectedIndex];
                SelectedVerdeVote = Catalogs.GetVotingValueKey(SelectedVerdeVoteText);
            }
        }

        /* PT */

		string selectedPTVote;
        public string SelectedPTVote
        {
            get { return selectedPTVote; }
            set { SetProperty(ref selectedPTVote, value); }
        }

        string selectedPTVoteText;
        public string SelectedPTVoteText
        {
            get { return selectedPTVoteText; }
            set { SetProperty(ref selectedPTVoteText, value); }
        }

        int ptVotesSelectedIndex;
        public int PTVotesSelectedIndex
        {
            get
            {
                return ptVotesSelectedIndex;
            }
            set
            {
                ptVotesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(PTVotesSelectedIndex));

                SelectedPTVoteText = votingValues[PTVotesSelectedIndex];
                SelectedPTVote = Catalogs.GetVotingValueKey(SelectedPTVoteText);
            }
        }

        /* Movimiento Ciudadano */

		string selectedMCVote;
        public string SelectedMCVote
        {
            get { return selectedMCVote; }
            set { SetProperty(ref selectedMCVote, value); }
        }

        string selectedMCVoteText;
        public string SelectedMCVoteText
        {
            get { return selectedMCVoteText; }
            set { SetProperty(ref selectedMCVoteText, value); }
        }

        int mcVotesSelectedIndex;
        public int MCVotesSelectedIndex
        {
            get
            {
                return mcVotesSelectedIndex;
            }
            set
            {
                mcVotesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(MCVotesSelectedIndex));

                SelectedMCVoteText = votingValues[MCVotesSelectedIndex];
                SelectedMCVote = Catalogs.GetVotingValueKey(SelectedMCVoteText);
            }
        }

        /* Nueva Alianza */

		string selectedNAVote;
        public string SelectedNAVote
        {
            get { return selectedNAVote; }
            set { SetProperty(ref selectedNAVote, value); }
        }

        string selectedNAVoteText;
        public string SelectedNAVoteText
        {
            get { return selectedNAVoteText; }
            set { SetProperty(ref selectedNAVoteText, value); }
        }

        int naVotesSelectedIndex;
        public int NAVotesSelectedIndex
        {
            get
            {
                return naVotesSelectedIndex;
            }
            set
            {
                naVotesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(NAVotesSelectedIndex));

                SelectedNAVoteText = votingValues[NAVotesSelectedIndex];
                SelectedNAVote = Catalogs.GetVotingValueKey(SelectedNAVoteText);
            }
        }

	    /* Morena */

		string selectedMORVote;
        public string SelectedMORVote
        {
            get { return selectedMORVote; }
            set { SetProperty(ref selectedMORVote, value); }
        }

        string selectedMORVoteText;
        public string SelectedMORVoteText
        {
            get { return selectedMORVoteText; }
            set { SetProperty(ref selectedMORVoteText, value); }
        }

        int morVotesSelectedIndex;
        public int MORVotesSelectedIndex
        {
            get
            {
                return morVotesSelectedIndex;
            }
            set
            {
                morVotesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(MORVotesSelectedIndex));

                SelectedMORVoteText = votingValues[MORVotesSelectedIndex];
                SelectedMORVote = Catalogs.GetVotingValueKey(SelectedMORVoteText);
            }
        }

        /* Encuentro Social */

		string selectedESVote;
        public string SelectedESVote
        {
            get { return selectedESVote; }
            set { SetProperty(ref selectedESVote, value); }
        }

        string selectedESVoteText;
        public string SelectedESVoteText
        {
            get { return selectedESVoteText; }
            set { SetProperty(ref selectedESVoteText, value); }
        }

        int esVotesSelectedIndex;
        public int ESVotesSelectedIndex
        {
            get
            {
                return esVotesSelectedIndex;
            }
            set
            {
                esVotesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(ESVotesSelectedIndex));

                SelectedESVoteText = votingValues[ESVotesSelectedIndex];
                SelectedESVote = Catalogs.GetVotingValueKey(SelectedESVoteText);
            }
        }

        /* Independiente */
        
		string selectedINDVote;
        public string SelectedINDVote
        {
            get { return selectedINDVote; }
            set { SetProperty(ref selectedINDVote, value); }
        }

        string selectedINDVoteText;
        public string SelectedINDVoteText
        {
            get { return selectedINDVoteText; }
            set { SetProperty(ref selectedINDVoteText, value); }
        }

        int indVotesSelectedIndex;
        public int INDVotesSelectedIndex
        {
            get
            {
                return indVotesSelectedIndex;
            }
            set
            {
                indVotesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(INDVotesSelectedIndex));

                SelectedINDVoteText = votingValues[INDVotesSelectedIndex];
                SelectedINDVote = Catalogs.GetVotingValueKey(SelectedINDVoteText);
            }
        }

        #endregion
    }
}