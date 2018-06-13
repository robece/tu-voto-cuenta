using System;
using System.Collections.Generic;
using System.Linq;
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

            Catalogs.InitEntities();
            Entities = Catalogs.Entities;

            if (!String.IsNullOrEmpty(Settings.CurrentRecordItem))
            {
                RecordItem item = JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem);
                LocationDetails = item.LocationDetails;

                if (item.Entity != null)
                {
                    SelectedEntity = Catalogs.GetEntityValue(item.Entity);

                }

                if (item.Municipality != null)
                {
                    SelectedMunicipality = Catalogs.GetMunicipalityValue(item.Municipality);
                }

                if (item.Locality != null)
                {
                    SelectedLocality = Catalogs.GetLocalityValue(item.Locality);
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
            item.Entity = SelectedEntity?.EntityName;
            item.Municipality = SelectedMunicipality?.MunicipalityName;
            item.Locality = SelectedLocality?.LocalityName;

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


        #region Catalogs

        private List<Entity> entities;

        public List<Entity> Entities
        {
            get => entities;
            set
            {
                if (entities != value && value != null)
                {
                    SetProperty(ref entities, value);
                    SelectedEntity = Catalogs.Entities.First();
                    Catalogs.InitMunicipalities(selectedEntity.EntityId);
                    Municipalities = Catalogs.Municipalities;
                    SelectedMunicipality = Municipalities.First();
                }
            }
        }


        private List<Municipality> municipalities;

        public List<Municipality> Municipalities
        {
            get => municipalities;
            set
            {
                SetProperty(ref municipalities, value);
            }
        }


        private List<Locality> localities;

        public List<Locality> Localities
        {
            get => localities;
            set => SetProperty(ref localities, value);
        }


        private Municipality selectedMunicipality;

        public Municipality SelectedMunicipality
        {
            get => selectedMunicipality;
            set
            {
                if (selectedMunicipality != value && value != null)
                {
                    SetProperty(ref selectedMunicipality, value);
                    Catalogs.InitLocalities(selectedEntity.EntityId, selectedMunicipality.MunicipalityId);
                    Localities = Catalogs.Localities;
                    SelectedLocality = Localities.First();
                }
            }
        }

        private Entity selectedEntity;

        public Entity SelectedEntity
        {
            get => selectedEntity;
            set
            {
                if (selectedEntity != value && value != null)
                {
                    SetProperty(ref selectedEntity, value);
                    Catalogs.InitMunicipalities(selectedEntity.EntityId);
                    Municipalities = Catalogs.Municipalities;
                    SelectedMunicipality = Municipalities.First();

                }
            }
        }


        private Locality selectedLocality;

        public Locality SelectedLocality
        {
            get => selectedLocality;
            set
            {
                if (value != null)
                    SetProperty(ref selectedLocality, value);
            }
        }


        #endregion

        #endregion

    }
}