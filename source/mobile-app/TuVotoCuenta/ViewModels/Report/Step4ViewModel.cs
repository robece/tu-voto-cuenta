using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Geolocator;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Pages;
using TuVotoCuenta.Pages.Report;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class Step4ViewModel : BaseViewModel
    {
        INavigation navigation = null;
        
        public Step4ViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

		void InitializeViewModel()
		{
			Title = "Imagen";

			TakePhotoCommand = new Command(async () => await TakePhoto());
			ChoosePhotoCommand = new Command(async () => await ChoosePhoto());
            
            NextCommand = new Command(async () => await Next());

            RecordItem item = JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem);
            Photo = Helpers.LocalFilesHelper.ReadFile(item.UID.ToString());
            App.Latitude = item.ImageLatitude;
            App.Longitude = item.ImageLongitude;
		}

		bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        async Task StartLocationAsync()
        {
            var locator = CrossGeolocator.Current;
            try
            {
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(2));
                if (position != null)
                {
                    App.Latitude = position.Latitude;
                    App.Longitude = position.Longitude;
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert("TuVotoCuenta", "Es necesario que actives la localización desde tu dispositivo móvil para poder usar la funcionalidad de mapas y geolocalización.", "Aceptar");
                    });
                }
            }
            catch
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("TuVotoCuenta", "Es necesario que actives la localización desde tu dispositivo móvil para poder usar la funcionalidad de mapas y geolocalización.", "Aceptar");
                });
            }
        }

        bool ValidateInformation()
        {
            if (Photo == null || !Photo.Any())
                return false;
            return true;
        }

        void SavePhoto()
        {
            RecordItem item = JsonConvert.DeserializeObject<RecordItem>(Settings.CurrentRecordItem);
            Helpers.LocalFilesHelper.SaveFile(item.UID.ToString(), Photo);
            item.ImageLatitude = App.Latitude;
            item.ImageLongitude = App.Longitude;
            item.Username = Settings.Profile_Username;
            item.ImageBytes = Convert.ToBase64String(Photo);
            Settings.CurrentRecordItem = JsonConvert.SerializeObject(item);
        }

        #region Commands

		public Command TakePhotoCommand { get; set; }
        public Command ChoosePhotoCommand { get; set; }

        async Task ChoosePhoto()
        {
            Photo = await Helpers.MediaHelper.PickPhotoAsync();
            await Task.Run(async () => {
                if (IsLocationAvailable())
                {
                    await StartLocationAsync();
                }
            });
            SavePhoto();
        }

        async Task TakePhoto()
        {
            Photo = await Helpers.MediaHelper.TakePhotoAsync();

			await Task.Run(async () => {
                if (IsLocationAvailable())
                {
                    await StartLocationAsync();
                }
            });
            SavePhoto();
        }
        
        public Command NextCommand { get; set; }

        async Task Next()
        {

            if (!ValidateInformation())
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresa la información correcta.", "Aceptar");
            else if (!IsBusy)
            {
                SavePhoto();
                await navigation.PushAsync(new ReportConfirmationPage());
            }

        }

        #endregion

        #region Binding attributes

		byte[] photo;
        public byte[] Photo
        {
            get { return photo; }
            set { SetProperty(ref photo, value); }
        }
        #endregion
    }
}