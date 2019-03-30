using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ClickTheGradientCenter
{
    class ClickTheGradientCenter : Window
    {
        RadialGradientBrush brush;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ClickTheGradientCenter());
        }

        public ClickTheGradientCenter()
        {
            Title = "Click the Gradient Center";

            brush = new RadialGradientBrush(Colors.White, Colors.Red);
            brush.RadiusX = .1;
            brush.RadiusY = .1;
            brush.SpreadMethod = GradientSpreadMethod.Repeat;

            Background = brush;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var width = ActualWidth - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            var height = ActualHeight - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight - SystemParameters.CaptionHeight;

            var mouse = e.GetPosition(this);
            mouse.X /= width;
            mouse.Y /= height;

            if (e.ChangedButton == MouseButton.Left)
            {
                brush.Center = mouse;
                brush.GradientOrigin = mouse;
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                brush.GradientOrigin = mouse;
            }
        }
    }
}
