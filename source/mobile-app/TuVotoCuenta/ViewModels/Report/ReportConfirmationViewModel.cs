using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Helpers;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels.Report
{
    public class ReportConfirmationViewModel : BaseViewModel
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

        INavigation navigation = null;

        public Command NextCommand { get; set; }

        RecordItem item;

        public ReportConfirmationViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Envió";
            IsContinueGoBackEnabled = false;
            NextCommand = new Command(async () => await Next());

            item = JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem);

            SendReport();
        }


        private async Task SendReport()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                MessageTitle = "Enviando";
                MessageSubTitle = "Espera un momento";
                item.RecordHash = CreateHash();

                AddReportRequest addReportRequest = new AddReportRequest()
                {
                    RecordItem = item
                };

                var response = await RestHelper.AddReportAsync(addReportRequest);
                if (!response.IsSucceded)
                {
                    IsContinueGoBackEnabled = true;
                    MessageTitle = "Se presentó un problema al realizar el registro.";
                    MessageSubTitle = "El proceso de registro no fue satisfactorio.";
                }
                else
                {
                    IsContinueEnabled = true;
                    MessageTitle = $"¡Gracias {Settings.Profile_Username}!";
                    MessageSubTitle = "Tu registro ha sido completado satisfactoriamente.";
                }
                    

                await Task.Delay(500);
            }
            IsBusy = false;
        }

        private async Task Next()
        {
            Application.Current.MainPage = new MasterPage();
        }


        string CreateHash()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(item.UID);
            sb.Append(item.DeviceHash);
            sb.Append(item.BoxNumber);
            sb.Append(item.BoxSection);
            sb.Append(item.Latitude);
            sb.Append(item.Longitude);
            sb.Append(item.LocationDetails);
            sb.Append(item.Entity);
            sb.Append(item.Municipality);
            sb.Append(item.Locality);
            sb.Append(item.PartyPAN);
            sb.Append(item.PartyPRI);
            sb.Append(item.PartyPRD);
            sb.Append(item.PartyVerde);
            sb.Append(item.PartyPT);
            sb.Append(item.PartyMC);
            sb.Append(item.PartyNA);
            sb.Append(item.PartyMOR);
            sb.Append(item.PartyES);
            sb.Append(item.PartyINDJai);
            sb.Append(item.PartyOtro);
            sb.Append(item.PartyPANMC);
            sb.Append(item.PartyPANPRD);
            sb.Append(item.PartyPRDPANMC);
            sb.Append(item.PartyPRDMC);
            sb.Append(item.PartyMORPT);
            sb.Append(item.PartyMORES);
            sb.Append(item.PartyPTESMOR);
            sb.Append(item.PartyPRIVERNA);
            sb.Append(item.PartyPRIVER);
            sb.Append(item.PartyPRINA);
            sb.Append(item.PartyVERNA);
            sb.Append(item.PartyPTES);
            sb.Append(item.PartyPRDPAN);
            sb.Append(Helpers.LocalFilesHelper.GetFileHexString(item.UID.ToString()));
            sb.Append(item.RecordHash);
            sb.Append(item.BlockchainTransaction);
            sb.Append(item.CreatedDate);

            var hash = Helpers.HashHelper.GetSha256Hash(sb.ToString());

            return hash;
        }

    }
}
