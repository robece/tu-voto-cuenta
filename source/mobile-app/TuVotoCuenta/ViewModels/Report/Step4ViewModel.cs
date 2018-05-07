using System.Threading.Tasks;
using TuVotoCuenta.Pages;
using Xamarin.Forms;

namespace TuVotoCuenta.ViewModels
{
    public class Step4ViewModel : BaseViewModel
    {
        INavigation navigation = null;
        
        public Step4ViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitializeViewModel();
        }

		void InitializeViewModel()
		{
			Title = "Imagen";

			TakePhotoCommand = new Command(async () => await TakePhoto());
			ChoosePhotoCommand = new Command(async () => await ChoosePhoto());

			SaveChangesCommand = new Command(async () => await Save());
            NextCommand = new Command(async () => await Next());
		}

        #region Commands

		public Command TakePhotoCommand { get; set; }
        public Command ChoosePhotoCommand { get; set; }

        async Task ChoosePhoto()
        {
            Photo = await Helpers.MediaHelper.PickPhotoAsync();
        }

        async Task TakePhoto()
        {
            Photo = await Helpers.MediaHelper.TakePhotoAsync();
        }

		public Command SaveChangesCommand { get; set; }
        public Command NextCommand { get; set; }

        async Task Save()
        {
        }

        async Task Next()
        {
            await navigation.PushAsync(new WelcomePage());
        }

        #endregion

        #region Binding attributes

		byte[] photo;
        public byte[] Photo
        {
            get { return photo; }
            set { SetProperty(ref photo, value); }
        }

		string photoTimestamp;
        public string PhotoTimestamp
        {
            get { return photoTimestamp; }
            set { SetProperty(ref photoTimestamp, value); }
        }

        #endregion
    }
}