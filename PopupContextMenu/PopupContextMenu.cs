using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace PopupContextMenu
{
    class PopupContextMenu : Window
    {
        ContextMenu menu;
        MenuItem itemBold;
        MenuItem itemItalic;
        MenuItem[] itemDecor;
        Inline inlClicked;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new PopupContextMenu());
        }

        public PopupContextMenu()
        {
            Title = "Popup Context Menu";

            menu = new ContextMenu();

            itemBold = new MenuItem();
            itemBold.Header = "Bold";
            menu.Items.Add(itemBold);

            itemItalic = new MenuItem();
            itemItalic.Header = "Italic";
            menu.Items.Add(itemItalic);

            var locs = (TextDecorationLocation[])Enum.GetValues(typeof(TextDecorationLocation));

            itemDecor = new MenuItem[locs.Length];
            for (int i = 0; i < itemDecor.Length; i++)
            {
                var decor = new TextDecoration();
                decor.Location = locs[i];

                itemDecor[i] = new MenuItem();
                itemDecor[i].Header = locs[i].ToString();
                itemDecor[i].Tag = decor;
                menu.Items.Add(itemDecor[i]);
            }

            menu.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(MenuOnClick));

            var text = new TextBlock();
            text.FontSize = 32;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            Content = text;

            var quote = "To be, or not to be, that is the question";

            foreach (var word in quote.Split())
            {
                var run = new Run(word);

                run.TextDecorations = new TextDecorationCollection();
                text.Inlines.Add(run);
                text.Inlines.Add(" ");
            }
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonUp(e);

            if ((inlClicked = e.Source as Inline) != null)
            {
                itemBold.IsChecked = inlClicked.FontWeight == FontWeights.Bold;
                itemItalic.IsChecked = inlClicked.FontStyle == FontStyles.Italic;

                foreach (var item in itemDecor)
                    item.IsChecked = inlClicked.TextDecorations.Contains(item.Tag as TextDecoration);

                menu.IsOpen = true;
                e.Handled = true;
            }
        }

        private void MenuOnClick(object sender, RoutedEventArgs e)
        {
            var item = e.Source as MenuItem;

            item.IsChecked = !item.IsChecked;

            if (item == itemBold)
                inlClicked.FontWeight = item.IsChecked ? FontWeights.Bold : FontWeights.Regular;
            else if (item == itemItalic)
                inlClicked.FontStyle = item.IsChecked ? FontStyles.Italic : FontStyles.Normal;
            else
            {
                if (item.IsChecked)
                    inlClicked.TextDecorations.Add(item.Tag as TextDecoration);
                else
                    inlClicked.TextDecorations.Remove(item.Tag as TextDecoration);
            }
            (inlClicked.Parent as TextBlock).InvalidateVisual();
        }
    }
}
