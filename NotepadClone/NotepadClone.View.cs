using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NotepadClone
{
    public partial class NotepadClone
    {
        MenuItem itemStatus;

        void AddViewMenu()
        {
            var itemView = new MenuItem();
            itemView.Header = "_View";
            itemView.SubmenuOpened += ViewOnOpen;
            menu.Items.Add(itemView);

            itemStatus = new MenuItem();
            itemStatus.Header = "_Status Bar";
            itemStatus.IsCheckable = true;
            itemStatus.Checked += StatusOnCheck;
            itemStatus.Unchecked += StatusOnCheck;
            itemView.Items.Add(itemStatus);
        }

        private void ViewOnOpen(object sender, RoutedEventArgs e) => itemStatus.IsChecked = status.Visibility == Visibility.Visible;

        private void StatusOnCheck(object sender, RoutedEventArgs e) => status.Visibility = (sender as MenuItem).IsChecked ? Visibility.Visible : Visibility.Collapsed;
    }
}
