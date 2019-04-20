using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace RecurseDirectoriesInefficiently
{
    class RecurseDirectoriesInefficiently : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new RecurseDirectoriesInefficiently());
        }

        public RecurseDirectoriesInefficiently()
        {
            Title = "Recurse Directories Inefficiently";

            var tree = new TreeView();
            Content = tree;

            var item = new TreeViewItem();
            item.Header = Path.GetPathRoot(Environment.SystemDirectory);
            item.Tag = new DirectoryInfo(item.Header as string);
            tree.Items.Add(item);
            GetSubdirectories(item);
        }

        void GetSubdirectories(TreeViewItem item)
        {
            var dir = item.Tag as DirectoryInfo;
            DirectoryInfo[] subdirs;

            try
            {
                subdirs = dir.GetDirectories();
            }
            catch
            {
                return;
            }

            foreach (var subdir in subdirs)
            {
                var subitem = new TreeViewItem();
                subitem.Header = subdir.Name;
                subitem.Tag = subdir;
                item.Items.Add(subitem);

                GetSubdirectories(subitem);
            }
        }
    }
}
