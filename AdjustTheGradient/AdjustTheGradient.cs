using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AdjustTheGradient
{
    class AdjustTheGradient : Window
    {
        LinearGradientBrush brush;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new AdjustTheGradient());
        }

        public AdjustTheGradient()
        {
            Title = "Adjust the Gradient";
            SizeChanged += WindowOnSizeChanged;

            brush = new LinearGradientBrush(Colors.Red, Colors.Blue, 0);
            brush.MappingMode = BrushMappingMode.Absolute;
            Background = brush;
        }

        private void WindowOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = ActualWidth - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            double height = ActualHeight - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight - SystemParameters.CaptionHeight;

            var center = new Point(width / 2, height / 2);
            var diag = new Vector(width, -height);
            var perp = new Vector(diag.Y, -diag.X);

            perp.Normalize();
            perp *= width * height / diag.Length;

            brush.StartPoint = center + perp;
            brush.EndPoint = center - perp;
        }
    }
}
