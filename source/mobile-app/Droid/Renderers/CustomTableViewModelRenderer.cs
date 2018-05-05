using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using TuVotoCuenta.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TableViewModelRenderer), typeof(CustomTableViewModelRenderer))]
namespace TuVotoCuenta.Droid.Renderers
{
    public class CustomTableViewModelRenderer : TableViewModelRenderer
    {
        public CustomTableViewModelRenderer(Context Context, global::Android.Widget.ListView ListView, TableView View)
            : base(Context, ListView, View)
        { }
        public override global::Android.Views.View GetView(int position, global::Android.Views.View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);

            var element = this.GetCellForPosition(position);

            if (element.GetType() == typeof(TextCell))
            {
                var text = ((((view as LinearLayout).GetChildAt(0) as LinearLayout).GetChildAt(1) as LinearLayout).GetChildAt(0) as TextView);
                var divider = (view as LinearLayout).GetChildAt(1);

                divider.SetBackgroundColor(Android.Graphics.Color.Rgb(150, 150, 150));

                if (text.Text == "Configuraciones")
                {
                    text.SetTextColor(Android.Graphics.Color.Rgb(155, 155, 159));
                }else
                {
                    text.SetTextColor(Android.Graphics.Color.Rgb(0, 0, 0));
                }
            }

            return view;
        }
    }
}