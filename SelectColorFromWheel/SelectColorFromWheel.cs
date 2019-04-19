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

namespace SelectColorFromWheel
{
    class SelectColorFromWheel : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SelectColorFromWheel());
        }

        public SelectColorFromWheel()
        {
            Title = "Select Color From Wheel";
            SizeToContent = SizeToContent.WidthAndHeight;

            var stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            var btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);

            var clrwheel = new ColorWheel();
            clrwheel.Margin = new Thickness(24);
            clrwheel.HorizontalAlignment = HorizontalAlignment.Center;
            clrwheel.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(clrwheel);

            clrwheel.SetBinding(Selector.SelectedValueProperty, nameof(Background));
            clrwheel.DataContext = this;

            btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);
        }
    }
}
