using System;
using TuVotoCuenta.Effects;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage()
        { 
        }

        public CustomNavigationPage(Page page) : base (page)
        { 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Effects.Add(new ShadowEffect()
            {
                Radius = 5,
                DistanceX = 0,
                DistanceY = 0,
                Color = Color.Black
            });
        }
    }
}