using TuVotoCuenta.Controls;
using Xamarin.Forms;

namespace TuVotoCuenta.Behaviors
{
	public class StepEntryLostFocus : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Unfocused += Bindable_Unfocused;
        }

		protected override void OnDetachingFrom(Entry bindable)
        {
			bindable.Unfocused -= Bindable_Unfocused;
        }

        void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            ((StepPage)((NavigationPage)((MasterDetailPage)Application.Current.MainPage).Detail).CurrentPage).UnfocusSave();
        }
    }
}