using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FollowTheRainbow
{
    class FollowTheRainbow : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new FollowTheRainbow());
        }

        public FollowTheRainbow()
        {
            Title = "Follow the Rainbow";

            var brush = new LinearGradientBrush();
            brush.StartPoint = new Point(0, 0);
            brush.EndPoint = new Point(1, 0);
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
