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

namespace RecurseDirectoriesIncrementally
{
    public class RecurseDirectoriesIncrementally : Window
    {
        StackPanel stack;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new RecurseDirectoriesIncrementally());
        }

        public RecurseDirectoriesIncrementally()
        {
            Title = "Recurse Directories Incrementally";

            var grid = new Grid();
            Content = grid;

            var coldef = new ColumnDefinition();
            coldef.Width = new GridLength(50, GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(50, GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);

            var tree = new DirectoryTreeView();
            tree.SelectedItemChanged += TreeViewOnSelectedItemChanged;
            grid.Children.Add(tree);
            Grid.SetColumn(tree, 0);

            var split = new GridSplitter();
            split.Width = 6;
            split.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
            grid.Children.Add(split);
            Grid.SetColumn(split, 1);

            var scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetColumn(scroll, 2);

            stack = new StackPanel();
            scroll.Content = stack;
        }

        private void TreeViewOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = e.NewValue as DirectoryTreeViewItem;

            stack.Children.Clear();

            FileInfo[] files;

            try
            {
                files = item.DirectoryInfo.GetFiles();
            }
            catch { return; }

            foreach (var info in files)
            {
                var text = new TextBlock();
                text.Text = info.Name;
                stack.Children.Add(text);
            }
        }
    }
}
