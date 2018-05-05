using System;
using System.Collections.Generic;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class AddReportStep1Page : ContentPage
    {
        public AddReportStep1Page()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new AddReportStep1ViewModel(this.Navigation);
        }
    }
}
