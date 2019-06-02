using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HybridClock
{
    /// <summary>
    /// Interaction logic for HybridClockPage.xaml
    /// </summary>
    public partial class HybridClockPage : Page
    {
        public static readonly Color clrBackground = Colors.Aqua;
        public static readonly Brush brushBackgroudn = Brushes.Aqua;

        TranslateTransform[] xform = new TranslateTransform[60];

        public HybridClockPage()
        {
            InitializeComponent();

            storyboard.BeginTime = -DateTime.Now.TimeOfDay;

            var menu = new ContextMenu();
            menu.Opened += ContextMenuOnOpened;
            ContextMenu = menu;

            Loaded += WindowOnLoaded;
        }

        private void WindowOnLoaded(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < 60; i++)
            {
                var elips = new Ellipse();
                elips.HorizontalAlignment = HorizontalAlignment.Center;
                elips.VerticalAlignment = VerticalAlignment.Center;
                elips.Fill = Brushes.Blue;
                elips.Width = elips.Height = i % 5 == 0 ? 6 : 2;

                var group = new TransformGroup();
                group.Children.Add(xform[i] = new TranslateTransform(datetime.ActualWidth, 0));
                group.Children.Add(new TranslateTransform(grd.Margin.Left / 2, 0));
                group.Children.Add(new TranslateTransform(-elips.Width / 2, -elips.Height / 2));
                group.Children.Add(new RotateTransform(i * 6));
                group.Children.Add(new TranslateTransform(elips.Width / 2, elips.Height / 2));

                elips.RenderTransform = group;
                grd.Children.Add(elips);
            }
            MakeMask();

            datetime.SizeChanged += DateTimeOnSizeChanged;
        }

        private void DateTimeOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!e.WidthChanged)
                return;

            for (var i = 0; i < 60; i++)
                xform[i].X = datetime.ActualWidth;
            MakeMask();
        }

        private void MakeMask()
        {
            var group = new DrawingGroup();
            var ptCenter = new Point(datetime.ActualWidth + grd.Margin.Left, datetime.ActualWidth + grd.Margin.Left);

            for (var i = 0; i < 256; i++)
            {
                var ptInner1 = new Point(
                    ptCenter.X + datetime.ActualWidth * Math.Cos(i * 2 * Math.PI / 256),
                    ptCenter.Y + datetime.ActualWidth * Math.Sin(i * 2 * Math.PI / 256)
                );
                var ptInner2 = new Point(
                    ptCenter.X + datetime.ActualWidth * Math.Cos((i + 2) * 2 * Math.PI / 256),
                    ptCenter.Y + datetime.ActualWidth * Math.Sin((i + 2) * 2 * Math.PI / 256)
                );
                var ptOuter1 = new Point(
                    ptCenter.X + (datetime.ActualWidth + grd.Margin.Left) * Math.Cos(i * 2 * Math.PI / 256),
                    ptCenter.Y + (datetime.ActualWidth + grd.Margin.Left) * Math.Sin(i * 2 * Math.PI / 256)
                );
                var ptOuter2 = new Point(
                    ptCenter.X + (datetime.ActualWidth + grd.Margin.Left) * Math.Cos((i + 2) * 2 * Math.PI / 256),
                    ptCenter.Y + (datetime.ActualWidth + grd.Margin.Left) * Math.Sin((i + 2) * 2 * Math.PI / 256)
                );

                var segcoll = new PathSegmentCollection();
                segcoll.Add(new LineSegment(ptInner2, false));
                segcoll.Add(new LineSegment(ptOuter2, false));
                segcoll.Add(new LineSegment(ptOuter1, false));
                segcoll.Add(new LineSegment(ptInner1, false));

                var fig = new PathFigure(ptInner1, segcoll, true);
                var figcoll = new PathFigureCollection();
                figcoll.Add(fig);

                var path = new PathGeometry(figcoll);
                var byOpacity = (byte)Math.Min(255, 512 - 2 * i);

                var br = new SolidColorBrush(Color.FromArgb(byOpacity, clrBackground.R, clrBackground.G, clrBackground.B));
                var draw = new GeometryDrawing(br, new Pen(br, 2), path);
                group.Children.Add(draw);
            }

            var brush = new DrawingBrush(group);
            mask.Fill = brush;
        }

        private void ContextMenuOnOpened(object sender, RoutedEventArgs e)
        {
            var menu = sender as ContextMenu;
            menu.Items.Clear();

            string[] strFormats =
            {
                "d", "D", "f", "F", "g", "G", "M", "R", "s", "t", "T", "u", "U", "Y"
            };

            foreach (var format in strFormats)
            {
                var item = new MenuItem();
                item.Header = DateTime.Now.ToString(format);
                item.Tag = format;
                item.IsChecked = format == (Resources["clock"] as ClockTicker).Format;
                item.Click += MenuItemOnClick;
                menu.Items.Add(item);
            }
        }

        private void MenuItemOnClick(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            (Resources["clock"] as ClockTicker).Format = item.Tag as string;
        }
    }
}
