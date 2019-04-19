using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ListColorsElegantly
{
    class ListColorsElegantly : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListColorsElegantly());
        }

        public ListColorsElegantly()
        {
            Title = "List Colors Elegantly";

            var lstbox = new ColorListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            lstbox.SelectedColor = SystemColors.WindowColor;
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Background = new SolidColorBrush((sender as ColorListBox).SelectedColor);
        }
    }
}
