using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Pages.Search;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels.Search
{
    public class SearchResultsViewModel : BaseViewModel
    {
        INavigation navigation = null;
        SearchViewModel searchViewModel;

        public SearchResultsViewModel(INavigation navigation, SearchViewModel searchViewModel)
        {
            Title = "Resultados";
            this.navigation = navigation;
            this.searchViewModel = searchViewModel;
            IsContinueGoBackEnabled = true;
            Locality = searchViewModel.SelectedLocality.LocalityName;
            Entity = searchViewModel.SelectedEntity.EntityName;
            Municipality = searchViewModel.SelectedMunicipality.MunicipalityName;

            Task.Run(async () => { await SearchAsync(); });

        }

        private ObservableCollection<RecordItem> records;

        public ObservableCollection<RecordItem> Records
        {
            get => records;
            set => SetProperty(ref records, value);
        }

        string messageTitle;
        public string MessageTitle
        {
            get { return messageTitle; }
            set { SetProperty(ref messageTitle, value); }
        }

        string messageSubTitle;
        public string MessageSubTitle
        {
            get { return messageSubTitle; }
            set { SetProperty(ref messageSubTitle, value); }
        }

        bool isContinueEnabled;
        public bool IsContinueEnabled
        {
            get { return isContinueEnabled; }
            set { SetProperty(ref isContinueEnabled, value); }
        }

        bool isContinueGoBackEnabled;
        public bool IsContinueGoBackEnabled
        {
            get { return isContinueGoBackEnabled; }
            set { SetProperty(ref isContinueGoBackEnabled, value); }
        }

        private string entity;

        public string Entity
        {
            get => entity;
            set => SetProperty(ref entity, value);
        }

        private string municipality;

        public string Municipality
        {
            get => municipality;
            set => SetProperty(ref municipality, value);
        }

        private string locality;

        public string Locality
        {
            get => locality;
            set => SetProperty(ref locality, value);
        }

        private RecordItem selectedResult;

        public RecordItem SelectedResult
        {
            get => selectedResult;
            set
            {
                SetProperty(ref selectedResult, value);
                if (selectedResult != null)
                {
                    ResultDetailPage resultDetailPage = new ResultDetailPage(selectedResult);
                    navigation.PushAsync(resultDetailPage);
                    SelectedResult = null;
                }
            }
        }

        public void OrderResults(ListOrder listOrder)
        {
            switch (listOrder)
            {
                case ListOrder.MoreUpvotes:
                    //SearchResults = new ObservableCollection<SearchResult>(SearchResults.OrderByDescending(result => result.UpVotes));
                    break;
                case ListOrder.MoreDownVotes:
                    //SearchResults = new ObservableCollection<SearchResult>(SearchResults.OrderByDescending(result => result.DownVotes));
                    break;
                case ListOrder.MoreVotes:
                    //SearchResults = new ObservableCollection<SearchResult>(SearchResults.OrderByDescending(result => result.DownVotes + result.UpVotes));
                    break;
                default:
                    break;
            }
        }

        private async Task SearchAsync()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                MessageTitle = "Buscando...";
                MessageSubTitle = "Espera un momento, el proceso puede tomar unos segundos.";

                GetRecordItemListRequest recordItemListRequest = new GetRecordItemListRequest();
                recordItemListRequest.entity = searchViewModel.SelectedEntity.EntityName;
                recordItemListRequest.locality = searchViewModel.SelectedLocality.LocalityName;
                recordItemListRequest.municipality = searchViewModel.SelectedMunicipality.MunicipalityName;
                var result = await Helpers.RestHelper.GetRecordListAsync(recordItemListRequest);

                if (result.Status != Enums.ResponseStatus.Ok)
                {
                    messageSubTitle = result.Message;
                    isContinueGoBackEnabled = true;
                }
                else
                {
                    if (!result.Records.Any())
                    {
                        MessageTitle = $"Búsqueda finalizada.";
                        MessageSubTitle = $"Actualmente no hay registros en esta {searchViewModel.SelectedLocality.LocalityName}.";
                        IsContinueGoBackEnabled = true;
                    }
                    else
                    {
                        Records = result.Records;
                        IsContinueGoBackEnabled = false;
                    }

                }
                IsBusy = false;
            }

        }


    }
}