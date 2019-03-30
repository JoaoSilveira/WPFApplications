using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StackThirtyButtons
{
    class StackThirtyButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new StackThirtyButtons());
        }

        public StackThirtyButtons()
        {
            Title = "Stack Thirty Buttons";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));

            var stackMain = new StackPanel();
            stackMain.Orientation = Orientation.Horizontal;
            stackMain.Margin = new Thickness(5);
            Content = stackMain;

            for (var i = 0; i < 3; i++)
            {
                var stackChild = new StackPanel();
                stackMain.Children.Add(stackChild);

                for (int j = 0; j < 10; j++)
                {
                    var btn = new Button();
                    btn.Content = $"Button No. {i * 10 + j + 1}";
                    btn.Margin = new Thickness(5);
                    stackChild.Children.Add(btn);
                }
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"You clicked the button labeled {(e.Source as Button).Content}");
        }
    }
}
