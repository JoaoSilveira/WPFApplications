using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NotepadClone
{
    public class WordWrapMenuItem : MenuItem
    {
        public static readonly DependencyProperty WordWrapProperty = DependencyProperty.Register("WordWrap", typeof(TextWrapping), typeof(WordWrapMenuItem));
        
        public TextWrapping WordWrap
        {
            get { return (TextWrapping)GetValue(WordWrapProperty); }
            set { SetValue(WordWrapProperty, value); }
        }

        public WordWrapMenuItem()
        {
            Header = "_Word Wrap";

            var item = new MenuItem();
            item.Header = "_No Wrap";
            item.Tag = TextWrapping.NoWrap;
            item.Click += MenuItemOnClick;
            Items.Add(item);

            item = new MenuItem();
            item.Header = "_Wrap";
            item.Tag = TextWrapping.Wrap;
            item.Click += MenuItemOnClick;
            Items.Add(item);

            item = new MenuItem();
            item.Header = "Wrap With _Overflow";
            item.Tag = TextWrapping.WrapWithOverflow;
            item.Click += MenuItemOnClick;
            Items.Add(item);
        }

        protected override void OnSubmenuOpened(RoutedEventArgs e)
        {
            base.OnSubmenuOpened(e);

            foreach (MenuItem item in Items)
                item.IsChecked = (TextWrapping)item.Tag == WordWrap;
        }

        private void MenuItemOnClick(object sender, RoutedEventArgs e) => WordWrap = (TextWrapping)(e.Source as MenuItem).Tag;
    }
}
