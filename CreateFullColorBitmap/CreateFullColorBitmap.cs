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

namespace CreateFullColorBitmap
{
    public class CreateFullColorBitmap : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CreateFullColorBitmap());
        }

        public CreateFullColorBitmap()
        {
            Title = "Create Full-Color Bitmap";

            var array = new int[256 << 8];

            for (var x = 0; x < 256; x++)
                for (var y = 0; y < 256; y++)
                    array[(y << 8) + x] = x | (y << 16);

            var bitmap = BitmapSource.Create(256, 256, 96, 96, PixelFormats.Bgr32, null, array, 256 << 2);
            var img = new Image();
            img.Source = bitmap;

            Content = img;
        }
    }
}
