using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InheritTheWindow
{
    class InheritTheWindow : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new InheritTheWindow());
        }

        public InheritTheWindow()
        {
            Title = "Inherit The Window";

            Width = 100 * Math.PI;
            Height = 100 * Math.E;

            Left = (SystemParameters.WorkArea.Width - Width) / 2 + SystemParameters.WorkArea.Left;
            Top = (SystemParameters.WorkArea.Height - Height) / 2 + SystemParameters.WorkArea.Top;
        }
    }
}
