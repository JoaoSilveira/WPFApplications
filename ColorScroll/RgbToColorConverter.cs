using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ColorScroll
{
    public class RgbToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var clr = Color.FromRgb((byte)(double)values[0], (byte)(double)values[1], (byte)(double)values[2]);

            if (targetType == typeof(Color))
                return clr;

            if (targetType == typeof(Brush))
                return new SolidColorBrush(clr);

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            Color clr;
            if (value is Color)
                clr = (Color)value;
            else if (value is SolidColorBrush)
                clr = (value as SolidColorBrush).Color;
            else
                return null;

            return new object[] { clr.R, clr.G, clr.B };
        }
    }
}
