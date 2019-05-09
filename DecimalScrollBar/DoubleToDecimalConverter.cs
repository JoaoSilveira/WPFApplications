﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace DecimalScrollBar
{
    [ValueConversion(typeof(double), typeof(decimal))]
    public class DoubleToDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var num = (decimal)(double)value;

            if (parameter is null)
                return num;

            return decimal.Round(num, int.Parse(parameter as string));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => decimal.ToDouble((decimal)value);
    }
}
