using System;
using System.Windows;
using System.Windows.Controls;

namespace SetSpaceProperty
{
    class SetSpaceProperty : SpaceWindow
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SetSpaceProperty());
        }

        public SetSpaceProperty()
        {
            Title = "Set Space Property";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            var spaces = new[] { 0, 1, 2 };

            var grid = new Grid();
            Content = grid;

            for (int i = 0; i < 2; i++)
            {
                var row = new RowDefinition();
                row.Height = GridLength.Auto;
                grid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < spaces.Length; i++)
            {
                var col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < spaces.Length; i++)
            {
                var btn = new SpaceButton();
                btn.Text = $"Set Window Space to {spaces[i]}";
                btn.Tag = spaces[i];
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += WindowPropertyOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 0);
                Grid.SetColumn(btn, i);

                btn = new SpaceButton();
                btn.Text = $"Set button Space to {spaces[i]}";
                btn.Tag = spaces[i];
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += ButtonPropertyOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 1);
                Grid.SetColumn(btn, i);
            }
        }

        private void ButtonPropertyOnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as SpaceButton;
            btn.Space = (int)btn.Tag;
        }

        private void WindowPropertyOnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as SpaceButton;
            Space = (int)btn.Tag;
        }
    }
}
