using System;
using System.Text.RegularExpressions;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Behaviors
{
    public class EmailValidatorBehavior : Behavior<Entry>
    {
        private const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            IsValid = (Regex.IsMatch(args.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            if (((Entry)sender).BindingContext is SignUpViewModel)
                SignUpViewModel.EmailValidation = IsValid;
            else if (((Entry)sender).BindingContext is SignInViewModel)
                SignInViewModel.EmailValidation = IsValid;
            else if (((Entry)sender).BindingContext is ForgotPasswordViewModel)
                ForgotPasswordViewModel.EmailValidation = IsValid;
        }
    }
}