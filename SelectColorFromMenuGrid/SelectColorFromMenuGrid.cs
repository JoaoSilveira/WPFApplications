using SelectColorFromGrid;
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

namespace SelectColorFromMenuGrid
{
    class SelectColorFromMenuGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SelectColorFromMenuGrid());
        }

        public SelectColorFromMenuGrid()
        {
            Title = "Select Color from Menu Grid";

            var dock = new DockPanel();
            Content = dock;

            var menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            var text = new TextBlock();
            text.Text = Title;
            text.FontSize = 32;
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);

            var itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);

            var itemForeground = new MenuItem();
            itemForeground.Header = "_Foreground";
            itemColor.Items.Add(itemForeground);

            var clrbox = new ColorGridBox();
            clrbox.SetBinding(Selector.SelectedValueProperty, "Foreground");
            clrbox.DataContext = this;
            itemForeground.Items.Add(clrbox);

            var itemBackground = new MenuItem();
            itemBackground.Header = "_Background";
            itemColor.Items.Add(itemBackground);

            clrbox = new ColorGridBox();
            clrbox.SetBinding(Selector.SelectedValueProperty, "Background");
            clrbox.DataContext = this;
            itemBackground.Items.Add(clrbox);
        }
    }
}
