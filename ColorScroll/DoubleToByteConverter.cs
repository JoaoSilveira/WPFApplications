using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ColorScroll
{
    [ValueConversion(typeof(double), typeof(byte))]
    public class DoubleToByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (byte)(double)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (double)value;
    }
}
