using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SetFontSizeProperty
{
    class SetFontSizeProperty : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SetFontSizeProperty());
        }

        public SetFontSizeProperty()
        {
            Title = "Set FontSize Property";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            FontSize = 16;
            double[] fntSizes = { 8, 16, 32 };

            var grid = new Grid();
            Content = grid;

            for (int i = 0; i < 2; i++)
            {
                var row = new RowDefinition();
                row.Height = GridLength.Auto;
                grid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < fntSizes.Length; i++)
            {
                var col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < fntSizes.Length; i++)
            {
                var btn = new Button();
                btn.Content = new TextBlock(new Run("Set window FontSize to " + fntSizes[i]));
                btn.Tag = fntSizes[i];
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += WindowFontSizeOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 0);
                Grid.SetColumn(btn, i);

                btn = new Button();
                btn.Content = new TextBlock(new Run("Set button FontSize to " + fntSizes[i]));
                btn.Tag = fntSizes[i];
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += ButtonFontSizeOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 1);
                Grid.SetColumn(btn, i);
            }
        }

        private void WindowFontSizeOnClick(object sender, RoutedEventArgs e)
        {
            FontSize = (double)(sender as Button).Tag;
        }

        private void ButtonFontSizeOnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.FontSize = (double)btn.Tag;
        }
    }
}
