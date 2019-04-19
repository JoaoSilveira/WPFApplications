using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ListColorNames
{
    class ListColorNames : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListColorNames());
        }

        public ListColorNames()
        {
            Title = "List Color Names";

            var lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            foreach (var prop in typeof(Colors).GetProperties())
                lstbox.Items.Add(prop.Name);
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lstbox = sender as ListBox;
            var str = lstbox.SelectedItem as string;

            if (str != null)
            {
                var clr = (Color)typeof(Colors).GetProperty(str).GetValue(null, null);
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
