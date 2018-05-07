using System;
using System.Collections.Generic;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
			BindingContext = new SettingsViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();          
        }
    }
}