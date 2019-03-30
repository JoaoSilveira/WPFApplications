using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace RotateTheGradientOrigin
{
    class RotateTheGradientOrigin : Window
    {
        RadialGradientBrush brush;
        double angle;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new RotateTheGradientOrigin());
        }

        public RotateTheGradientOrigin()
        {
            Title = "Rotate the Gradient Origin";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 384;
            Height = 384;

            brush = new RadialGradientBrush(Colors.White, Colors.Blue);
            brush.Center = brush.GradientOrigin = new Point(.5, .5);
            brush.RadiusX = brush.RadiusY = .1;
            brush.SpreadMethod = GradientSpreadMethod.Repeat;
            Background = brush;

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += TimerOnClick;
            timer.Start();
        }

        private void TimerOnClick(object sender, EventArgs e)
        {
            var point = new Point(.5 + .05 * Math.Cos(angle), .5 + .05 * Math.Sin(angle));
            brush.GradientOrigin = point;
            angle += Math.PI / 6;
        }
    }
}
