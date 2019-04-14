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

namespace SelectColor
{
    class SelectColor : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SelectColor());
        }

        public SelectColor()
        {
            Title = "Select Color";
            SizeToContent = SizeToContent.WidthAndHeight;

            var stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            var btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            var clrGrid = new ColorGrid();
            clrGrid.Margin = new Thickness(24);
            clrGrid.VerticalAlignment = VerticalAlignment.Center;
            clrGrid.HorizontalAlignment = HorizontalAlignment.Center;
            clrGrid.SelectedColorChanged += ColorGridOnSelectedColorChanged;
            stack.Children.Add(clrGrid);

            btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);
        }

        private void ColorGridOnSelectedColorChanged(object sender, EventArgs e)
        {
            var clrGrid = sender as ColorGrid;
            Background = new SolidColorBrush(clrGrid.SelectedColor);
        }
    }
}
