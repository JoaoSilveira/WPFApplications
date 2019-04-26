using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PrintEllipse
{
    class PrintEllipse : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new PrintEllipse());
        }

        public PrintEllipse()
        {
            Title = "Print Ellipse";

            var stack = new StackPanel();
            Content = stack;

            var btn = new Button();
            btn.Content = "_Print...";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += PrintOnClick;
            stack.Children.Add(btn);
        }

        private void PrintOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new PrintDialog();

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            var vis = new DrawingVisual();

            using (var dc = vis.RenderOpen())
            {
                dc.DrawEllipse(
                    Brushes.LightGray,
                    new Pen(Brushes.Black, 3),
                    new Point(dlg.PrintableAreaWidth / 2, dlg.PrintableAreaHeight / 2),
                    dlg.PrintableAreaWidth / 2,
                    dlg.PrintableAreaHeight / 2);
            }

            dlg.PrintVisual(vis, "My first print fob");
        }
    }
}
