using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GradiateTheBrush
{
    class GradiateTheBrush : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new GradiateTheBrush());
        }

        public GradiateTheBrush()
        {
            Title = "Gradiate the Brush";

            var brush = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(0, 0), new Point(.25, .25));
            brush.SpreadMethod = GradientSpreadMethod.Reflect;
            Background = brush;
        }
    }
}
