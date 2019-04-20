using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RecurseDirectoriesIncrementally
{
    public class DirectoryTreeViewItem : ImagedTreeViewItem
    {
        public DirectoryInfo DirectoryInfo { get; }

        public DirectoryTreeViewItem(DirectoryInfo dir)
        {
            DirectoryInfo = dir;
            Text = dir.Name;

            SelectedImage = new BitmapImage(new Uri("pack://application:,,,/Images/OpenFolder.png"));
            UnselectedImage = new BitmapImage(new Uri("pack://application:,,,/Images/ClosedFolder.png"));
        }

        public void Populate()
        {
            DirectoryInfo[] dirs;

            try
            {
                dirs = DirectoryInfo.GetDirectories();
            }
            catch { return; }

            foreach (var child in dirs)
                Items.Add(new DirectoryTreeViewItem(child));
        }

        protected override void OnExpanded(RoutedEventArgs e)
        {
            base.OnExpanded(e);

            foreach (var obj in Items)
                (obj as DirectoryTreeViewItem)?.Populate();
        }
    }
}
