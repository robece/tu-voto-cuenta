using System;
using System.Globalization;
using Xamarin.Forms;

namespace TuVotoCuenta.Converters
{
    public class IntToStringConverter: IValueConverter
    {
        public IntToStringConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            var intvalue = (int)value;

            if (intvalue > -1)
            {
                return value.ToString();
            }


            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = -1;

            if (string.IsNullOrWhiteSpace(value?.ToString()) || !int.TryParse(value?.ToString(), out result))
                return result;
            return result;
        }
    }
}
