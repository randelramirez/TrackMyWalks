using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TrackMyWalks.ValueConverters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Declare our Difficulty Level based on the value
            // parameter
            var DiffLevel = (string)value;

            // Determine the type of URL to return based on the
            // difficulty level
            switch (DiffLevel)
            {
                case "Easy":
                    return "http://www.trailhiking.com.au/wp-content/uploads/2013/08/g1.jpeg";
                case "Medium":
                    return "http://www.trailhiking.com.au/wp-content/uploads/2013/08/g2.jpeg";
                case "Hard":
                    return "http://www.trailhiking.com.au/wp-content/uploads/2013/08/g3.jpeg";
                case "Extreme":
                    return "http://www.trailhiking.com.au/wp-content/uploads/2013/08/g5.jpeg";
                default:
                    return "http://www.trailhiking.com.au/wp-content/uploads/2013/08/g1.jpeg";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
