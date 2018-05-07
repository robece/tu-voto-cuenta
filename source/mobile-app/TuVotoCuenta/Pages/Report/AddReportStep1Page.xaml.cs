using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Geolocator;
using TuVotoCuenta.Classes;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TuVotoCuenta.Pages
{
    public partial class AddReportStep1Page : ContentPage
    {
        public AddReportStep1Page()
        {
            InitializeComponent();
			BindingContext = new AddReportStep1ViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
                          
			Task.Run(() => {
                if (IsLocationAvailable())
                {
                    StartLocationAsync();
                }
            });
        }

		bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        async void StartLocationAsync()
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
							customMap.IsVisible = true;
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
