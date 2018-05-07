using System.Collections.Generic;
using Xamarin.Forms;
using TuVotoCuenta.Domain;
using TuVotoCuenta.MasterDetail;

namespace TuVotoCuenta.Controls
{
    public class MenuListView : ListView
    {
        public MenuListView()
        {
            List<Group> data = new MenuListData();

            ItemsSource = data;
            HasUnevenRows = true;
            IsGroupingEnabled = true;
            GroupDisplayBinding = new Binding("Name");
            Style = Application.Current.Resources["MasterDetailHeaderFullMenu"] as Style;
            SeparatorVisibility = SeparatorVisibility.None;

            var header = new DataTemplate(typeof(HeaderCell));
            GroupHeaderTemplate = header;

            var cell = new DataTemplate(typeof(MenuCell));
            ItemTemplate = cell;
        }
    }
}