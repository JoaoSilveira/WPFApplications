using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClickTheButton
{
    class ClickTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ClickTheButton());
        }

        public ClickTheButton()
        {
            Title = "Click the Button";

            var button = new Button();
            button.Content = "_Click me, please!";
            button.Click += ButtonOnClick;

            Content = button;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The button has been clicked and all is well.", Title);
        }
    }
}
