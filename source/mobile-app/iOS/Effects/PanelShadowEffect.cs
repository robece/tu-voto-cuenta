using System;
using System.Linq;
using TuVotoCuenta.Effects;
using TuVotoCuenta.iOS.Effects;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName ("TuVotoCuenta.Effects")]
[assembly: ExportEffect (typeof (PanelShadowEffect), "PanelShadowEffect")]
namespace TuVotoCuenta.iOS.Effects
{
    public class PanelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
                if (effect == null)
                {
                    return;
                }
                var control = Control;
                if (control == null)
                {
                    var renderer = Platform.GetRenderer((VisualElement)Element);
                    control = renderer.ViewController.View;
                }
                control.Layer.CornerRadius = effect.Radius;
                control.Layer.ShadowColor = effect.Color.ToCGColor();
                control.Layer.ShadowOffset = new CGSize(effect.DistanceX, effect.DistanceY);
                control.Layer.ShadowOpacity = 1.0f;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: {0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}