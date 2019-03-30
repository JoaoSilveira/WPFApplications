using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NavigateTheWeb
{
    class NavigateTheWeb : Window
    {
        Frame frm;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new NavigateTheWeb());
        }

        public NavigateTheWeb()
        {
            Title = "Navigate the Web";

            frm = new Frame();
            Content = frm;

            Loaded += WindowOnLoaded;
        }

        private void WindowOnLoaded(object sender, RoutedEventArgs e)
        {
            var dialog = new UriDialog();
            dialog.Owner = this;
            dialog.Text = "https://";
            dialog.ShowDialog();

            try
            {
                frm.Source = new Uri(dialog.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title);
            }
        }
    }
}
