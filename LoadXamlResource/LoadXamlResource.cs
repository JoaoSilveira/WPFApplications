using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace LoadXamlResource
{
    class LoadXamlResource : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new LoadXamlResource());
        }

        public LoadXamlResource()
        {
            Title = "Load Xaml Resource";

            var uri = new Uri("pack://application:,,,/LoadXamlResource.xml");
            var stream = Application.GetResourceStream(uri).Stream;
            var el = XamlReader.Load(stream) as FrameworkElement;
            Content = el;

            if (el.FindName("MyButton") is Button btn)
                btn.Click += ButtonOnClick;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e) => MessageBox.Show($"The button Labeled '{(e.Source as Button).Content}' has been clicked");
    }
}
