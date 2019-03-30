using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CircleTheRainbow
{
    class CircleTheRainbow : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CircleTheRainbow());
        }

        public CircleTheRainbow()
        {
            Title = "Follow the Rainbow";

            var brush = new RadialGradientBrush();
            brush.GradientOrigin = new Point(0.75, 0.75);

            Background = brush;

            brush.GradientStops.Add(new GradientStop(Colors.Red, 0));
            brush.GradientStops.Add(new GradientStop(Colors.Orange, .17));
            brush.GradientStops.Add(new GradientStop(Colors.Yellow, .33));
            brush.GradientStops.Add(new GradientStop(Colors.Green, .5));
            brush.GradientStops.Add(new GradientStop(Colors.Blue, .67));
            brush.GradientStops.Add(new GradientStop(Colors.Indigo, .84));
            brush.GradientStops.Add(new GradientStop(Colors.Violet, 1));
        }
    }
}
