using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ToggleTheButton
{
    class ToggleTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ToggleTheButton());
        }

        public ToggleTheButton()
        {
            Title = "Toggle the Button";

            var button = new ToggleButton();
            button.Content = "Can _Resize";
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.IsChecked = ResizeMode == ResizeMode.CanResize;
            button.Checked += ButtonOnChecked;
            button.Unchecked += ButtonOnChecked;

            Content = button;
        }

        private void ButtonOnChecked(object sender, RoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            ResizeMode = button.IsChecked.Value ? ResizeMode.CanResize : ResizeMode.NoResize;
        }
    }
}
