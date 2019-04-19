using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ListNamedColors
{
    class ListNamedColors : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListNamedColors());
        }

        public ListNamedColors()
        {
            Title = "List Named Colors";

            var lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            lstbox.ItemsSource = NamedColor.All;
            lstbox.DisplayMemberPath = "Name";
            lstbox.SelectedValuePath = "Color";
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lstbox = sender as ListBox;

            if (lstbox.SelectedValue != null)
                Background = new SolidColorBrush((Color)lstbox.SelectedValue);
        }
    }
}
