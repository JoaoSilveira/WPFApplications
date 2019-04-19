using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ListWithListBoxItems
{
    class ListWithListBoxItems : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListWithListBoxItems());
        }

        public ListWithListBoxItems()
        {
            Title = "List with ListBoxItem";

            var lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            foreach (var prop in typeof(Colors).GetProperties())
            {
                var clr = (Color)prop.GetValue(null, null);

                var isWhite = .222 * clr.R + .707 * clr.G + .071 * clr.B > 128;

                var item = new ListBoxItem();
                item.Content = prop.Name;
                item.Background = new SolidColorBrush(clr);
                item.Foreground = isWhite ? Brushes.Black : Brushes.White;
                item.HorizontalContentAlignment = HorizontalAlignment.Center;
                item.Padding = new Thickness(2);
                lstbox.Items.Add(item);
            }
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lstbox = sender as ListBox;
            ListBoxItem item;
            
            if (e.RemovedItems.Count > 0)
            {
                item = e.RemovedItems[0] as ListBoxItem;
                var str = item.Content as string;
                item.Content = str.Substring(2, str.Length - 4);
                item.FontWeight = FontWeights.Regular;
            }
            if (e.AddedItems.Count > 0)
            {
                item = e.AddedItems[0] as ListBoxItem;
                var str = item.Content as string;
                item.Content = $"[ {str} ]";
                item.FontWeight = FontWeights.Bold;
            }

            item = lstbox.SelectedItem as ListBoxItem;

            if (item != null)
                Background = item.Background;
        }
    }
}
