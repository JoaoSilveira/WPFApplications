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

namespace SplitNine
{
    class SplitNine : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SplitNine());
        }

        public SplitNine()
        {
            Title = "Split Nine";

            var grid = new Grid();
            Content = grid;

            for (var i = 0; i < 3; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    var btn = new Button();
                    btn.Content = $"Row {y} and Column {x}";
                    btn.Margin = new Thickness(10);
                    grid.Children.Add(btn);
                    Grid.SetRow(btn, y);
                    Grid.SetColumn(btn, x);
                }
            }

            var split = new GridSplitter();
            split.Width = 6;
            grid.Children.Add(split);
            Grid.SetRow(split, 1);
            Grid.SetColumn(split, 1);
        }

    }
}
