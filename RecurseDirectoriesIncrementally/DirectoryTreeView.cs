using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RecurseDirectoriesIncrementally
{
    public class DirectoryTreeView : TreeView
    {
        public DirectoryTreeView()
        {
            RefreshTree();
        }

        public void RefreshTree()
        {
            BeginInit();
            Items.Clear();

            foreach (var drive in DriveInfo.GetDrives())
            {
                var chDrive = drive.Name.ToUpper()[0];

                var item = new DirectoryTreeViewItem(drive.RootDirectory);

                if (chDrive != 'A' && chDrive != 'B' && drive.IsReady && drive.VolumeLabel.Length > 0)
                    item.Text = $"{drive.VolumeLabel} ({drive.Name})";
                else
                    item.Text = $"{drive.DriveType} ({drive.Name})";

                if (chDrive == 'A' || chDrive == 'B')
                    item.SelectedImage = item.UnselectedImage = new BitmapImage(new Uri("pack://application:,,,/Images/FloppyDrive.png"));
                else if (drive.DriveType == DriveType.CDRom)
                    item.SelectedImage = item.UnselectedImage = new BitmapImage(new Uri("pack://application:,,,/Images/CDDrive.png"));
                else
                    item.SelectedImage = item.UnselectedImage = new BitmapImage(new Uri("pack://application:,,,/Images/HardDrive.png"));

                Items.Add(item);

                if (chDrive != 'A' && chDrive != 'B' && drive.IsReady)
                    item.Populate();
            }
            EndInit();
        }
    }
}
