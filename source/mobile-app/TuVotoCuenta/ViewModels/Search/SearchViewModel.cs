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

        private List<String> entities = Catalogs.GetEntities();

        public List<String> Entities
        {
            get => entities;
            set => SetProperty(ref entities, value);
        }

        private List<String> municipalities;

        public List<String> Municipalities
        {
            get => municipalities;
            set => SetProperty(ref municipalities, value);
        }

        private List<String> localities;

        public List<String> Localities
        {
            get => localities;
            set => SetProperty(ref localities, value);
        }


        private string selectedEntity;
        public string SelectedEntity
        {
            get => selectedEntity;
            set => SetProperty(ref selectedEntity, value);
        }

        private string selectedEntityText;
        public string SelectedEntityText
        {
            get => selectedEntityText;
            set => SetProperty(ref selectedEntityText, value);
        }


        private int entitySelectedIndex;

        public int EntitySelectedIndex
        {
            get => entitySelectedIndex;
            set
            {
                if (value != -1)
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

        private string selectedMunicipality;
        public string SelectedMunicipality
        {
            get => selectedEntity;
            set => SetProperty(ref selectedMunicipality, value);
        }

        private string selectedMunicipalityText;
        public string SelectedMunicipalityText
        {
            get => selectedMunicipalityText;
            set => SetProperty(ref selectedMunicipalityText, value);
        }

        private int municipalitySelectedIndex;

        public int MunicipalitySelectedIndex
        {
            get => municipalitySelectedIndex;
            set
            {
                if (value != -1)
                {
                    municipalitySelectedIndex = value;

                    // trigger some action to take such as updating other labels or fields
                    OnPropertyChanged(nameof(MunicipalitySelectedIndex));

                    SelectedMunicipalityText = municipalities[municipalitySelectedIndex];
                    SelectedMunicipality = Catalogs.GetMunicipalityKey(SelectedMunicipalityText);

                    Catalogs.InitLocalities(SelectedMunicipality);
                    Localities = Catalogs.GetLocalities();
                }
            }
        }

        private int localitySelectedIndex;

        public int LocalitySelectedIndex
        {
            get => localitySelectedIndex;
            set
            {
                if (value != -1)
                {
                    localitySelectedIndex = value;

                    // trigger some action to take such as updating other labels or fields
                    OnPropertyChanged(nameof(LocalitySelectedIndex));

                    selectedLocalityText = localities[municipalitySelectedIndex];
                    selectedLocality = Catalogs.GetEntityKey(selectedLocalityText);
                }
            }
        }

        private string selectedLocality;
        public string SelectedLocality
        {
            get => selectedLocality;
            set => SetProperty(ref selectedLocality, value);
        }

        private string selectedLocalityText;
        public string SelectedLocalityText
        {
            get { return selectedLocalityText; }
            set { SetProperty(ref selectedLocalityText, value); }
        }


        private ObservableCollection<SearchResult> searchResults;

        public ObservableCollection<SearchResult> SearchResults
        {
            get => searchResults;
            set => SetProperty(ref searchResults, value);
        }


        private SearchResult selectedResult;

        public SearchResult SelectedResult
        {
            get => selectedResult;
            set 
            {
                SetProperty(ref selectedResult, value);
                if(selectedResult!=null)
                {
                    ResultDetailPage resultDetailPage = new ResultDetailPage();
                    navigation.PushAsync(resultDetailPage);
                    DetailResult = selectedResult;
                    resultDetailPage.BindingContext = this;
                    SelectedResult = null;
                }
            }
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
        }


        public async Task OrderResults(ListOrder listOrder)
        {
            switch (listOrder)
            {
                case ListOrder.MoreUpvotes:
                    SearchResults = new ObservableCollection<SearchResult>(SearchResults.OrderByDescending(result => result.UpVotes));
                    break;
                case ListOrder.MoreDownVotes:
                    SearchResults = new ObservableCollection<SearchResult>(SearchResults.OrderByDescending(result => result.DownVotes));
                    break;
                case ListOrder.MoreVotes:
                    SearchResults = new ObservableCollection<SearchResult>(SearchResults.OrderByDescending(result => result.DownVotes + result.UpVotes));
                    break;
                default:
                    break;
            }

        }

        private async Task NextAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    await SearchAsync();
                    var searchStep2Page = new SearchStep2Page();
                    await navigation.PushAsync(searchStep2Page);
                    searchStep2Page.BindingContext = this;
                    IsBusy = false;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private async Task SearchAsync()
        {
            ISearchService searchService;
#if DEBUG
            if ((bool)App.Current.Resources["UseMock"])
                searchService = new Services.MockServices.SearchMockService();
            else
                searchService = new Services.RestApi.SearchService();
#else
            searchService = new Services.RestApi.SearchService();
#endif
            SearchResults = new ObservableCollection<SearchResult>(await searchService.SearchAsync(selectedEntity, selectedMunicipality, selectedLocality));

        }
    }
}
