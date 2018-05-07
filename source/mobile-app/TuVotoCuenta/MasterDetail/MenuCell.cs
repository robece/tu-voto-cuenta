using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace TuVotoCuenta.MasterDetail
{
    public class MenuCell : ViewCell
    {
        public MenuCell() : base()
        {
            ContentView cellContent = new ContentView
            {
                Style = Application.Current.Resources["MasterDetailMenuCell"] as Style,
                Padding = 8
            };

            StackLayout cellContentLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            StackLayout imageLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(15, 5, 0, 3)
            };

            SvgCachedImage image = new SvgCachedImage
            {
                WidthRequest = 30,
                HeightRequest = 30
            };
            image.SetBinding(FFImageLoading.Forms.CachedImage.SourceProperty, "IconSource");

            StackLayout textLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(25, 5, 0, 3)
            };

            Label text = new Label
            {
                Style = Application.Current.Resources["MasterDetailMenuLabelCell"] as Style,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Start
            };
            text.SetBinding(Label.TextProperty, "Title");

            imageLayout.Children.Add(image);
            textLayout.Children.Add(text);

            cellContentLayout.Children.Add(imageLayout);
            cellContentLayout.Children.Add(textLayout);

            cellContent.Content = cellContentLayout;

            this.View = cellContent;
        }
    }
}