using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TextGeometryDemo
{
    public class TextGeometry
    {
        public string Text { get; set; } = string.Empty;

        public FontFamily FontFamily { get; set; } = new FontFamily();

        public FontStyle FontStyle { get; set; } = FontStyles.Normal;

        public FontWeight FontWeight { get; set; } = FontWeights.Normal;

        public FontStretch FontStretch { get; set; } = FontStretches.Normal;

        public double FontSize { get; set; } = 24;

        public Point Origin { get; set; } = new Point();

        public Geometry Geometry
        {
            get
            {
                var formtxt = new FormattedText(
                    Text,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface(
                        FontFamily,
                        FontStyle,
                        FontWeight,
                        FontStretch),
                    FontSize,
                    Brushes.Black);

                return formtxt.BuildGeometry(Origin);
            }
        }

        public PathGeometry PathGeometry
        {
            get
            {
                return PathGeometry.CreateFromGeometry(Geometry);
            }
        }
    }
}
