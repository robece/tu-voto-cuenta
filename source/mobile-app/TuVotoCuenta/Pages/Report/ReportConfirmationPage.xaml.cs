using System;
using System.Collections.Generic;
using TuVotoCuenta.ViewModels.Report;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages.Report
{
    public partial class ReportConfirmationPage : ContentPage
    {
        public ReportConfirmationPage()
        {
            InitializeComponent();
            BindingContext = new ReportConfirmationViewModel(this.Navigation);
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}
