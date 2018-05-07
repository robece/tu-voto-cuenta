using System;
using System.Collections.Generic;
using TuVotoCuenta.Controls;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class ProductPage : ContentPage
    {
        public ProductPage()
        {
            InitializeComponent();
			BindingContext = new ProductViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();      
        }
    }
}