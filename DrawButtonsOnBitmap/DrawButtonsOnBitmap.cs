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
using System.Windows.Media.Imaging;

namespace DrawButtonsOnBitmap
{
    public class DrawButtonsOnBitmap : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DrawButtonsOnBitmap());
        }

        public DrawButtonsOnBitmap()
        {
            Title = "Draw Buttons on Bitmap";

            var unigrid = new UniformGrid();
            unigrid.Columns = 4;

            for (var i = 0; i < 32; i++)
            {
                var btn = new ToggleButton();
                btn.Width = 96;
                btn.Height = 24;
                btn.IsChecked = (i < 4 | i > 27) ^ (i % 4 == 0 | i % 4 == 3);
                unigrid.Children.Add(btn);
            }

            unigrid.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var szGrid = unigrid.DesiredSize;
            unigrid.Arrange(new Rect(new Point(), szGrid));

            var renderbitmap = new RenderTargetBitmap((int)Math.Ceiling(szGrid.Width), (int)Math.Ceiling(szGrid.Height), 96, 96, PixelFormats.Default);
            renderbitmap.Render(unigrid);

            var img = new Image();
            img.Source = renderbitmap;
            Content = img;
        }
    }
}
