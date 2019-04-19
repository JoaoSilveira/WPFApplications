using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PeruseTheMenu
{
    class PeruseTheMenu : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new PeruseTheMenu());
        }

        public PeruseTheMenu()
        {
            Title = "Peruse the Menu";

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

            var itemFile = new MenuItem();
            itemFile.Header = "_File";
            menu.Items.Add(itemFile);

            var itemNew = new MenuItem();
            itemNew.Header = "_New";
            itemNew.Click += UninplementedOnClick;
            itemFile.Items.Add(itemNew);

            var itemOpen = new MenuItem();
            itemOpen.Header = "_Open";
            itemOpen.Click += UninplementedOnClick;
            itemFile.Items.Add(itemOpen);

            var itemSave = new MenuItem();
            itemSave.Header = "_Save";
            itemSave.Click += UninplementedOnClick;
            itemFile.Items.Add(itemSave);

            itemFile.Items.Add(new Separator());

            var itemExit = new MenuItem();
            itemExit.Header = "E_xit";
            itemExit.Click += ExitOnClicked;
            itemFile.Items.Add(itemExit);

            var itemWindow = new MenuItem();
            itemWindow.Header = "_Window";
            menu.Items.Add(itemWindow);

            var itemTaskbar = new MenuItem();
            itemTaskbar.Header = "_Show in Taskbar";
            itemTaskbar.IsCheckable = true;
            itemTaskbar.IsChecked = ShowInTaskbar;
            itemTaskbar.Click += TaskbarOnClick;
            itemWindow.Items.Add(itemTaskbar);

            var itemSize = new MenuItem();
            itemSize.Header = "Size to _Content";
            itemSize.IsCheckable = true;
            itemSize.IsChecked = SizeToContent == SizeToContent.WidthAndHeight;
            itemSize.Checked += SizeOnCheck;
            itemSize.Unchecked += SizeOnCheck;
            itemWindow.Items.Add(itemSize);

            var itemResize = new MenuItem();
            itemResize.Header = "_Resizable";
            itemResize.IsCheckable = true;
            itemResize.IsChecked = ResizeMode == ResizeMode.CanResize;
            itemResize.Click += ResizeOnClick;
            itemWindow.Items.Add(itemResize);

            var itemTopmost = new MenuItem();
            itemTopmost.Header = "_Topmost";
            itemTopmost.IsCheckable = true;
            itemTopmost.IsChecked = Topmost;
            itemSize.Checked += TopmostOnCheck;
            itemSize.Unchecked += TopmostOnCheck;
            itemWindow.Items.Add(itemTopmost);
        }

        private void UninplementedOnClick(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            var strItem = item.Header.ToString().Replace("_", string.Empty);
            MessageBox.Show($"The {strItem} option has not yet been implemented", Title);
        }

        private void ExitOnClicked(object sender, RoutedEventArgs e) => Close();

        private void TaskbarOnClick(object sender, RoutedEventArgs e) => ShowInTaskbar = (sender as MenuItem).IsChecked;

        private void SizeOnCheck(object sender, RoutedEventArgs e) => SizeToContent = (sender as MenuItem).IsChecked ? SizeToContent.WidthAndHeight : SizeToContent.Manual;

        private void ResizeOnClick(object sender, RoutedEventArgs e) => ResizeMode = (sender as MenuItem).IsChecked ? ResizeMode.CanResize : ResizeMode.NoResize;

        private void TopmostOnCheck(object sender, RoutedEventArgs e) => Topmost = (sender as MenuItem).IsChecked;
    }
}
