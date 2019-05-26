using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace RotatedText
{
    public class RotatedText : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new RotatedText());
        }

        public RotatedText()
        {
            Title = "Rotated Text";

            var canv = new Canvas();
            Content = canv;

            for (var angle = 0; angle < 360; angle += 20)
            {
                var txtblk = new TextBlock();
                txtblk.FontFamily = new FontFamily("Arial");
                txtblk.FontSize = 24;
                txtblk.Text = "     Rotated Text";
                txtblk.RenderTransformOrigin = new Point(0, .5);
                txtblk.RenderTransform = new RotateTransform(angle);

                canv.Children.Add(txtblk);
                Canvas.SetLeft(txtblk, 200);
                Canvas.SetTop(txtblk, 200);
            }
        }
    }
}
