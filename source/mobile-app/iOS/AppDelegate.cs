using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Touch;
using FFImageLoading.Svg.Forms;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;

namespace TuVotoCuenta.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

			App.ScreenWidth = UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = UIScreen.MainScreen.Bounds.Height;

            app.StatusBarStyle = UIStatusBarStyle.LightContent;

            LoadApplication(new App());

            ImageCircleRenderer.Init();
            SvgCachedImage.Init();

            CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);

            var statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                statusBar.BackgroundColor = UIColor.Black;
            }
         
            return base.FinishedLaunching(app, options);
        }
    }
}