using System;
using TuVotoCuenta.Helpers;
using Xamarin.Forms;

namespace TuVotoCuenta.Behaviors
{
    public class EntryValidationBehavior : Behavior<Entry>
    {

        readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(EntryValidationBehavior), int.MaxValue);

        public int MaxLength
        {
            get { return (int)base.GetValue(MaxLengthProperty); }
            set
            { base.SetValue(MaxLengthProperty, value); }
        }
              

        readonly BindableProperty ValidationTypeProperty =
            BindableProperty.Create(nameof(ValidationType), typeof(ValidationType), typeof(EntryValidationBehavior), ValidationType.None);

        public ValidationType ValidationType
        {
            get { return (ValidationType)base.GetValue(ValidationTypeProperty); }
            set
            { base.SetValue(ValidationTypeProperty, value); }
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

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            BindingContext = null;
            bindable.TextChanged -= Bindable_TextChanged;

        }

        void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OldTextValue != e.NewTextValue)
            {
                Entry entry = ((Entry)sender);
                if (BindingContext == null)
                    BindingContext = entry.BindingContext;
                var b = entry.BindingContext;

                if (entry.Text.Length > MaxLength)
                {
                    entry.Text = e.OldTextValue;
                }


                ValidationResult = ValidationHelper.ValidateString(ValidationType, entry.Text);
                IsValid = ValidationResult == ValidationResult.IsValid ? true : false;
                ((Entry)sender).TextColor = ValidationResult == ValidationResult.IsValid || ValidationResult == ValidationResult.NoValue ? Color.Black : Color.Red;
            }
        }
    }
}
