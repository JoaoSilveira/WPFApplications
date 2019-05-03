using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace XamlCruncher
{
    class XamlOrientationMenuItem : MenuItem
    {
        MenuItem itemChecked;
        Grid grid;
        TextBox txtbox;
        Frame frame;

        public Dock Orientation
        {
            get { return (Dock)itemChecked.Tag; }
            set
            {
                foreach (MenuItem item in Items)
                    if (item.IsChecked = (Dock)item.Tag == value)
                        itemChecked = item;
            }
        }

        public XamlOrientationMenuItem(Grid grid, TextBox txtbox, Frame frame)
        {
            this.grid = grid;
            this.txtbox = txtbox;
            this.frame = frame;
            Header = "_Orientation";

            for (var i = 0; i < 4; i++)
                Items.Add(CreateItem((Dock)i));

            (itemChecked = (MenuItem)Items[0]).IsChecked = true;
        }

        private MenuItem CreateItem(Dock dock)
        {
            var item = new MenuItem();
            item.Tag = dock;
            item.Click += ItemOnClick;
            item.Checked += ItemOnChecked;

            var formtxt1 = CreateFormattedText("Edit");
            var formtxt2 = CreateFormattedText("Display");
            var widthMax = Math.Max(formtxt1.Width, formtxt2.Width);

            var vis = new DrawingVisual();
            using (var dc = vis.RenderOpen())
            {
                switch (dock)
                {
                    case Dock.Left:
                        BoxText(dc, formtxt1, formtxt1.Width, new Point());
                        BoxText(dc, formtxt2, formtxt2.Width, new Point(formtxt1.Width + 4, 0));
                        break;
                    case Dock.Top:
                        BoxText(dc, formtxt1, widthMax, new Point());
                        BoxText(dc, formtxt2, widthMax, new Point(0, formtxt1.Height + 4));
                        break;
                    case Dock.Right:
                        BoxText(dc, formtxt2, formtxt2.Width, new Point());
                        BoxText(dc, formtxt1, formtxt1.Width, new Point(formtxt2.Width + 4, 0));
                        break;
                    case Dock.Bottom:
                        BoxText(dc, formtxt2, widthMax, new Point());
                        BoxText(dc, formtxt1, widthMax, new Point(0, formtxt2.Height + 4));
                        break;
                }
            }
            var drawing = new DrawingImage(vis.Drawing);
            var img = new Image();
            img.Source = drawing;

            item.Header = img;

            return item;
        }

        private FormattedText CreateFormattedText(string str) => new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface (SystemFonts.MenuFontFamily, SystemFonts.MenuFontStyle, SystemFonts.MenuFontWeight, FontStretches.Normal), SystemFonts.MenuFontSize, SystemColors.MenuTextBrush, VisualTreeHelper.GetDpi(this).PixelsPerDip);

        private void BoxText(DrawingContext dc, FormattedText formtxt, double width, Point pt)
        {
            var pen = new Pen(SystemColors.MenuTextBrush, 1);

            dc.DrawRectangle(null, pen, new Rect(pt.X, pt.Y, width + 4, formtxt.Height + 4));
            dc.DrawText(formtxt, new Point(pt.X + (width - formtxt.Width) / 2 + 2, pt.Y + 2));
        }

        private void ItemOnClick(object sender, RoutedEventArgs e)
        {
            itemChecked.IsChecked = false;
            itemChecked = e.Source as MenuItem;
            itemChecked.IsChecked = true;
        }

        private void ItemOnChecked(object sender, RoutedEventArgs e)
        {
            var itemChecked = e.Source as MenuItem;

            for (var i = 1; i < 3; i++)
            {
                grid.RowDefinitions[i].Height = new GridLength(0);
                grid.ColumnDefinitions[i].Width = new GridLength(0);
            }

            Grid.SetRow(txtbox, 0);
            Grid.SetColumn(txtbox, 0);
            Grid.SetRow(frame, 0);
            Grid.SetColumn(frame, 0);

            switch ((Dock)itemChecked.Tag)
            {
                case Dock.Left:
                    grid.ColumnDefinitions[1].Width = GridLength.Auto;
                    grid.ColumnDefinitions[2].Width = new GridLength(100, GridUnitType.Star);
                    Grid.SetColumn(frame, 2);
                    break;
                case Dock.Top:
                    grid.RowDefinitions[1].Height = GridLength.Auto;
                    grid.RowDefinitions[2].Height = new GridLength(100, GridUnitType.Star);
                    Grid.SetRow(frame, 2);
                    break;
                case Dock.Right:
                    grid.ColumnDefinitions[1].Width = GridLength.Auto;
                    grid.ColumnDefinitions[2].Width = new GridLength(100, GridUnitType.Star);
                    Grid.SetColumn(txtbox, 2);
                    break;
                case Dock.Bottom:
                    grid.RowDefinitions[1].Height = GridLength.Auto;
                    grid.RowDefinitions[2].Height = new GridLength(100, GridUnitType.Star);
                    Grid.SetRow(txtbox, 2);
                    break;
            }
        }
    }
}
