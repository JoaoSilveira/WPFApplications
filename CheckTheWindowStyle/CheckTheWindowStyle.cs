using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CheckTheWindowStyle
{
    class CheckTheWindowStyle : Window
    {
        MenuItem itemChecked;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CheckTheWindowStyle());
        }

        public CheckTheWindowStyle()
        {
            Title = "Check the Window Style";

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

            var itemStyle = new MenuItem();
            itemStyle.Header = "_Style";
            menu.Items.Add(itemStyle);

            itemStyle.Items.Add(CreateMenuItem("_No border or caption", WindowStyle.None));

            itemStyle.Items.Add(CreateMenuItem("_Single-border window", WindowStyle.SingleBorderWindow));

            itemStyle.Items.Add(CreateMenuItem("3_D-border window", WindowStyle.ThreeDBorderWindow));

            itemStyle.Items.Add(CreateMenuItem("_Tool window", WindowStyle.ToolWindow));
        }

        private object CreateMenuItem(string header, WindowStyle style)
        {
            var item = new MenuItem();
            item.Header = header;
            item.Tag = style;
            item.IsChecked = WindowStyle == style;
            item.Click += StyleOnClick;

            if (item.IsChecked)
                itemChecked = item;

            return item;
        }

        private void StyleOnClick(object sender, RoutedEventArgs e)
        {
            itemChecked.IsChecked = false;
            itemChecked = e.Source as MenuItem;
            itemChecked.IsChecked = true;

            WindowStyle = (WindowStyle)itemChecked.Tag;
        }
    }
}
