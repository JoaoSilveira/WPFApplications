using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DrawGraphicsOnBitmap
{
    public class DrawGraphicsOnBitmap : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DrawGraphicsOnBitmap());
        }

        public DrawGraphicsOnBitmap()
        {
            Title = "Draw Graphics on Bitmap";
            Background = Brushes.Khaki;

            var renderbitmap = new RenderTargetBitmap(100, 100, 96, 96, PixelFormats.Default);
            var drawvis = new DrawingVisual();
            using (var dc = drawvis.RenderOpen())
            {
                dc.DrawRoundedRectangle(Brushes.Blue, new Pen(Brushes.Red, 10), new Rect(25, 25, 50, 50), 10, 10);
            }
            renderbitmap.Render(drawvis);

            var img = new Image();
            img.Source = renderbitmap;

            Content = img;
        }
    }
}
