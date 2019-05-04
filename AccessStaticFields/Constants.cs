using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AccessStaticFields
{
    public static class Constants
    {
        public static readonly FontFamily fntfam = new FontFamily("Times New Roman Italic");

        public static double FontSize => 72d / .75;

        public static readonly LinearGradientBrush brush = new LinearGradientBrush(Colors.LightGray, Colors.DarkGray, new Point(), new Point(1, 1));
    }
}
