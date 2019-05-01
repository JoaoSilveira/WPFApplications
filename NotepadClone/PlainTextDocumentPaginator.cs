using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace NotepadClone
{
    public class PlainTextDocumentPaginator : DocumentPaginator
    {
        char[] charsBreak = { ' ', '-' };
        Size sizePage = new Size(8.5 * 96, 11 * 96);
        Size sizeMax = new Size();
        List<DocumentPage> listPages;

        public string Text { get; set; } = string.Empty;

        public TextWrapping TextWrapping { get; set; } = TextWrapping.Wrap;

        public Thickness Margins { get; set; } = new Thickness(96);

        public Typeface Typeface { get; set; } = new Typeface(string.Empty);

        public double FaceSize { get; set; } = 11d;

        public PrintTicket PrintTicket { get; set; } = new PrintTicket();

        public string Header { get; set; } = null;

        public override bool IsPageCountValid
        {
            get
            {
                if (listPages == null)
                    Format();
                return true;
            }
        }

        public override int PageCount => listPages?.Count ?? 0;

        public override Size PageSize
        {
            get { return sizePage; }
            set { sizePage = value; }
        }

        public override DocumentPage GetPage(int pageNumber) => listPages[pageNumber];

        public override IDocumentPaginatorSource Source => null;

        class PrintLine
        {
            public string String;
            public bool Flag;

            public PrintLine(string str, bool flag)
            {
                String = str;
                Flag = flag;
            }
        }

        void Format()
        {
            var listLines = new List<PrintLine>();
            var formtxtSample = GetFormattedText("W");

            var width = PageSize.Width - Margins.Left - Margins.Right;

            if (width < formtxtSample.Width)
                return;

            var pn = new Pen(Brushes.Black, 2);
            using (var reader = new StringReader(Text))
            {
                string strLine;
                while ((strLine = reader.ReadLine()) != null)
                    ProcessLine(strLine, width, listLines);
            }

            var heightLine = formtxtSample.LineHeight + formtxtSample.Height;
            var height = PageSize.Height - Margins.Top - Margins.Bottom;
            var linesPerPage = (int)(height / heightLine);

            if (linesPerPage < 1)
                return;

            var numPages = (listLines.Count + linesPerPage - 1) / linesPerPage;
            var xStart = Margins.Left;
            var yStart = Margins.Top;

            listPages = new List<DocumentPage>();
            for (int iPage = 0, iLine = 0; iPage < numPages; iPage++)
            {
                var vis = new DrawingVisual();
                using (var dc = vis.RenderOpen())
                {
                    if (!string.IsNullOrEmpty(Header))
                    {
                        var formtxt = GetFormattedText(Header);
                        formtxt.SetFontWeight(FontWeights.Bold);
                        var ptText = new Point(xStart, yStart - 2 * formtxt.Height);
                        
                        dc.DrawText(formtxt, ptText);
                    }
                    
                    if (numPages > 1)
                    {
                        var formtxt = GetFormattedText($"Page {iPage + 1} of {numPages}");
                        formtxt.SetFontWeight(FontWeights.Bold);
                        var ptText = new Point(
                            (PageSize.Width + Margins.Left - Margins.Right - formtxt.Width) / 2,
                            PageSize.Height - Margins.Bottom + formtxt.Height);

                        dc.DrawText(formtxt, ptText);
                    }

                    for (var i = 0; i < linesPerPage && iLine < listLines.Count; i++, iLine++)
                    {
                        var formtxt = GetFormattedText(listLines[iLine].String);
                        var ptText = new Point(xStart, yStart + i * heightLine);

                        dc.DrawText(formtxt, ptText);

                        if (!listLines[iLine].Flag)
                            continue;

                        var x = xStart + width + 6;
                        var y = yStart + i * heightLine + formtxt.Baseline;
                        var len = Typeface.CapsHeight * FaceSize;

                        dc.DrawLine(pn, new Point(x, y), new Point(x + len, y - len));
                        dc.DrawLine(pn, new Point(x, y), new Point(x, y - len / 2));
                        dc.DrawLine(pn, new Point(x, y), new Point(x + len / 2, y));
                    }
                }
                listPages.Add(new DocumentPage(vis));
            }
        }

        void ProcessLine(string str, double width, List<PrintLine> list)
        {
            str = str.TrimEnd(' ');

            if (TextWrapping == TextWrapping.NoWrap)
            {
                do
                {
                    var length = str.Length;

                    while (GetFormattedText(str.Substring(0, length)).Width > width)
                        length--;

                    list.Add(new PrintLine(str.Substring(0, length), length < str.Length));

                    str = str.Substring(length);
                } while (str.Length > 0);
            }
            else
            {
                do
                {
                    var length = str.Length;
                    var flag = false;

                    while (GetFormattedText(str.Substring(0, length)).Width > width)
                    {
                        var index = str.LastIndexOfAny(charsBreak, length - 2);

                        if (index != -1)
                            length = index + 1;
                        else
                        {
                            index = str.IndexOfAny(charsBreak);

                            if (index != -1)
                                length = index + 1;

                            if (TextWrapping == TextWrapping.Wrap)
                            {
                                while (GetFormattedText(str.Substring(0, length)).Width > width)
                                    length--;
                                flag = true;
                            }
                            break;
                        }
                    }

                    list.Add(new PrintLine(str.Substring(0, length), flag));
                    str = str.Substring(length);
                } while (str.Length > 0);
            }
        }

        FormattedText GetFormattedText(string str)
        {
            return new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface, FaceSize, Brushes.Black, VisualTreeHelper.GetDpi(Application.Current.MainWindow).PixelsPerDip);
        }
    }
}
