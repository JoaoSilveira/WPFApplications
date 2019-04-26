using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PrintaBunchaButtons
{
    class PrintaBunchaButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new PrintaBunchaButtons());
        }

        public PrintaBunchaButtons()
        {
            Title = "Print a Bunch of Buttons";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;

            var btn = new Button();
            btn.FontSize = 24;
            btn.Content = "Print...";
            btn.Padding = new Thickness(12);
            btn.Margin = new Thickness(96);
            btn.Click += PrintOnClick;
            Content = btn;
        }

        private void PrintOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new PrintDialog();

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            var grid = new Grid();

            for (var i = 0; i < 5; i++)
            {
                var coldef = new ColumnDefinition();
                coldef.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(coldef);

                var rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            grid.Background = new LinearGradientBrush(Colors.Gray, Colors.White, new Point(0, 0), new Point(1, 1));

            var rand = new Random();

            for (var i = 0; i < 25; i++)
            {
                var btn = new Button();
                btn.FontSize = 12 + rand.Next(8);
                btn.Content = $"Button No. {i + 1}";
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Margin = new Thickness(6);
                grid.Children.Add(btn);
                Grid.SetRow(btn, i % 5);
                Grid.SetColumn(btn, i / 5);
            }

            grid.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            var sizeGrid = grid.DesiredSize;

            var ptGrid = new Point(
                (dlg.PrintableAreaWidth - sizeGrid.Width) / 2,
                (dlg.PrintableAreaHeight - sizeGrid.Height) / 2);

            grid.Arrange(new Rect(ptGrid, sizeGrid));

            // added to fix grid not printing
            grid.InvalidateArrange();
            grid.UpdateLayout();

            dlg.PrintVisual(grid, Title);
        }
    }
}
