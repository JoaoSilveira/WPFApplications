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

namespace ScrollCustomColors
{
    class ScrollCustomColors : Window
    {
        ScrollBar[] scrolls = new ScrollBar[3];
        TextBlock[] txtValue = new TextBlock[3];
        Panel pnlColor;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ScrollCustomColors());
        }

        public ScrollCustomColors()
        {
            Title = "Scroll Custom Colors";
            Width = 500;
            Height = 300;

            var gridMain = new Grid();
            Content = gridMain;

            var coldef = new ColumnDefinition();
            coldef.Width = new GridLength(200, GridUnitType.Pixel);
            gridMain.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            gridMain.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            gridMain.ColumnDefinitions.Add(coldef);

            var split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            gridMain.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);

            pnlColor = new StackPanel();
            pnlColor.Background = new SolidColorBrush(SystemColors.WindowColor);
            gridMain.Children.Add(pnlColor);
            Grid.SetRow(pnlColor, 0);
            Grid.SetColumn(pnlColor, 2);

            var grid = new Grid();
            gridMain.Children.Add(grid);
            Grid.SetRow(grid, 0);
            Grid.SetColumn(grid, 0);

            var rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(100, GridUnitType.Star);
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            for (var i = 0; i < 3; i++)
            {
                coldef = new ColumnDefinition();
                coldef.Width = new GridLength(33, GridUnitType.Star);
                grid.ColumnDefinitions.Add(coldef);
            }

            var colors = new[]
            {
                "Red", "Green", "Blue"
            };

            for (var i = 0; i < 3; i++)
            {
                var lbl = new Label();
                lbl.Content = colors[i];
                lbl.HorizontalAlignment = HorizontalAlignment.Center;
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, 0);
                Grid.SetColumn(lbl, i);

                scrolls[i] = new ScrollBar();
                scrolls[i].Focusable = true;
                scrolls[i].Orientation = Orientation.Vertical;
                scrolls[i].Minimum = 0;
                scrolls[i].Maximum = 255;
                scrolls[i].SmallChange = 1;
                scrolls[i].LargeChange = 16;
                scrolls[i].ValueChanged += ScrollOnValueChanged;
                grid.Children.Add(scrolls[i]);
                Grid.SetRow(scrolls[i], 1);
                Grid.SetColumn(scrolls[i], i);

                txtValue[i] = new TextBlock();
                txtValue[i].TextAlignment = TextAlignment.Center;
                txtValue[i].HorizontalAlignment = HorizontalAlignment.Center;
                txtValue[i].Margin = new Thickness(5);
                grid.Children.Add(txtValue[i]);
                Grid.SetRow(txtValue[i], 2);
                Grid.SetColumn(txtValue[i], i);
            }

            var clr = (pnlColor.Background as SolidColorBrush).Color;
            scrolls[0].Value = clr.R;
            scrolls[1].Value = clr.G;
            scrolls[2].Value = clr.B;

            scrolls[0].Focus();
        }

        private void ScrollOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var scroll = sender as ScrollBar;
            var pnl = scroll.Parent as Panel;
            var txt = pnl.Children[1 + pnl.Children.IndexOf(scroll)] as TextBlock;

            txt.Text = $"{(int)scroll.Value}\n0x{(int)scroll.Value:X2}";

            pnlColor.Background = new SolidColorBrush(
                Color.FromRgb(
                    (byte)scrolls[0].Value,
                    (byte)scrolls[1].Value,
                    (byte)scrolls[2].Value
                )
            );
        }
    }
}
