using System;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Interfaces;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class LegalConcernsViewModel : BaseViewModel
    {
        INavigation navigation = null;

        public LegalConcernsViewModel(INavigation navigation) 
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

        void InitializeViewModel()
        {
            Title = "Uso y privacidad";
            Task.Run(async() => { 
                LegalConcerns = await DependencyService.Get<ILegalConcerns>().ReadLegalConcerns();
            });
        }

        #region Commands

        #endregion

        #region Binding attributes

        string legalConcerns;
        public string LegalConcerns
        {
            get { return legalConcerns; }
            set { SetProperty(ref legalConcerns, value); }
        }

        #endregion
    }
}