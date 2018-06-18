using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Interfaces;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace TuVotoCuenta.Pages
{
    public partial class LegalConcernsPage : ContentPage
    {
        public LegalConcernsPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, true);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            BindingContext = new LegalConcernsViewModel(this.Navigation);
        }

        public LegalConcernsPage(SignUpAccountRequest model)
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            BindingContext = new LegalConcernsViewModel(this.Navigation, model);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var legalConcerns = Helpers.LocalFilesHelper.ReadFileInPackage("LegalConcerns.html"); ;
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = legalConcerns;
            browser.Source = htmlSource;
        }
    }
}