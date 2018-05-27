using System;
using System.Collections.Generic;
using TuVotoCuenta.Domain;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
    public partial class LegalConcernsPage : ContentPage
	{
        public LegalConcernsPage()
        {
            InitializeComponent();
			BindingContext = new LegalConcernsViewModel(this.Navigation);
        }

		public LegalConcernsPage(SignUpAccountRequest model)
        {
            InitializeComponent();
			BindingContext = new LegalConcernsViewModel(this.Navigation, model);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();         
        }
    }
}