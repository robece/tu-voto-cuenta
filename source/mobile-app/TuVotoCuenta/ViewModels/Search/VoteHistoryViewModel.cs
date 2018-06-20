using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels.Search
{
    public class VoteHistoryViewModel : BaseViewModel
    {

        INavigation navigation = null;
        RecordItem recordItem;

        public VoteHistoryViewModel(INavigation navigation, RecordItem recordItem)
        {
            Title = "Historial de Votos";
            this.navigation = navigation;
            this.recordItem = recordItem;
            IsContinueGoBackEnabled = true;
            GetvotesHitoryAsync(); ;

        }

        private ObservableCollection<Vote> votes;

        public ObservableCollection<Vote> Votes
        {
            get => votes;
            set => SetProperty(ref votes, value);
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

        private Vote selectedVote;

        public Vote SelectedVote
        {
            get => selectedVote;
            set
            {
                if (selectedVote != value && value != null)
                {
                    Device.OpenUri(new Uri($"{Settings.BlockchainURL}{value.Hash}"));
                    SelectedVote = null;
                }
                SetProperty(ref selectedVote, value);
            }
        }


        private async Task GetvotesHitoryAsync()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                MessageTitle = "Buscando...";
                MessageSubTitle = "Espera un momento, el proceso puede tomar unos segundos.";

                GetRecordVoteListRequest recordVoteListRequest = new GetRecordVoteListRequest();
                recordVoteListRequest.Hash = recordItem.RecordHash;

                var result = await Helpers.RestHelper.GetRecordVoteListAsync(recordVoteListRequest);

                if (result.Status != Enums.ResponseStatus.Ok)
                {
                    messageSubTitle = result.Message;
                    isContinueGoBackEnabled = true;
                }
                else
                {
                    if (!result.Votes.Any())
                    {
                        MessageTitle = $"Búsqueda finalizada.";
                        MessageSubTitle = $"Actualmente no hay votos en este registro.";
                        IsContinueGoBackEnabled = true;
                    }
                    else
                    {
                        Votes = new ObservableCollection<Vote>(result.Votes);
                        IsContinueGoBackEnabled = false;
                    }

                }
                IsBusy = false;
            }
        }
    }
}
