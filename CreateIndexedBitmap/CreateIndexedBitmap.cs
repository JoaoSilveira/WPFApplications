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

namespace CreateIndexedBitmap
{
    public class CreateIndexedBitmap : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CreateIndexedBitmap());
        }

        public CreateIndexedBitmap()
        {
            Title = "Create Indexed Bitmap";

            var colors = new List<Color>();
            for (var r = 0; r < 256; r += 17)
                for (var b = 0; b < 256; b += 17)
                    colors.Add(Color.FromRgb((byte)r, 0, (byte)b));

            var palette = new BitmapPalette(colors);
            var array = new byte[256 << 8];

            for (var x = 0; x < 256; x++)
                for (var y = 0; y < 256; y++)
                    array[(y << 8) + x] = (byte)(((int)Math.Round(y / 17.0) << 4) | (int)Math.Round(x / 17.0));

            var bitmap = BitmapSource.Create(256, 256, 96, 96, PixelFormats.Indexed8, palette, array, 256);
            var img = new Image();
            img.Source = bitmap;

            Content = img;
        }
    }
}
