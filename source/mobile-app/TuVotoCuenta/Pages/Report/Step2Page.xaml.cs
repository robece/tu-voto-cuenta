using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using TuVotoCuenta.Classes;
using TuVotoCuenta.Controls;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TuVotoCuenta.Pages
{
	public partial class Step2Page : StepPage
    {      
        public Step2Page()
        {
            InitializeComponent();
			BindingContext = new Step2ViewModel(this.Navigation);
        }

		public override void UnfocusSave()
        {
            base.UnfocusSave();
            ((Step2ViewModel)BindingContext).Save();
        }

		protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(async() => {
                if (IsLocationAvailable())
                {
                    await StartLocationAsync();
                }
            });
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

                    if (!App.Latitude.Equals(0) && !App.Longitude.Equals(0))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            InitDrawing();
                            MapStackLayout.IsVisible = true;
                        });
                    }
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

        void InitDrawing()
        {
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(App.Latitude, App.Longitude),
                Label = "Aquí estás!",
                Address = string.Empty
            };

            var position = new Position(App.Latitude, App.Longitude);
            customMap.Circle = new CustomCircle
            {
                Position = position,
                Radius = 1000
            };

            customMap.Pins.Add(pin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.5)));
            customMap.HasZoomEnabled = false;
            customMap.HasScrollEnabled = false;
        }
    }
}