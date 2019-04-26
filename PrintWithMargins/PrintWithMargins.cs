using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PrintWithMargins
{
    class PrintWithMargins : Window
    {
        PrintQueue prnqueue;
        PrintTicket prntkt;
        Thickness marginPage = new Thickness(96);

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new PrintWithMargins());
        }

        public PrintWithMargins()
        {
            Title = "Print with Margins";
            FontSize = 24;

            var stack = new StackPanel();
            Content = stack;

            var btn = new Button();
            btn.Content = "Page Set_up";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += SetupOnClick;
            stack.Children.Add(btn);

            btn = new Button();
            btn.Content = "_Print...";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += PrintOnClick;
            stack.Children.Add(btn);
        }

        private void SetupOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new PageMarginsDialog();
            dlg.Owner = this;
            dlg.PageMargins = marginPage;

            if (dlg.ShowDialog().GetValueOrDefault())
                marginPage = dlg.PageMargins;
        }

        private void PrintOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new PrintDialog();

            if (prnqueue != null)
                dlg.PrintQueue = prnqueue;

            if (prntkt != null)
                dlg.PrintTicket = prntkt;

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                prnqueue = dlg.PrintQueue;
                prntkt = dlg.PrintTicket;

                var vis = new DrawingVisual();

                using (var dc = vis.RenderOpen())
                {
                    var pn = new Pen(Brushes.Black, 1);

                    var rectPage = new Rect(
                        marginPage.Left,
                        marginPage.Top,
                        dlg.PrintableAreaWidth - (marginPage.Left + marginPage.Right),
                        dlg.PrintableAreaHeight - (marginPage.Top + marginPage.Bottom));

                    dc.DrawRectangle(null, pn, rectPage);

                    var formtxt = new FormattedText(
                        $"Hello, Printer! {dlg.PrintableAreaWidth / 96:F3} x {dlg.PrintableAreaHeight / 96:F3}",
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface(
                            new FontFamily("Times New Roman"),
                            FontStyles.Italic,
                            FontWeights.Normal,
                            FontStretches.Normal),
                        48,
                        Brushes.Black);

                    var sizeText = new Size(formtxt.Width, formtxt.Height);
                    var ptText = new Point(
                        rectPage.Left + (rectPage.Width - formtxt.Width) / 2,
                        rectPage.Top + (rectPage.Height - formtxt.Height) / 2);

                    dc.DrawText(formtxt, ptText);
                    dc.DrawRectangle(null, pn, new Rect(ptText, sizeText));
                }

                dlg.PrintVisual(vis, Title);
            }
        }
    }
}
