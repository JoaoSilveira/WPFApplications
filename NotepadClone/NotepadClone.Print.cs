using PrintWithMargins;
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

namespace NotepadClone
{
    public partial class NotepadClone : Window
    {
        PrintQueue prnqueue;
        PrintTicket prntkt;
        Thickness marginPage = new Thickness(96);

        void AddPrintMenuItems(MenuItem itemFile)
        {
            var itemSetup = new MenuItem();
            itemSetup.Header = "Page Set_up...";
            itemSetup.Click += PageSetupOnClick;
            itemFile.Items.Add(itemSetup);

            var itemPrint = new MenuItem();
            itemPrint.Header = "_Print...";
            itemPrint.Command = ApplicationCommands.Print;
            itemFile.Items.Add(itemPrint);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, PrintOnExecute));
        }

        private void PageSetupOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new PageMarginsDialog();
            dlg.Owner = this;
            dlg.PageMargins = marginPage;

            if (dlg.ShowDialog().GetValueOrDefault())
                marginPage = dlg.PageMargins;
        }

        private void PrintOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new PrintDialog();

            if (prnqueue != null)
                dlg.PrintQueue = prnqueue;

            if (prntkt != null)
                dlg.PrintTicket = prntkt;

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            prnqueue = dlg.PrintQueue;
            prntkt = dlg.PrintTicket;

            var paginator = new PlainTextDocumentPaginator();

            paginator.PrintTicket = prntkt;
            paginator.Text = txtbox.Text;
            paginator.Header = strLoadedFile;
            paginator.Typeface = new Typeface(txtbox.FontFamily, txtbox.FontStyle, txtbox.FontWeight, txtbox.FontStretch);
            paginator.FaceSize = txtbox.FontSize;
            paginator.TextWrapping = txtbox.TextWrapping;
            paginator.Margins = marginPage;
            paginator.PageSize = new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight);

            dlg.PrintDocument(paginator, Title);
        }
    }
}
