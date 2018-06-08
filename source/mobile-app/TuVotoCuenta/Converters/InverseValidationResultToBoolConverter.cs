using System;
using System.Globalization;
using TuVotoCuenta.Helpers;
using Xamarin.Forms;

namespace TuVotoCuenta.Converters
{
    public class InverseValidationResultToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var current = (ValidationResult)value;

            switch (current)
            {
                case ValidationResult.IsValid:      return false;
                case ValidationResult.IsInvalid:    return true;
                case ValidationResult.NoValue:      return false;
                default:    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
