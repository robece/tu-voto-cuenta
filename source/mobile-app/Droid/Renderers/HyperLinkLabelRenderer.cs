using System;
using Android.Text.Util;
using TuVotoCuenta.Controls;
using TuVotoCuenta.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HyperLinkLabel), typeof(HyperLinkLabelRenderer))]
namespace TuVotoCuenta.Droid.Renderers
{
    public class HyperLinkLabelRenderer : LabelRenderer
    {
        public HyperLinkLabelRenderer()
            :base(Android.App.Application.Context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {

                var nativeEditText = (global::Android.Widget.TextView)Control;

                nativeEditText.SetTextColor(Color.Blue.ToAndroid());
                nativeEditText.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;

                nativeEditText.Click += (se, ev) =>
                {
                    Device.OpenUri(((HyperLinkLabel)Element).UriToNavigate);
                };

            }
        }
    }
}
