using System;
using System.Linq;
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

        public Command ContinueCommand { get; set; }

        public Command ContinueGoBackCommand { get; set; }

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
            ContinueCommand = new Command(async () => await Next());
            ContinueGoBackCommand = new Command(async () => await Back());

            item = JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem);

            SendReport();
        }

        private async Task Back()
        {
            await navigation.PopAsync();
        }

        private async Task SendReport()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                MessageTitle = "Enviando";
                MessageSubTitle = "Espera un momento";
                item.RecordHash = CreateHash();
                item.DeviceHash = Helpers.HashHelper.GetSha256Hash(Plugin.DeviceInfo.CrossDeviceInfo.Current.Id);
                item.UserName = Settings.Profile_Username;
                var imagefile = Helpers.LocalFilesHelper.ReadFile(item.UID.ToString());
                item.Image = HashHelper.GetSha256Hash(imagefile);
                imagefile = null;

                AddReportRequest addReportRequest = new AddReportRequest()
                {
                    RecordItem = item
                };

                var response = await RestHelper.AddReportAsync(addReportRequest);
                if (response.Status != Enums.ResponseStatus.Ok)
                {
                    IsContinueGoBackEnabled = true;
                    IsContinueEnabled = false;
                    MessageTitle = "Se presentó un problema al realizar el registro.";
                    MessageSubTitle = response.Message;
                }
                else
                {

                    IsContinueEnabled = true;
                    IsContinueGoBackEnabled = false;
                    MessageTitle = $"¡Gracias {Settings.Profile_Username}!";
                    MessageSubTitle = "Tu registro ha sido completado satisfactoriamente.";

                    Settings.CurrentRecordItem = string.Empty;

                    var count = navigation.NavigationStack.Count;
                    for (int i = 0; i < count - 1; i++)
                    {
                        navigation.RemovePage(navigation.NavigationStack.ElementAt(0));
                    }
                    navigation.InsertPageBefore(new WelcomePage(), navigation.NavigationStack.Last());

                }
            }
            IsBusy = false;
        }

        private async Task Next()
        {
            NavigationPage.SetHasBackButton(navigation.NavigationStack.Last(), true);
            Application.Current.MainPage = new MasterPage();
        }


        string CreateHash()
        {

            var imagefile = Helpers.LocalFilesHelper.ReadFile(item.UID.ToString());

            StringBuilder sb = new StringBuilder();
            sb.Append(item.UID);
            sb.Append(item.DeviceHash);
            sb.Append(item.BoxNumber);
            sb.Append(item.BoxSection);
            sb.Append(item.ImageLatitude);
            sb.Append(item.ImageLongitude);
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
            sb.Append(item.UID);
            sb.Append(item.RecordHash);
            sb.Append(item.BlockchainTransaction);
           

            var hash = $"0x{Helpers.HashHelper.GetSha256Hash(sb.ToString())}";

            return hash;
        }

    }
}
