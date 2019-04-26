using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace PrintBanner
{
    public class BannerDocumentPaginator : DocumentPaginator
    {
        Size sizePage;
        Size sizeMax = new Size(0, 0);

        public string Text { get; set; } = string.Empty;

        public Typeface Typeface { get; set; } = new Typeface(string.Empty);

        private FormattedText GetFormattedText(char ch, Typeface face, double em)
        {
            return new FormattedText(ch.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, em, Brushes.Black);
        }

        public override bool IsPageCountValid
        {
            get
            {
                foreach (var ch in Text)
                {
                    var formtxt = GetFormattedText(ch, Typeface, 100);

                    sizeMax.Width = Math.Max(sizeMax.Width, formtxt.Width);
                    sizeMax.Height = Math.Max(sizeMax.Height, formtxt.Height);
                }
                return true;
            }
        }

        public override int PageCount => Text?.Length ?? 0;

        public override Size PageSize
        {
            get { return sizePage; }
            set { sizePage = value; }
        }

        public override IDocumentPaginatorSource Source => null;

        public override DocumentPage GetPage(int pageNumber)
        {
            var vis = new DrawingVisual();

            using (var dc = vis.RenderOpen())
            {
                var factor = Math.Min(
                    (PageSize.Width - 96d) / sizeMax.Width,
                    (PageSize.Height - 96d) / sizeMax.Height);

                var formtxt = GetFormattedText(Text[pageNumber], Typeface, factor * 100d);
                var ptText = new Point((PageSize.Width - formtxt.Width) / 2, (PageSize.Height - formtxt.Height) / 2);

                dc.DrawText(formtxt, ptText);

                return new DocumentPage(vis);
            }
        }
    }
}
