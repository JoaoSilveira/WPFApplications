using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GrowAndShrink
{
    class GrowAndShrink : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new GrowAndShrink());
        }

        public GrowAndShrink()
        {
            Title = "Grow & Shrink";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Width = 192;
            Height = 192;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Up)
            {
                Left -= 0.05 * Width;
                Top -= 0.05 * Height;
                Width *= 1.1;
                Height *= 1.1;
            }
            else if (e.Key == Key.Down)
            {
                Left += 0.05 * Width;
                Top += 0.05 * Height;
                Width /= 1.1;
                Height /= 1.1;
            }
        }
    }
}
