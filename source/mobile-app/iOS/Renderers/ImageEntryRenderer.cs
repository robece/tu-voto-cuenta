using System;
using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using TuVotoCuenta.Controls;
using TuVotoCuenta.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FFImageLoading;
using FFImageLoading.Svg.Platform;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace TuVotoCuenta.iOS.Renderers
{
    public class ImageEntryRenderer : EntryRenderer
    {
        protected async override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (ImageEntry)this.Element;
            var textField = this.Control;
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        textField.LeftViewMode = UITextFieldViewMode.Always;
                        textField.LeftView = await GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                    case ImageAlignment.Right:
                        textField.RightViewMode = UITextFieldViewMode.Always;
                        textField.RightView = await GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                }
            }

            var dark = new UIColor((System.nfloat)(48.0 / 255.0), (System.nfloat)(48.0 / 255.0), (System.nfloat)(48.0 / 255.0), (System.nfloat)1.0);
           
            textField.BorderStyle = UITextBorderStyle.None;
            CALayer bottomBorder = new CALayer
            {
                Frame = new CGRect(0.0f, element.HeightRequest - 1, this.Frame.Width, 1.0f),
                BorderWidth = 2.0f,
                BorderColor = dark.CGColor
            };

            textField.Layer.AddSublayer(bottomBorder);
            textField.Layer.MasksToBounds = true;
        }

        async Task<UIView> GetImageView(string imagePath, int height, int width)
        {
            UIImage img = await ImageService.Instance.LoadFile(imagePath)
                                            .WithCustomDataResolver(new SvgDataResolver(30, 0, true))
                                            .AsUIImageAsync();

            var uiImageView = new UIImageView(img)
            {
                Frame = new RectangleF(0, 0, width, height)
            };
            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 10, height));
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }
    }
}