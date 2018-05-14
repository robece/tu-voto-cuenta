using TuVotoCuenta.Controls;
using Xamarin.Forms;

namespace TuVotoCuenta.Behaviors
{
    public class StepPickerLostFocus : Behavior<Picker>
    {
        protected override void OnAttachedTo(Picker bindable)
        {
            bindable.Unfocused += Bindable_Unfocused;
        }

		protected override void OnDetachingFrom(Picker bindable)
        {
            bindable.Unfocused -= Bindable_Unfocused;
        }

        void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            ((StepPage)((NavigationPage)((MasterDetailPage)Application.Current.MainPage).Detail).CurrentPage).UnfocusSave();
        }
    }
}