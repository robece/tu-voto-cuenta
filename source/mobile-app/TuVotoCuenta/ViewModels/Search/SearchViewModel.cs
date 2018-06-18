using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Interfaces.Services;
using TuVotoCuenta.Pages.Search;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels.Search
{
    public enum ListOrder
    {
        MoreUpvotes,
        MoreDownVotes,
        MoreVotes
    }

    public class SearchViewModel : BaseViewModel
    {
        private Uri defaultPhoto = new Uri("http://via.placeholder.com/500x750");

        public Uri DefaultPhoto
        {
            get => defaultPhoto;
            set => SetProperty(ref defaultPhoto, value);
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

        private ObservableCollection<SearchResult> searchResults;

        public ObservableCollection<SearchResult> SearchResults
        {
            get => searchResults;
            set => SetProperty(ref searchResults, value);
        }

        private SearchResult detailResult;

        public SearchResult DetailResult
        {
            get => detailResult;
            set => SetProperty(ref detailResult, value);
        }

        public Command NextCommand
        {
            get;
            set;
        }

        private INavigation navigation;

        public SearchViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            NextCommand = new Command(async () => await NextAsync());
            Title = "Búsqueda de casilla";
            Catalogs.InitEntities();
            Entities = Catalogs.Entities;
        }

        private async Task NextAsync()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                {
                    var searchStep2Page = new SearchStep2Page(this);
                    await navigation.PushAsync(searchStep2Page);
                    IsBusy = false;
                }
            }
        }
    }
}
