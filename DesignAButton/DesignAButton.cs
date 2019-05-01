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
using System.Windows.Shapes;

namespace DesignAButton
{
    class DesignAButton : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DesignAButton());
        }

        public DesignAButton()
        {
            Title = "Design a Button";

            var btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;

            var stack = new StackPanel();
            btn.Content = stack;

            stack.Children.Add(ZigZag(10));

            var uri = new Uri("pack://application:,,/book06.png");
            var bitmap = new BitmapImage(uri);

            Image img = new Image();
            img.Margin = new Thickness(0, 10, 0, 0);
            img.Source = bitmap;
            img.Stretch = Stretch.Uniform;
            img.Width = 32;
            stack.Children.Add(img);

            var lbl = new Label();
            lbl.Content = "_Read books!";
            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
            stack.Children.Add(lbl);
        }

        private Polyline ZigZag(int offset)
        {
            var poly = new Polyline();
            poly.Stroke = SystemColors.ControlTextBrush;
            poly.Points = new PointCollection();

            for (var i = 0; i <= 100; i += 10)
            {
                poly.Points.Add(new Point(i, (i + offset) % 20));
            }

            return poly;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The button has been clicked", Title);
        }
    }
}
