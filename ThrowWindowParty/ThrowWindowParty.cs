using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ThrowWindowParty
{
    class ThrowWindowParty : Application
    {
        [STAThread]
        public static void Main()
        {
            var app = new ThrowWindowParty();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var winMain = new Window();
            winMain.Title = "Main Window";
            winMain.MouseDown += WindowOnMouseDown;
            winMain.Show();

            for (var i = 0; i < 2; i++)
            {
                var win = new Window();
                win.Title = "Extra Window No. " + (i + 1);
                win.Owner = winMain;
                //win.ShowInTaskbar = false;
                win.Show();
            }
        }

        void WindowOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var win = new Window();
            win.Title = "Modal Dialog Box";
            win.ShowDialog();
        }
    }
}
