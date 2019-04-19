using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ListColorShapes
{
    class ListColorShapes : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListColorShapes());
        }

        public ListColorShapes()
        {
            Title = "List Color Shapes";

            var lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            foreach (var prop in typeof(Brushes).GetProperties())
            {
                var ellip = new Ellipse();
                ellip.Width = 100;
                ellip.Height = 25;
                ellip.Margin = new Thickness(10, 5, 0, 5);
                ellip.Fill = (Brush)prop.GetValue(null, null);
                lstbox.Items.Add(ellip);
            }
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lstbox = sender as ListBox;

            if (lstbox.SelectedIndex != -1)
                Background = (lstbox.SelectedItem as Shape).Fill;
        }
    }
}
