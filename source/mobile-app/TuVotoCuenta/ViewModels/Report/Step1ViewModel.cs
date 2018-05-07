using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Plugin.DeviceInfo;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class Step1ViewModel : BaseViewModel
    {
        INavigation navigation = null;

		public Step1ViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Casilla";

			SaveChangesCommand = new Command(async () => await Save());
			NextCommand = new Command(async() => await Next());

			Task.Run(() =>
            {
                using(var sha256 = SHA256.Create())  
                {
                    var dh = sha256.ComputeHash(Encoding.UTF8.GetBytes(CrossDeviceInfo.Current.Id));
                    DeviceHash = System.BitConverter.ToString(dh).Replace("-", "").ToLower();  
                }  
            });  
        }

        #region Commands

		public Command SaveChangesCommand { get; set; }
		public Command NextCommand { get; set; }
        
		async Task Save()
        {
        }

		async Task Next()
        {
			await navigation.PushAsync(new Step2Page());
        }

        #endregion

        #region Binding attributes

		int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

		string deviceHash;
        public string DeviceHash
        {
            get { return deviceHash; }
            set { SetProperty(ref deviceHash, value); }
        }

        string boxNumber;
        public string BoxNumber
        {
            get { return boxNumber; }
            set { SetProperty(ref boxNumber, value); }
        }

        string boxSection;
        public string BoxSection
        {
            get { return boxSection; }
            set { SetProperty(ref boxSection, value); }
        }

        #endregion
    }
}