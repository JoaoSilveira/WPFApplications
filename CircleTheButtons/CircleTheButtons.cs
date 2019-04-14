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

namespace CircleTheButtons
{
    class CircleTheButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CircleTheButtons());
        }

        public CircleTheButtons()
        {
            Title = "Circle the Buttons";

            var pnl = new RadialPanel();
            pnl.Orientation = RadialPanelOrientation.ByHeight;
            pnl.ShowPieLines = true;
            Content = pnl;

            var rand = new Random();

            for (var i = 0; i < 10; i++)
            {
                var btn = new Button();
                btn.Content = $"Button Number {i + 1}";
                btn.FontSize += rand.Next(10);
                pnl.Children.Add(btn);
            }
        }

    }
}
