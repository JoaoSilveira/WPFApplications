using System;
using System.Windows;
using System.Windows.Controls;

namespace ScrollFiftyButtons
{
    class ScrollFiftyButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ScrollFiftyButtons());
        }

        public ScrollFiftyButtons()
        {
            Title = "Scroll Fifty Buttons";
            SizeToContent = SizeToContent.Width;
            AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));

            var scroll = new ScrollViewer();
            Content = scroll;

            var stack = new StackPanel();
            stack.Margin = new Thickness(5);
            scroll.Content = stack;

            for (var i = 0; i < 50; i++)
            {
                var btn = new Button();
                btn.Name = $"Button{i + 1}";
                btn.Content = $"{btn.Name} says 'Click me'";
                btn.Margin = new Thickness(5);

                stack.Children.Add(btn);
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as Button;

            if (btn != null)
                MessageBox.Show($"{btn.Name} has been clicked", "Button Click");
        }
    }
}
