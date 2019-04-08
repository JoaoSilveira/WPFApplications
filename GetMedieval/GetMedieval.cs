using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace GetMedieval
{
    class GetMedieval : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new GetMedieval());
        }

        public GetMedieval()
        {
            Title = "Get Medieval";

            var btn = new MedievalButton();
            btn.Text = "Click this button";
            btn.FontSize = 24;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Padding = new Thickness(5, 20, 5, 20);
            btn.Knock += ButtonOnKnock;
            btn.Width = 50;

            Content = btn;
        }

        private void ButtonOnKnock(object sender, RoutedEventArgs e)
        {
            var btn = sender as MedievalButton;

            MessageBox.Show($"The button labeled {btn.Text} has been knocked", Title);
        }
    }
}
