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

namespace ListNamedBrushes
{
    class ListNamedBrushes : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListNamedBrushes());
        }

        public ListNamedBrushes()
        {
            Title = "List Named Brushes";

            var lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            Content = lstbox;

            lstbox.ItemsSource = NamedBrush.All;
            lstbox.DisplayMemberPath = "Name";
            lstbox.SelectedValuePath = "Brush";

            lstbox.SetBinding(Selector.SelectedValueProperty, nameof(Background));
            lstbox.DataContext = this;
        }
    }
}
