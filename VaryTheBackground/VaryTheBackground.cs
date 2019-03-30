using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace VaryTheBackground
{
    class VaryTheBackground : Window
    {
        SolidColorBrush brush;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new VaryTheBackground());
        }

        public VaryTheBackground()
        {
            brush = Brushes.Black.Clone();
            Title = "Vary the Background";
            Width = 384;
            Height = 384;
            Background = brush;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var width = ActualWidth - 2d * SystemParameters.ResizeFrameVerticalBorderWidth;
            var height = ActualHeight - 2d * SystemParameters.ResizeFrameHorizontalBorderHeight - SystemParameters.CaptionHeight;

            var mouse = e.GetPosition(this);
            var center = new Point(width / 2d, height / 2d);
            var distance = mouse - center;
            var angle = Math.Atan2(distance.Y, distance.X);
            var ellipse = new Vector(width / 2d * Math.Cos(angle), height / 2d * Math.Sin(angle));
            var level = (byte)(255d * (1 - Math.Min(1d, distance.Length / ellipse.Length)));
            var color = brush.Color;
            color.R = color.G = color.B = level;
            brush.Color = color;

        }
    }
}
