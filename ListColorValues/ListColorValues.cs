using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ListColorValues
{
    class ListColorValues : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListColorValues());
        }

        public ListColorValues()
        {
            Title = "List Color Values";

            var lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            foreach (var prop in typeof(Colors).GetProperties())
                lstbox.Items.Add(prop.GetValue(null, null));
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lstbox = sender as ListBox;

            if (lstbox.SelectedIndex != -1)
                Background = new SolidColorBrush((Color)lstbox.SelectedItem);
        }
    }
}
