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

namespace CheckTheColor
{
    class CheckTheColor : Window
    {
        TextBlock text;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CheckTheColor());
        }

        public CheckTheColor()
        {
            Title = "Check the Color";

            var dock = new DockPanel();
            Content = dock;

            var menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            text = new TextBlock();
            text.Text = Title;
            text.TextAlignment = TextAlignment.Center;
            text.FontSize = 32;
            text.Background = SystemColors.WindowBrush;
            text.Foreground = SystemColors.WindowTextBrush;
            dock.Children.Add(text);

            var itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);

            var itemForeground = new MenuItem();
            itemForeground.Header = "_Foreground";
            itemForeground.SubmenuOpened += ForegroundOnOpened;
            itemColor.Items.Add(itemForeground);

            FillWithColors(itemForeground, ForegroundOnClick);

            var itemBackground = new MenuItem();
            itemBackground.Header = "_Background";
            itemBackground.SubmenuOpened += BackgroundOnOpened;
            itemColor.Items.Add(itemBackground);

            FillWithColors(itemBackground, BackgroundOnClick);
        }

        private void FillWithColors(MenuItem itemParent, RoutedEventHandler handler)
        {
            foreach (var prop in typeof(Colors).GetProperties())
            {
                var clr = (Color)prop.GetValue(null, null);

                var count = 0;

                count += clr.R == 0 || clr.R == 255 ? 1 : 0;
                count += clr.G == 0 || clr.G == 255 ? 1 : 0;
                count += clr.B == 0 || clr.B == 255 ? 1 : 0;

                if (clr.A == 255 && count > 1)
                {
                    var item = new MenuItem();
                    item.Header = $"_{prop.Name}";
                    item.Tag = clr;
                    item.Click += handler;
                    itemParent.Items.Add(item);
                }
            }
        }

        private void ForegroundOnOpened(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in (sender as MenuItem).Items)
                item.IsChecked = (text.Foreground as SolidColorBrush).Color == (Color)item.Tag;
        }

        private void BackgroundOnOpened(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in (sender as MenuItem).Items)
                item.IsChecked = (text.Background as SolidColorBrush).Color == (Color)item.Tag;
        }

        private void ForegroundOnClick(object sender, RoutedEventArgs e)
        {
            text.Foreground = new SolidColorBrush((Color)(sender as MenuItem).Tag);
        }

        private void BackgroundOnClick(object sender, RoutedEventArgs e)
        {
            text.Background = new SolidColorBrush((Color)(sender as MenuItem).Tag);
        }
    }
}
