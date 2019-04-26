using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PrintBanner
{
    public class PrintBanner : Window
    {
        TextBox txtbox;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new PrintBanner());
        }

        public PrintBanner()
        {
            Title = "Print Banner";
            SizeToContent = SizeToContent.WidthAndHeight;

            var stack = new StackPanel();
            Content = stack;

            txtbox = new TextBox();
            txtbox.Width = 250;
            txtbox.Margin = new Thickness(12);
            stack.Children.Add(txtbox);

            var btn = new Button();
            btn.Content = "_Print...";
            btn.Margin = new Thickness(12);
            btn.Click += PrintOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            txtbox.Focus();
        }

        private void PrintOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new PrintDialog();

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            var prntkt = dlg.PrintTicket;
            prntkt.PageOrientation = PageOrientation.Portrait;
            dlg.PrintTicket = prntkt;

            var paginator = new BannerDocumentPaginator();
            paginator.Text = txtbox.Text;
            paginator.PageSize = new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight);

            dlg.PrintDocument(paginator, $"Banner: {txtbox.Text}");
        }
    }
}
