using System;
using Android.Content;
using TuVotoCuenta.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TableView), typeof(CustomTableViewRenderer))]
namespace TuVotoCuenta.Droid.Renderers
{
    public class CustomTableViewRenderer : TableViewRenderer
    {
        Context context = null;

        public CustomTableViewRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        protected override TableViewModelRenderer GetModelRenderer(global::Android.Widget.ListView listView, TableView view)
        {
            return new CustomTableViewModelRenderer(this.Context, listView, view);
        }
    }
}