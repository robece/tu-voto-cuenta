using System;
using System.Collections.Generic;
using TuVotoCuenta.Controls;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class ProductTeamPage : ContentPage
    {
        public ProductTeamPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new ProductTeamViewModel(this.Navigation);
        }
    }
}