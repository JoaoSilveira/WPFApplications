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

namespace SplitTheClient
{
    class SplitTheClient : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SplitTheClient());
        }

        public SplitTheClient()
        {
            Title = "Split the Client";

            var grid1 = new Grid();
            grid1.ColumnDefinitions.Add(new ColumnDefinition());
            grid1.ColumnDefinitions.Add(new ColumnDefinition());
            grid1.ColumnDefinitions.Add(new ColumnDefinition());
            grid1.ColumnDefinitions[1].Width = GridLength.Auto;
            Content = grid1;

            var btn = new Button();
            btn.Content = "Button No. 1";
            grid1.Children.Add(btn);
            Grid.SetRow(btn, 0);
            Grid.SetColumn(btn, 0);

            var split = new GridSplitter();
            split.ShowsPreview = true;
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            grid1.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);

            var grid2 = new Grid();
            grid2.RowDefinitions.Add(new RowDefinition());
            grid2.RowDefinitions.Add(new RowDefinition());
            grid2.RowDefinitions.Add(new RowDefinition());
            grid2.RowDefinitions[1].Height = GridLength.Auto;
            grid1.Children.Add(grid2);
            Grid.SetRow(grid2, 0);
            Grid.SetColumn(grid2, 2);

            btn = new Button();
            btn.Content = "Button No. 2";
            grid2.Children.Add(btn);
            Grid.SetRow(btn, 0);
            Grid.SetColumn(btn, 0);

            split = new GridSplitter();
            split.ShowsPreview = true;
            split.HorizontalAlignment = HorizontalAlignment.Stretch;
            split.VerticalAlignment = VerticalAlignment.Center;
            split.Height = 6;
            grid2.Children.Add(split);
            Grid.SetRow(split, 1);
            Grid.SetColumn(split, 0);

            btn = new Button();
            btn.Content = "Button No. 3";
            grid2.Children.Add(btn);
            Grid.SetRow(btn, 2);
            Grid.SetColumn(btn, 0);
        }

    }
}
