using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace TuVotoCuenta.Pages.FAQ
{
    public partial class FaqPage : ContentPage
    {
        public FaqPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, true);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var legalConcerns = Helpers.LocalFilesHelper.ReadFileInPackage("Faqs.html"); ;
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = legalConcerns;
            browser.Source = htmlSource;
            browser.Navigating += (s, e) =>
             {
                 Uri uri = null;
                if (e.Url.StartsWith("http") && Uri.TryCreate(e.Url, UriKind.RelativeOrAbsolute, out uri))
                 {
                     Device.OpenUri(uri);
                    e.Cancel = true;
                 }
                 
             };
        }
    }
}
