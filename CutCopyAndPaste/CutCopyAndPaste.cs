using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CutCopyAndPaste
{
    public class CutCopyAndPaste : Window
    {
        TextBlock text;
        protected MenuItem itemCut;
        protected MenuItem itemCopy;
        protected MenuItem itemPaste;
        protected MenuItem itemDelete;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CutCopyAndPaste());
        }

        public CutCopyAndPaste()
        {
            Title = "Cut Copy and Paste";

            var dock = new DockPanel();
            Content = dock;

            var menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            text = new TextBlock();
            text.Text = "Sample clipboard text";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.FontSize = 32;
            text.TextWrapping = TextWrapping.Wrap;
            dock.Children.Add(text);

            var itemEdit = new MenuItem();
            itemEdit.Header = "_Edit";
            itemEdit.SubmenuOpened += EditOnOpened;
            menu.Items.Add(itemEdit);

            itemCut = new MenuItem();
            itemCut.Header = "Cu_t";
            itemCut.Click += CutOnClick;
            var img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/Cut.png"));
            itemCut.Icon = img;
            itemEdit.Items.Add(itemCut);

            itemCopy = new MenuItem();
            itemCopy.Header = "_Copy";
            itemCopy.Click += CopyOnClick;
            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/Copy.png"));
            itemCopy.Icon = img;
            itemEdit.Items.Add(itemCopy);

            itemPaste = new MenuItem();
            itemPaste.Header = "_Paste";
            itemPaste.Click += PasteOnClick;
            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/Paste.png"));
            itemPaste.Icon = img;
            itemEdit.Items.Add(itemPaste);

            itemDelete = new MenuItem();
            itemDelete.Header = "_Delete";
            itemDelete.Click += DeleteOnClick;
            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/Delete.png"));
            itemDelete.Icon = img;
            itemEdit.Items.Add(itemDelete);
        }

        private void EditOnOpened(object sender, RoutedEventArgs e)
        {
            itemCut.IsEnabled = itemCopy.IsEnabled = itemDelete.IsEnabled = !string.IsNullOrEmpty(text.Text);
            itemPaste.IsEnabled = Clipboard.ContainsText();
        }

        protected void CutOnClick(object sender, RoutedEventArgs e)
        {
            CopyOnClick(sender, e);
            DeleteOnClick(sender, e);
        }

        protected void CopyOnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(text.Text))
                Clipboard.SetText(text.Text);
        }

        protected void PasteOnClick(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
                text.Text = Clipboard.GetText();
        }

        protected void DeleteOnClick(object sender, RoutedEventArgs e)
        {
            text.Text = null;
        }
    }
}
