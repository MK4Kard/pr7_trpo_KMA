using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace pr7_trpo_1_KMA.Converters
{
    class DiscountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            if (value is long l)
                return System.Convert.ToInt64(l);

            if (value is DateTime d)
                return System.Convert.ToDateTime(d);

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (long.TryParse(value.ToString(), out long result))
                return result;

            if (DateTime.TryParse(value.ToString(), out DateTime resultD))
                return resultD;

            return null;
        }
    }
}
