using System;
using System.Collections.Generic;
using TuVotoCuenta.Domain;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages.Search
{
    public partial class VoteHistoryPage : ContentPage
    {
        public VoteHistoryPage()
        {
            InitializeComponent();
        }

        public VoteHistoryPage(RecordItem recordItem)
        {
            InitializeComponent();
            BindingContext = new ViewModels.Search.VoteHistoryViewModel(Navigation, recordItem);
        }
    }
}