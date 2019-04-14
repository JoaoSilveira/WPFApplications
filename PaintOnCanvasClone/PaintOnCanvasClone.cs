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
using System.Windows.Shapes;

namespace PaintOnCanvasClone
{
    class PaintOnCanvasClone : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new PaintOnCanvasClone());
        }

        public PaintOnCanvasClone()
        {
            Title = "Paint on Canvas Clone";

            var canv = new CanvasClone();
            Content = canv;

            var brushes = new[] { Brushes.Red, Brushes.Green, Brushes.Blue };

            for (var i = 0; i < brushes.Length; i++)
            {
                var rect = new Rectangle();
                rect.Fill = brushes[i];
                rect.Width = 200;
                rect.Height = 200;
                canv.Children.Add(rect);
                CanvasClone.SetLeft(rect, 100 * (i + 1));
                CanvasClone.SetTop(rect, 100 * (i + 1));
            }
        }
    }
}
