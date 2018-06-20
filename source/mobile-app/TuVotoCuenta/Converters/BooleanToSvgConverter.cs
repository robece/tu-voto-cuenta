using System;
using System.Globalization;
using FFImageLoading.Svg.Forms;
using TuVotoCuenta.Converters;
using Xamarin.Forms;


namespace TuVotoCuenta.Converters
{
    [ValueConversion(typeof(bool), typeof(ImageSource))]
    public class BooleanToSvgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool == false)
            {
                return default(ImageSource);
            }

            var input = (bool)value;

            var result = input ? SvgImageSource.FromFile("svgdarkvoteup.svg") :
                                               SvgImageSource.FromFile("svgdarkvotedown.svg");

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}