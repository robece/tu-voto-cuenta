using Xamarin.Forms;

namespace TuVotoCuenta.MasterDetail
{
    public class HeaderCell : ViewCell
    {
        public HeaderCell() : base()
        {
            ContentView cellContent = new ContentView
            {
                Style = Application.Current.Resources["MasterDetailHeaderCell"] as Style,
                Padding = 8
            };

            StackLayout cellContentLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            StackLayout textLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(15, 5, 0, 3)
            };

            Label text = new Label
            {
                Style = Application.Current.Resources["MasterDetailHeaderLabelCell"] as Style,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Start
            };
            text.SetBinding(Label.TextProperty, "Name");

            textLayout.Children.Add(text);
            cellContentLayout.Children.Add(textLayout);
            cellContent.Content = cellContentLayout;
            this.View = cellContent;
        }
    }
}