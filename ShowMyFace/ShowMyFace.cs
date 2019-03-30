using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShowMyFace
{
    class ShowMyFace : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ShowMyFace());
        }

        public ShowMyFace()
        {
            Title = "Show My Face";


            var uri = new Uri("http://www.charlespetzold.com/PetzoldTattoo.jpg");
            var bitmap = new BitmapImage(uri);
            var image = new Image();
            image.Source = bitmap;
            Content = image;
        }
    }
}
