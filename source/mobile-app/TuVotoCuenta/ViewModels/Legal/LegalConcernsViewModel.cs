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
		SignUpAccountRequest model = null;

        public LegalConcernsViewModel(INavigation navigation) 
        {
            this.navigation = navigation;
            InitializeViewModel();
			IsVisibleAccept = false;
			IsVisibleCancel = false;
        }

		public LegalConcernsViewModel(INavigation navigation, SignUpAccountRequest model)
        {
            this.navigation = navigation;
            this.model = model;
            InitializeViewModel();
			IsVisibleAccept = true;
            IsVisibleCancel = true;
        }

        void InitializeViewModel()
        {
            Title = "Uso y privacidad";
            Task.Run(async() => { 
                LegalConcerns = await DependencyService.Get<ILegalConcerns>().ReadLegalConcerns();
            });

			SignUpCommand = new Command(() => SignUp());
            CancelSignUpCommand = new Command(() => CancelSignUp());
        }

        void SignUp()
        {
            Application.Current.MainPage = new SignUpConfirmationPage(this.model);
        }

        void CancelSignUp()
        {
            Application.Current.MainPage = new SignUpPage();
        }

        #region Commands

		public Command SignUpCommand { get; set; }
        public Command CancelSignUpCommand { get; set; }

        #endregion

        #region Binding attributes

        string legalConcerns;
        public string LegalConcerns
        {
            get { return legalConcerns; }
            set { SetProperty(ref legalConcerns, value); }
        }

		bool isVisibleAccept;
        public bool IsVisibleAccept
        {
			get { return isVisibleAccept; }
			set { SetProperty(ref isVisibleAccept, value); }
        }

		bool isVisibleCancel;
        public bool IsVisibleCancel
        {
			get { return isVisibleCancel; }
			set { SetProperty(ref isVisibleCancel, value); }
        }

        #endregion
    }
}