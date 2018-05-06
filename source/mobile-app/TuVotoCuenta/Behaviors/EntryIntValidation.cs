using System;
using Xamarin.Forms;

namespace TuVotoCuenta.Behaviors
{
    public class EntryIntValidation :Behavior<Entry>
    {

		protected override void OnAttachedTo(Entry bindable)
		{
            bindable.TextChanged += Bindable_TextChanged;
     	}

		protected override void OnDetachingFrom(Entry bindable)
		{
            bindable.TextChanged -= Bindable_TextChanged;
		}

		void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OldTextValue != e.NewTextValue && !string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                int result = 0;
                if (!int.TryParse(e.NewTextValue, out result) || result>9999)
                    ((Entry)sender).Text = e.OldTextValue;
            } 
        }

	}
}
