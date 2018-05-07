using System;
using System.Collections.Generic;
using TuVotoCuenta.MasterDetail;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class MasterPage : MasterDetailPage
    {
        MenuPage menuPage = null;

        public MasterPage()
        {
            menuPage = new MenuPage();

            Master = new NavigationPage(menuPage)
            {
                Title = "Menu",
                Icon = "hamburger"
            };
            Detail = new CustomNavigationPage(new WelcomePage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            menuPage.Menu.ItemSelected += NavigateToEventHandler;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            menuPage.Menu.ItemSelected -= NavigateToEventHandler;
        }

        void NavigateToEventHandler(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            MasterPageItem selectedItem = e.SelectedItem as MasterPageItem;

            Page displayPage = null;

            if (selectedItem.TargetTypeParameters == null)
            {
                displayPage = (Page)Activator.CreateInstance(selectedItem.TargetType);
            }
            else
            {
                displayPage = (Page)Activator.CreateInstance(selectedItem.TargetType, selectedItem.TargetTypeParameters);
            }

            //TODO: App Center event analytics

            Detail = new NavigationPage(displayPage);

            menuPage.Menu.SelectedItem = null;
            IsPresented = false;
        }
    }
}