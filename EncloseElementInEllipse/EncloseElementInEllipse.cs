using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EncloseElementInEllipse
{
    class EncloseElementInEllipse : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new EncloseElementInEllipse());
        }

        public EncloseElementInEllipse()
        {
            Title = "Enclose Element in Ellipse";

            var elips = new EllipseWithChild();
            elips.Fill = Brushes.ForestGreen;
            elips.Stroke = new Pen(Brushes.Magenta, 48);

            Content = elips;

            var text = new TextBlock();
            text.FontFamily = new FontFamily("Times New Roman");
            text.FontSize = 48;
            text.Text = "Text inside ellipse";

            elips.Child = text;
        }
    }
}
