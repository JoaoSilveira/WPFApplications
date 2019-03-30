using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StackTenButtons
{
    class StackTenButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new StackTenButtons());
        }

        public StackTenButtons()
        {
            Title = "Stack Ten Buttons";

            var stack = new StackPanel();
            Content = stack;
            var rand = new Random();

            for (var i = 0; i < 10; i++)
            {
                var btn = new Button();
                btn.Name = ((char)('A' + i)).ToString();
                btn.FontSize += rand.Next(10);
                btn.Content = $"Button {btn.Name} says 'Click me'";
                btn.Click += ButtonOnClick;

                stack.Children.Add(btn);
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as Button;

            MessageBox.Show($"Button {btn.Name} has been clicked", "Button Click");
        }
    }
}
