using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BezierReproduce
{
    public class BezierReproduce : BezierExperimenter.BezierExperimenter
    {
        Polyline bezier;

        [STAThread]
        public new static void Main()
        {
            var app = new Application();
            app.Run(new BezierReproduce());
        }

        public BezierReproduce()
        {
            Title = "Bezier Reproduce";

            bezier = new Polyline();
            bezier.Stroke = Brushes.Blue;
            canvas.Children.Add(bezier);
        }

        protected override void CanvasOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            base.CanvasOnSizeChanged(sender, e);
            DrawBezierManually();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            DrawBezierManually();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            DrawBezierManually();
        }

        private void DrawBezierManually()
        {
            var pts = new Point[10];

            for (var i = 0; i < pts.Length; i++)
            {
                var t = (double)i / (pts.Length - 1);

                var x = (1 - t) * (1 - t) * (1 - t) * ptStart.Center.X + 3 * t * (1 - t) * (1 - t) * ptCtrl1.Center.X + 3 * t * t * (1 - t) * ptCtrl2.Center.X + t * t * t * ptEnd.Center.X;
                var y = (1 - t) * (1 - t) * (1 - t) * ptStart.Center.Y + 3 * t * (1 - t) * (1 - t) * ptCtrl1.Center.Y + 3 * t * t * (1 - t) * ptCtrl2.Center.Y + t * t * t * ptEnd.Center.Y;

                pts[i] = new Point(x, y);
            }
            bezier.Points = new PointCollection(pts);
        }
    }
}
