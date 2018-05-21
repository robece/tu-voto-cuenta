using TuVotoCuenta.Droid.Effects;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;

[assembly: ResolutionGroupName("Effects")]
[assembly: ExportEffect(typeof(DisabledSelectionEffect), "DisabledSelectionEffect")]
namespace TuVotoCuenta.Droid.Effects
{
    public class DisabledSelectionEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var listview = Control as Android.Widget.ListView;
            listview.Selector.SetColorFilter(Xamarin.Forms.Color.Transparent.ToAndroid(), PorterDuff.Mode.Multiply);
        }

        protected override void OnDetached()
        {
            // TODO: Remove your effect.
        }
    }
}
