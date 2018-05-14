using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using TuVotoCuenta.Pages;
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

        #region Commands

		public Command TakePhotoCommand { get; set; }
        public Command ChoosePhotoCommand { get; set; }

        async Task ChoosePhoto()
        {
            Photo = await Helpers.MediaHelper.PickPhotoAsync();
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
        }
        
        public Command NextCommand { get; set; }

        async Task Next()
        {
            await navigation.PushAsync(new WelcomePage());
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