using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayHello
{
    class SayHello
    {
        [STAThread]
        public static void Main()
        {
            var win = new Window();
            win.Title = "SayHello";
            win.Show();

            var app = new Application();
            app.Run();
        }
    }
}
