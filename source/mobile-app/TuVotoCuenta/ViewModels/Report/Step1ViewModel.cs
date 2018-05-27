using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using TuVotoCuenta.Domain;
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
			Title = "Registro de casilla";
            
			if (!String.IsNullOrEmpty(Settings.CurrentRecordItem))
			{
				Device.BeginInvokeOnMainThread(async() => { 
					var answer = await Application.Current.MainPage.DisplayAlert("TuVotoCuenta", "Hemos detectado que ya tienes un reporte en progreso, ¿deseas continuar con él?, si seleccionas 'No' se borrarán todos los datos previamente capturados.", "Si", "No");
                    if (answer)
                    {
                        RecordItem item = JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem);
                        BoxNumber = item.BoxNumber;
                        BoxSection = item.BoxSection;
                    }
                    else
                    {
                        Settings.CurrentRecordItem = string.Empty;
                    }
				});
			}
               
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

		bool ValidateInformation()
		{
			if (String.IsNullOrEmpty(BoxNumber))
                return false;
			if (String.IsNullOrEmpty(BoxSection))
                return false;
			return true;
		}

        public void Save()
		{
			RecordItem item = (!String.IsNullOrEmpty(Settings.CurrentRecordItem)) ? JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem) : new RecordItem();

            item.BoxNumber = BoxNumber;
            item.BoxSection = BoxSection;

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
				await navigation.PushAsync(new Step2Page());            
            }
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