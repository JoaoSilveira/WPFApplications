using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapeAnEllipse
{
    class ShapeAnEllipse : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ShapeAnEllipse());
        }

        public ShapeAnEllipse()
        {
            Title = "Shape an Ellipse";

            var ellipse = new Ellipse();
            ellipse.Fill = Brushes.AliceBlue;
            ellipse.StrokeThickness = 24;
            ellipse.Stroke = new LinearGradientBrush(Colors.CadetBlue, Colors.Chocolate, new Point(1, 0), new Point(0, 1));

            Content = ellipse;
        }
    }
}
