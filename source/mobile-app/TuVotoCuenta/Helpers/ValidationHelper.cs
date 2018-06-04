using System;
using System.Text.RegularExpressions;

namespace TuVotoCuenta.Helpers
{
    public enum ValidationType
    {
        Email,
        UserName,
        PhoneNumber,
        Password,
        Length,
        None
    }

    public enum ValidationResult
    {
        IsValid,
        IsInvalid,
        NoValue
    }

    public static class ValidationHelper
    {

        public const string EMAIL = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string PHONE = @"\(?\+[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})? ?(\w{1,10}\s?\d{1,6})?";
        public const string PASSWORD = @"^[A-Za-z\d!·@_.$%*&()=?¿]{6,10}$";
        public const string USERNAME = @"^[A-Za-z\d]{6,12}$";

        public static ValidationResult ValidateString(ValidationType validationType, string valueToValidate)
        {

            if (string.IsNullOrEmpty(valueToValidate))
                return ValidationResult.NoValue;

            Regex regex = null;

            switch (validationType)
            {
                case ValidationType.Email:
                    regex = new Regex(EMAIL);
                    break;
                case ValidationType.Password:
                    regex = new Regex(PASSWORD);
                    break;
                case ValidationType.UserName:
                    regex = new Regex(USERNAME);
                    break;
                case ValidationType.PhoneNumber:
                    regex = new Regex(PHONE);
                    break;
                case ValidationType.None:
                    return  ValidationResult.NoValue;
                default:
                    throw new InvalidCastException("OperationType is invalid");

            }

            Match match = regex.Match(valueToValidate);
            bool isValid = match.Success;
            return string.IsNullOrWhiteSpace(valueToValidate) ? ValidationResult.NoValue : isValid ? ValidationResult.IsValid : ValidationResult.IsInvalid;
        }
    }
}
