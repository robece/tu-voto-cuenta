using TuVotoCuenta.Controls;
using Xamarin.Forms;

namespace TuVotoCuenta.Behaviors
{
    public class StepImageEntryLostFocus : Behavior<ImageEntry>
    {
        protected override void OnAttachedTo(ImageEntry bindable)
        {
            bindable.Unfocused += Bindable_Unfocused;
        }

		protected override void OnDetachingFrom(ImageEntry bindable)
        {
            bindable.Unfocused -= Bindable_Unfocused;
        }

        void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            ((StepPage)((NavigationPage)((MasterDetailPage)Application.Current.MainPage).Detail).CurrentPage).UnfocusSave();
        }
    }
}