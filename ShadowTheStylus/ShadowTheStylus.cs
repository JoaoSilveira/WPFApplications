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

namespace ShadowTheStylus
{
    class ShadowTheStylus : Window
    {
        const double widthStroke = 96 / 2.54;
        static readonly SolidColorBrush brushStylus = Brushes.Blue;
        static readonly SolidColorBrush brushShadow = Brushes.LightBlue;
        static readonly Vector vecShadow = new Vector(widthStroke / 4, widthStroke / 4);

        Canvas canv;
        Polyline polyStylus;
        Polyline polyShadow;
        bool isDrawing;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ShadowTheStylus());
        }

        public ShadowTheStylus()
        {
            Title = "Shadow the Stylus";
            Content = canv = new Canvas();
        }

        protected override void OnStylusDown(StylusDownEventArgs e)
        {
            base.OnStylusDown(e);
            var ptStylus = e.GetPosition(canv);

            polyStylus = new Polyline();
            polyStylus.Stroke = brushStylus;
            polyStylus.StrokeThickness = widthStroke;
            polyStylus.StrokeStartLineCap = PenLineCap.Round;
            polyStylus.StrokeEndLineCap = PenLineCap.Round;
            polyStylus.StrokeLineJoin = PenLineJoin.Round;
            polyStylus.Points = new PointCollection();
            polyStylus.Points.Add(ptStylus);

            polyShadow = new Polyline();
            polyShadow.Stroke = brushShadow;
            polyShadow.StrokeThickness = widthStroke;
            polyShadow.StrokeStartLineCap = PenLineCap.Round;
            polyShadow.StrokeEndLineCap = PenLineCap.Round;
            polyShadow.StrokeLineJoin = PenLineJoin.Round;
            polyShadow.Points = new PointCollection();
            polyShadow.Points.Add(ptStylus + vecShadow);

            canv.Children.Insert(canv.Children.Count / 2, polyShadow);
            canv.Children.Add(polyStylus);

            CaptureStylus();
            isDrawing = true;
            e.Handled = true;
        }

        protected override void OnStylusMove(StylusEventArgs e)
        {
            base.OnStylusMove(e);

            if (isDrawing)
            {
                var point = e.GetPosition(canv);

                polyStylus.Points.Add(point);
                polyShadow.Points.Add(point + vecShadow);

                e.Handled = true;
            }
        }

        protected override void OnStylusUp(StylusEventArgs e)
        {
            base.OnStylusUp(e);

            if (isDrawing)
            {
                isDrawing = false;
                ReleaseStylusCapture();
                e.Handled = true;
            }
        }

        protected override void OnLostStylusCapture(StylusEventArgs e)
        {
            base.OnLostStylusCapture(e);

            if (isDrawing)
            {
                canv.Children.Remove(polyStylus);
                canv.Children.Remove(polyShadow);
                isDrawing = false;
            }
        }
    }
}
