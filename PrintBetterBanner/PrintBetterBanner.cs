using ChooseFont;
using PrintBanner;
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

namespace PrintBetterBanner
{
    class PrintBetterBanner : Window
    {
        TextBox txtbox;
        Typeface face;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new PrintBetterBanner());
        }

        public PrintBetterBanner()
        {
            Title = "Print Better Banner";
            SizeToContent = SizeToContent.WidthAndHeight;

            var stack = new StackPanel();
            Content = stack;

            txtbox = new TextBox();
            txtbox.Width = 250;
            txtbox.Margin = new Thickness(12);
            stack.Children.Add(txtbox);

            var btn = new Button();
            btn.Content = "_Font...";
            btn.Margin = new Thickness(12);
            btn.Click += FontOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            btn = new Button();
            btn.Content = "_Print...";
            btn.Margin = new Thickness(12);
            btn.Click += PrintOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            face = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            txtbox.Focus();
        }

        private void FontOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new FontDialog();
            dlg.Owner = this;
            dlg.Typeface = face;

            if (dlg.ShowDialog().GetValueOrDefault())
                face = dlg.Typeface;
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
            paginator.Typeface = face;

            paginator.PageSize = new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight);

            dlg.PrintDocument(paginator, $"Banner: {txtbox.Text}");
        }
    }
}
