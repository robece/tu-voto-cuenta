using System;
using Foundation;
using TuVotoCuenta.Controls;
using TuVotoCuenta.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HyperLinkLabel), typeof(HyperLinkLabelRenderer))]
namespace TuVotoCuenta.iOS.Renderers
{
    public class HyperLinkLabelRenderer : LabelRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var uiLabel = (UILabel)Control;
                uiLabel.TextColor = UIColor.Blue;
                uiLabel.BackgroundColor = UIColor.Clear;
                uiLabel.UserInteractionEnabled = true;
                uiLabel.AttributedText = new NSAttributedString(Element.Text, underlineStyle: NSUnderlineStyle.Single);

                var linkGesture = new UITapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1,
                    DelaysTouchesBegan = true
                };

                linkGesture.AddTarget(() =>
                {
                    Device.OpenUri(((HyperLinkLabel)Element).UriToNavigate);
                });

                uiLabel.AddGestureRecognizer(linkGesture);
            }
        }


    }
}
