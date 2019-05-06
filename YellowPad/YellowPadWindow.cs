using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace YellowPad
{
    public partial class YellowPadWindow : Window
    {
        public static readonly double widthCanvas = 5 * 96;
        public static readonly double heightCanvas = 7 * 96;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new YellowPadWindow());
        }

        public YellowPadWindow()
        {
            InitializeComponent();


            for (var y = 96d; y < heightCanvas; y += 24)
            {
                var line = new Line();
                line.X1 = 0;
                line.Y1 = y;
                line.X2 = widthCanvas;
                line.Y2 = y;
                line.Stroke = Brushes.LightBlue;
                inkcanv.Children.Add(line);
            }

            if (Tablet.TabletDevices.Count == 0)
                menuEraserMode.Visibility = Visibility.Collapsed;
        }
    }
}
