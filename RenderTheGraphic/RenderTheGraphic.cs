using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RenderTheGraphic
{
    class RenderTheGraphic : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new RenderTheGraphic());
        }

        public RenderTheGraphic()
        {
            Title = "Render the Graphic";

            Content = new SimpleEllipse();
        }
    }
}
