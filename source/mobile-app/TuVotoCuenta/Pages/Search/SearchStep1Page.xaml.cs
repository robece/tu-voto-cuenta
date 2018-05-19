using System;
using System.Collections.Generic;
using TuVotoCuenta.ViewModels.Search;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages.Search
{
    public partial class SearchStep1Page : ContentPage
    {
        public SearchStep1Page()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel(Navigation);
        }
    }
}
