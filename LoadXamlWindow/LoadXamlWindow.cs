using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace LoadXamlWindow
{
    class LoadXamlWindow
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new Application();

            var uri = new Uri("pack://application:,,,/LoadXamlWindow.xml");
            var stream = Application.GetResourceStream(uri).Stream;
            var win = XamlReader.Load(stream) as Window;

            win.AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));

            app.Run(win);

        }

        private static void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"The button labeled '{(e.Source as Button).Content}' has been clicked");
        }
    }
}
