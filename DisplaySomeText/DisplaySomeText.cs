using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DisplaySomeText
{
    class DisplaySomeText : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DisplaySomeText());
        }

        public DisplaySomeText()
        {
            Title = "Display Some Text";
            Content = "Content can be simple text!";
        }
    }
}
