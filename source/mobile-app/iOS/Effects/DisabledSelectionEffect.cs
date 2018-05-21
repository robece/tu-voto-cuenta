using TuVotoCuenta.iOS.Effects;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;

[assembly: ExportEffect(typeof(DisabledSelectionEffect), "DisabledSelectionEffect")]
namespace TuVotoCuenta.iOS.Effects
{
    public class DisabledSelectionEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            ((UITableView)Control).TintColor = Color.Transparent.ToUIColor();
        }

        protected override void OnDetached()
        {
            // TODO: Remove your effect.
        }
    }
}
