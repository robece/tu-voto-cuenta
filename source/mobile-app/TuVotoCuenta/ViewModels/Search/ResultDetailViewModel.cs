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
    public class ResultDetailViewModel : BaseViewModel
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

        private int upVotes;

        public int UpVotes
        {
            get => upVotes;
            set => SetProperty(ref upVotes, value);
        }


        private int downVotes;

        public int DownVotes
        {
            get => downVotes;
            set => SetProperty(ref downVotes, value);
        }

        INavigation navigation = null;

        public Command ContinueGoBackCommand { get; set; }


        private RecordItem recordItem;

        public RecordItem RecordItem
        {
            get => recordItem;
            set => SetProperty(ref recordItem, value);
        }

        public ResultDetailViewModel(INavigation navigation, RecordItem recordItem)
        {
            this.navigation = navigation;
            this.recordItem = recordItem;
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


                GetRecordVoteCountRequest recordVoteCountRequest = new GetRecordVoteCountRequest()
                {
                    Hash = recordItem.RecordHash
                };

                var response = await RestHelper.GetRecordVoteCountAsync(recordVoteCountRequest);
                if (response.Status != Enums.ResponseStatus.Ok)
                {
                    IsContinueGoBackEnabled = true;
                    MessageTitle = "Se presentó un problema al consultar el registro.";
                    MessageSubTitle = response.Message;
                }
                else
                {
                    IsContinueGoBackEnabled = false;
                    UpVotes = response.Approvals;
                    DownVotes = response.Disapprovals;
                }
            }
            IsBusy = false;
        }

    }
}
