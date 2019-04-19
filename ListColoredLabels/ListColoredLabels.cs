using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ListColoredLabels
{
    class ListColoredLabels : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListColoredLabels());
        }

        public ListColoredLabels()
        {
            Title = "List Colored Labels";

            var lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            foreach (var prop in typeof(Colors).GetProperties())
            {
                var clr = (Color)prop.GetValue(null, null);

                var isWhite = .222 * clr.R + .707 * clr.G + .071 * clr.B > 128;
                var lbl = new Label();
                lbl.Content = prop.Name;
                lbl.Background = new SolidColorBrush(clr);
                lbl.Foreground = isWhite ? Brushes.Black : Brushes.White;
                lbl.Width = 100;
                lbl.Margin = new Thickness(15, 0, 0, 0);
                lbl.Tag = clr;
                lstbox.Items.Add(lbl);
            }
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbl = (sender as ListBox).SelectedItem as Label;

            if (lbl != null)
                Background = new SolidColorBrush((Color)lbl.Tag);
        }
    }
}
