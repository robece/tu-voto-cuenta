using TuVotoCuenta.Controls;
using TuVotoCuenta.ViewModels;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace TuVotoCuenta.Pages
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        public MenuPage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
			BindingContext = new MenuViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();        
        }

        void InitializeComponent()
        {
            BackgroundColor = Color.White;

            if (Device.RuntimePlatform == Device.iOS)
                this.Padding = new Thickness(0, 20, 0, 0);
            
            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Style = Application.Current.Resources["MasterDetailHeaderUserDetailCell"] as Style
            };

            var user_layout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Style = Application.Current.Resources["MasterDetailHeaderUserDetailCell"] as Style
            };

            var user_image = new CircleImage
            {
                WidthRequest = 55,
                HeightRequest = 55,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                BorderColor = Color.White,
                BorderThickness = 2,
                Margin = new Thickness(15, 10, 0, 0)
            };
            user_image.SetBinding(Image.SourceProperty, "UserPicture");

            var user_detail_layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.End,
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(5, 0, 10, 0)
            };

            var user_name = new Label
            {
                Style = Application.Current.Resources["MasterDetailHeaderFullnameCell"] as Style,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            user_name.SetBinding(Label.TextProperty, "UserFullname");

            var user_account = new Label
            {
                Style = Application.Current.Resources["MasterDetailHeaderEmailCell"] as Style,
				FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                LineBreakMode = LineBreakMode.TailTruncation
            };
			user_account.SetBinding(Label.TextProperty, "UserAccount");

            user_detail_layout.Children.Add(user_name);
			user_detail_layout.Children.Add(user_account);

            user_layout.Children.Add(user_image);
            user_layout.Children.Add(user_detail_layout);

			var user_detail_vertical = new StackLayout
			{
				VerticalOptions = LayoutOptions.End,
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(15, 10, 10, 0)
			};
                     
            Menu = new MenuListView();
			layout.Children.Add(user_layout);
            layout.Children.Add(Menu);
            Content = layout;
        }
    }
}