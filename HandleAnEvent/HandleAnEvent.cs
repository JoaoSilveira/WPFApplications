using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HandleAnEvent
{
    class HandleAnEvent
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();

            var win = new Window();
            win.Title = "Handle an event";
            win.MouseDown += WindowOnMouseDown;

            app.Run(win);
        }

        static void WindowOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var win = sender as Window;

            MessageBox.Show($"Window clicked with {e.ChangedButton} button at point ({e.GetPosition(win)})", win.Title);
        }
    }
}
