using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TuVotoCuenta.Domain;
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

			if (!String.IsNullOrEmpty(Settings.CurrentRecordItem))
			{
				RecordItem item = JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem);
				LocationDetails = item.LocationDetails;

				if(item.Entity != null)
				{
					var EntityText = Catalogs.GetEntityValue(item.Entity);
					EntitySelectedIndex = Entities.IndexOf(EntityText);
				}

				if (item.Municipality != null)
				{
					var MunicipalityText = Catalogs.GetMunicipalityValue(item.Municipality);
					MunicipalitySelectedIndex = (MunicipalityText == string.Empty) ? 0 : Municipalities.IndexOf(MunicipalityText);
				}

				if (item.Locality != null)
                {
                    var LocalityText = Catalogs.GetLocalityValue(item.Locality);
					LocalitySelectedIndex = (LocalityText == string.Empty) ? 0 : Localities.IndexOf(LocalityText);
                }
			}

			NextCommand = new Command(async () => await Next());
		}

		bool ValidateInformation()
		{
			if (String.IsNullOrEmpty(LocationDetails))
				return false;
			return true;
		}

		public void Save()
		{
			RecordItem item = JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem);

			item.LocationDetails = LocationDetails;
			item.Entity = SelectedEntity;
			item.Municipality = SelectedMunicipality;
			item.Locality = SelectedLocality;

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
				await navigation.PushAsync(new Step3Page());
			}
		}

		#endregion

		#region Binding attributes

		string locationDetails;
		public string LocationDetails
		{
			get { return locationDetails; }
			set { SetProperty(ref locationDetails, value); }
		}
        
		#region entity
        
		List<string> entities = Catalogs.GetEntities();
		public List<string> Entities => entities;

        private string selectedEntity;
		public string SelectedEntity
        {
			get { return selectedEntity; }
			set { SetProperty(ref selectedEntity, value); }
        }

		private string selectedEntityText;
		public string SelectedEntityText
        {
			get { return selectedEntityText; }
			set { SetProperty(ref selectedEntityText, value); }
        }

		int entitySelectedIndex;
		public int EntitySelectedIndex
        {
            get
            {
				return entitySelectedIndex;
            }
            set
            {
				if(value != -1) 
				{
					entitySelectedIndex = value;

                    // trigger some action to take such as updating other labels or fields
                    OnPropertyChanged(nameof(EntitySelectedIndex));

                    SelectedEntityText = entities[EntitySelectedIndex];
                    SelectedEntity = Catalogs.GetEntityKey(SelectedEntityText);

                    Catalogs.InitMunicipalities(SelectedEntity);
                    Municipalities = Catalogs.GetMunicipalities();
                    
					Catalogs.InitLocalities(SelectedMunicipality);
                    Localities = Catalogs.GetLocalities();               
				}            
            }
        }

		#endregion

		#region municipality

		List<string> municipalities;
		public List<string> Municipalities
        {
			get { return municipalities; }
			set { SetProperty(ref municipalities, value); }
        }

        private string selectedMunicipality;
		public string SelectedMunicipality
        {
			get { return selectedMunicipality; }
			set { SetProperty(ref selectedMunicipality, value); }
        }

		private string selectedMunicipalityText;
		public string SelectedMunicipalityText
        {
			get { return selectedMunicipalityText; }
			set { SetProperty(ref selectedMunicipalityText, value); }
        }

		int municipalitySelectedIndex;
		public int MunicipalitySelectedIndex
        {
            get
            {
				return municipalitySelectedIndex;
            }
            set
            {
                if (value != -1)
				{
					municipalitySelectedIndex = value;

                    // trigger some action to take such as updating other labels or fields
                    OnPropertyChanged(nameof(MunicipalitySelectedIndex));

                    SelectedMunicipalityText = Municipalities[MunicipalitySelectedIndex];
                    SelectedMunicipality = Catalogs.GetMunicipalityKey(SelectedMunicipalityText);

					Catalogs.InitLocalities(SelectedMunicipality);
                    Localities = Catalogs.GetLocalities();
				}
            }
        }

        #endregion

		#region locality

        List<string> localities;
        public List<string> Localities
        {
			get { return localities; }
			set { SetProperty(ref localities, value); }
        }

        private string selectedLocality;
		public string SelectedLocality
        {
			get { return selectedLocality; }
			set { SetProperty(ref selectedLocality, value); }
        }

		private string selectedLocalityText;
		public string SelectedLocalityText
        {
			get { return selectedLocalityText; }
			set { SetProperty(ref selectedLocalityText, value); }
        }

		int localitySelectedIndex;
		public int LocalitySelectedIndex
        {
            get
            {
                return localitySelectedIndex;
            }
            set
            {
                if (value != -1)
                {
                    localitySelectedIndex = value;

                    // trigger some action to take such as updating other labels or fields
					OnPropertyChanged(nameof(LocalitySelectedIndex));

					SelectedLocalityText = Localities[LocalitySelectedIndex];
					SelectedLocality = Catalogs.GetLocalityKey(SelectedLocalityText);
                }
            }
        }

        #endregion

		#endregion
	}
}