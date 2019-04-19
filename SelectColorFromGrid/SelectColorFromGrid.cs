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

namespace SelectColorFromGrid
{
    class SelectColorFromGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SelectColorFromGrid());
        }

        public SelectColorFromGrid()
        {
            Title = "Select Color from Grid";
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

            var clrgrid = new ColorGridBox();
            clrgrid.Margin = new Thickness(24);
            clrgrid.HorizontalAlignment = HorizontalAlignment.Center;
            clrgrid.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(clrgrid);

            clrgrid.SetBinding(Selector.SelectedValueProperty, nameof(Background));
            clrgrid.DataContext = this;

            btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);
        }
    }
}
