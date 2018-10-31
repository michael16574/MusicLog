using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MusicLogWPF
{
    public class UnixTimeToLocalTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                string format = "M/d/y H:mm";
                string time = "";

                if ((int)value != 0)
                {
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((int)value);
                    time = dateTimeOffset.DateTime.ToLocalTime().ToString(format);
                }
                else
                {
                    time = "N/A";
                }

                return time;
            }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
