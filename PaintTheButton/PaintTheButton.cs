using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintTheButton
{
    class PaintTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new PaintTheButton());
        }

        public PaintTheButton()
        {
            Title = "Paint the Button";

            var btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            Content = btn;

            var canvas = new Canvas();
            canvas.Width = 144;
            canvas.Height = 144;
            btn.Content = canvas;

            var rect = new Rectangle();
            rect.Width = canvas.Width;
            rect.Height = canvas.Height;
            rect.RadiusX = 24;
            rect.RadiusY = 24;
            rect.Fill = Brushes.Blue;
            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);

            var poly = new Polygon();
            poly.Fill = Brushes.Yellow;
            poly.Points = new PointCollection();

            for (var i = 0; i < 5; i++)
            {
                var angle = i * 4 * Math.PI / 5;
                var pt = new Point(48 * Math.Sin(angle), -48 * Math.Cos(angle));

                poly.Points.Add(pt);
            }
            canvas.Children.Add(poly);
            Canvas.SetLeft(poly, canvas.Width / 2);
            Canvas.SetTop(poly, canvas.Height / 2);
        }
    }
}
