using System;
using System.Globalization;
using Xamarin.Forms;


namespace TuVotoCuenta.Converters
{
    [ValueConversion(typeof(string), typeof(Uri))]
    public class StringToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string == false)
            {
                return default(Uri);
            }

            var input = (string)value;

            Uri uri = new Uri($"https://rinkeby.etherscan.io/tx/{input}");

            return uri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}