using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace YellowPad
{
    public partial class YellowPadHelp : NavigationWindow
    {
        public YellowPadHelp()
        {
            InitializeComponent();

            (tree.Items[0] as TreeViewItem).IsSelected = true;
            tree.Focus();
        }

        void HelpOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = e.NewValue as TreeViewItem;

            if (item.Tag is string uri)
                frame.Navigate(new Uri(uri, UriKind.Relative));
        }
    }
}
