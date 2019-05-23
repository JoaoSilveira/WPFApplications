using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SineWave
{
    public class SineWave : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SineWave());
        }

        public SineWave()
        {
            Title = "Sine Wave";

            var poly = new Polyline();
            poly.VerticalAlignment = VerticalAlignment.Center;
            poly.Stroke = SystemColors.WindowTextBrush;
            poly.StrokeThickness = 2;
            Content = poly;

            for (var i = 0; i < 2000; i++)
                poly.Points.Add(new Point(i, 96d * (1 - Math.Sin(i * Math.PI / 192d))));
        }
    }
}
