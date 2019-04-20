using ShowClassHierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ExploreDependencyProperties
{
    public class ExploreDependencyProperties : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ExploreDependencyProperties());
        }

        public ExploreDependencyProperties()
        {
            Title = "Explore Dependency Properties";

            var grid = new Grid();
            Content = grid;

            var col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);

            col = new ColumnDefinition();
            col.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(col);

            col = new ColumnDefinition();
            col.Width = new GridLength(3, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);

            var treevue = new ClassHierarchyTreeView(typeof(DependencyObject));
            grid.Children.Add(treevue);
            Grid.SetColumn(treevue, 0);

            var split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            grid.Children.Add(split);
            Grid.SetColumn(split, 1);

            var lstvue = new DependencyPropertyListView();
            grid.Children.Add(lstvue);
            Grid.SetColumn(lstvue, 2);

            lstvue.SetBinding(DependencyPropertyListView.TypeProperty, "SelectedItem.Type");
            lstvue.DataContext = treevue;
        }
    }
}
