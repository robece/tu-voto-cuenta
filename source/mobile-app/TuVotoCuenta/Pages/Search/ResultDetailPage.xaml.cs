using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages.Search
{
    public partial class ResultDetailPage : ContentPage
    {
        public ResultDetailPage(RecordItem recordItem)
        {
            InitializeComponent();

            BindingContext = new ViewModels.Search.ResultDetailViewModel(Navigation, recordItem);
        }
    }
}