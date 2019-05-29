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

namespace RenderTheAnimation
{
    public class RenderTheAnimation : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new RenderTheAnimation());
        }

        public RenderTheAnimation()
        {
            Title = "Render the Animation";

            Content = new AnimatedCircle();
        }
    }
}
