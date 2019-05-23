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

namespace Spiral
{
    public class Spiral : Window
    {
        const int revs = 20;
        const int numpts = 1000 * revs;
        Polyline poly;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new Spiral());
        }

        public Spiral()
        {
            Title = "Spiral";

            var canv = new Canvas();
            canv.SizeChanged += CanvasOnSizeChanged;
            Content = canv;

            poly = new Polyline();
            poly.Stroke = SystemColors.WindowTextBrush;
            canv.Children.Add(poly);

            var pts = new Point[numpts];
            for (var i = 0; i < numpts; i++)
            {
                var angle = i * 2d * Math.PI / (numpts / revs);
                var scale = 250 * (1d - (double)i / numpts);
                pts[i].X = scale * Math.Cos(angle);
                pts[i].Y = scale * Math.Sin(angle);
            }
            poly.Points = new PointCollection(pts);
        }

        private void CanvasOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(poly, e.NewSize.Width / 2);
            Canvas.SetTop(poly, e.NewSize.Height / 2);
        }
    }
}
