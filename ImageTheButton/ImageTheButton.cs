using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageTheButton
{
    class ImageTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ImageTheButton());
        }

        public ImageTheButton()
        {
            Title = "Image the Button";

            var uri = new Uri("pack://application:,,/munch.jpg");
            var bitmap = new BitmapImage(uri);
            var image = new Image();
            image.Source = bitmap;
            image.Stretch = Stretch.None;

            var button = new Button();
            button.Content = image;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;

            Content = button;
        }
    }
}
