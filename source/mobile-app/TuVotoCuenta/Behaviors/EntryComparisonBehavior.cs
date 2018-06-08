using System;
using TuVotoCuenta.Helpers;
using Xamarin.Forms;

namespace TuVotoCuenta.Behaviors
{
    public class EntryComparisonBehavior : Behavior<Entry>
    {
        readonly BindableProperty CompareWithTypeProperty =
            BindableProperty.Create(nameof(CompareWith), typeof(Entry), typeof(EntryValidationBehavior), null);

        public Entry CompareWith
        {
            get { return (Entry)base.GetValue(CompareWithTypeProperty); }
            set
            { base.SetValue(CompareWithTypeProperty, value); }
        }


        static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EntryValidationBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set
            {
                if (IsValid != value)
                    base.SetValue(IsValidPropertyKey, value);
            }
        }

        static readonly BindablePropertyKey ValidationResultPropertyKey =
            BindableProperty.CreateReadOnly("ValidationResult", typeof(ValidationResult), typeof(EntryValidationBehavior), ValidationResult.NoValue);

        public static readonly BindableProperty ValidationResultProperty = ValidationResultPropertyKey.BindableProperty;

        public ValidationResult ValidationResult
        {
            get { return (ValidationResult)base.GetValue(ValidationResultProperty); }
            private set
            {
                if (ValidationResult != value)
                    base.SetValue(ValidationResultPropertyKey, value);
            }
        }



        private Entry attachedEntry;
        protected override void OnAttachedTo(Entry bindable)
        {
            attachedEntry = bindable;
            attachedEntry.TextChanged += Bindable_TextChanged;
            CompareWith.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {

            bindable.TextChanged -= Bindable_TextChanged;
            CompareWith.TextChanged -= Bindable_TextChanged;
        }

        void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {


            if (BindingContext == null)
                BindingContext = attachedEntry.BindingContext;


            IsValid = CompareWith.Text == attachedEntry.Text;

            if (string.IsNullOrWhiteSpace(attachedEntry.Text))
                ValidationResult = ValidationResult.NoValue;
            else
                ValidationResult = IsValid ? ValidationResult.IsValid : ValidationResult.IsInvalid;

            attachedEntry.TextColor = IsValid ? Color.Black : Color.Red;
        }
    }
}
