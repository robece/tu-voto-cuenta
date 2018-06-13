using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Helpers;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels.Search
{
    public class RecordsVoteListViewModel : BaseViewModel
    {
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

        bool isContinueGoBackEnabled;
        public bool IsContinueGoBackEnabled
        {
            get { return isContinueGoBackEnabled; }
            set { SetProperty(ref isContinueGoBackEnabled, value); }
        }

        private string hash;

        public string Hash
        {
            get => hash;
            set => SetProperty(ref hash, value);
        }

        private ObservableCollection<Vote> votes;

        public ObservableCollection<Vote> Votes
        {
            get => votes;
            set => SetProperty(ref votes, value);
        }

        INavigation navigation = null;

        public Command ContinueGoBackCommand { get; set; }

       

        public RecordsVoteListViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Envió";
            IsContinueGoBackEnabled = false;
            ContinueGoBackCommand = new Command(async () => await Back());
            SendRequest();
        }

        private async Task Back()
        {
            await navigation.PopAsync();
        }

        private async Task SendRequest()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                MessageTitle = "Enviando";
                MessageSubTitle = "Espera un momento";
               

                GetRecordVoteListRequest recordVoteListRequest = new GetRecordVoteListRequest()
                {
                    Hash = Hash
                };

                var response = await RestHelper.GetRecordVoteListAsync(recordVoteListRequest);
                if (response.Status != Enums.ResponseStatus.Ok)
                {
                    IsContinueGoBackEnabled = true;
                    MessageTitle = "Se presentó un problema al realizar el registro.";
                    MessageSubTitle = response.Message;
                }
                else
                {
                    Votes = new ObservableCollection<Vote>(response.Votes);
                }
            }
            IsBusy = false;
        }

    }
}
