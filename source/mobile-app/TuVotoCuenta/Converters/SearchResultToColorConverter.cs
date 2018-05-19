using System;
using System.Globalization;
using TuVotoCuenta.Domain;
using Xamarin.Forms;

namespace TuVotoCuenta.Converters
{
    public class SearchResultToColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var current = value as SearchResult;
            if (current != null)
            {
                bool morePositives = current.UpVotes > current.DownVotes;
                bool equalPositivesAndNegatives = current.UpVotes == current.DownVotes;
                if (equalPositivesAndNegatives)
                    return Color.Gray;
                return morePositives ? Color.Green : Color.Red;
            }
            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
