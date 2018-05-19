using System;
using System.Collections.Generic;
using TuVotoCuenta.ViewModels.Search;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages.Search
{
    public partial class SearchStep2Page : ContentPage
    {
        public SearchStep2Page()
        {
            InitializeComponent();
        }

        public async void Handle_Tapped(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet ("Ordenar por:", "Cancelar", null, "Más positivos", "Más negativos","Más votos");
            if (BindingContext is SearchViewModel viewModel)
            {
                switch (action)
                {
                    case "Más positivos":
                        await viewModel.OrderResults(ListOrder.MoreUpvotes);
                        break;
                    case "Más negativos":
                        await viewModel.OrderResults(ListOrder.MoreDownVotes);
                        break;
                    case "Más votos":
                        await viewModel.OrderResults(ListOrder.MoreVotes);
                        break;
                }
            }
        }
    }
}
