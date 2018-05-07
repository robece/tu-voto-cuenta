using System;
using Android.Content;
using TuVotoCuenta.Controls;
using TuVotoCuenta.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(ImplicitButton), typeof(CustomButtonRenderer))]
namespace TuVotoCuenta.Droid.Renderers
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        Context context = null;

        public CustomButtonRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                {
                    Control.Elevation = 0;
                }
            }
        }
    }
}