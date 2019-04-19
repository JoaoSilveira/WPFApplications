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

namespace CircleTheButtons
{
    public class RadialPanel : Panel
    {
        public static readonly DependencyProperty OrientationProperty;

        static RadialPanel()
        {
            OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(RadialPanelOrientation), typeof(RadialPanel), new FrameworkPropertyMetadata(RadialPanelOrientation.ByWidth, FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        bool showPieLines;
        double angleEach;
        Size sizeLargest;
        double radius;
        double outerEdgeFromCenter;
        double innerEdgeFromCenter;

        public RadialPanelOrientation Orientation
        {
            get { return (RadialPanelOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public bool ShowPieLines
        {
            get { return showPieLines; }
            set
            {
                if (value ^ showPieLines)
                    InvalidateVisual();

                showPieLines = value;
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (InternalChildren.Count == 0)
                return new Size();

            angleEach = 360d / InternalChildren.Count;
            sizeLargest = new Size();

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                sizeLargest.Width = Math.Max(sizeLargest.Width, child.DesiredSize.Width);
                sizeLargest.Height = Math.Max(sizeLargest.Height, child.DesiredSize.Height);
            }

            if (Orientation == RadialPanelOrientation.ByWidth)
            {
                innerEdgeFromCenter = sizeLargest.Width / 2d / Math.Tan(Math.PI * angleEach / 360d);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Height;
                radius = Math.Sqrt(Math.Pow(sizeLargest.Width / 2d, 2d) + Math.Pow(outerEdgeFromCenter, 2d));
            }
            else
            {
                innerEdgeFromCenter = sizeLargest.Height / 2d / Math.Tan(Math.PI * angleEach / 360d);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Width;
                radius = Math.Sqrt(Math.Pow(sizeLargest.Height / 2d, 2d) + Math.Pow(outerEdgeFromCenter, 2d));
            }

            return new Size(radius * 2d, radius * 2d);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var angleChild = 0d;
            var center = new Point(finalSize.Width / 2d, finalSize.Height / 2d);
            var multiplier = Math.Min(finalSize.Width / (2d * radius), finalSize.Height / (radius * 2d));

            foreach (UIElement child in InternalChildren)
            {
                child.RenderTransform = Transform.Identity;

                if (Orientation == RadialPanelOrientation.ByWidth)
                {
                    child.Arrange(new Rect(center.X - multiplier * sizeLargest.Width / 2d, center.Y - multiplier * outerEdgeFromCenter, multiplier * sizeLargest.Width, multiplier * sizeLargest.Height));
                }
                else
                {
                    child.Arrange(new Rect(center.X + multiplier * innerEdgeFromCenter, center.Y - multiplier * sizeLargest.Height / 2d, multiplier * sizeLargest.Width, multiplier * sizeLargest.Height));
                }

                var pt = TranslatePoint(center, child);
                child.RenderTransform = new RotateTransform(angleChild, pt.X, pt.Y);

                angleChild += angleEach;
            }

            return  finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            if (!ShowPieLines)
                return;

            var center = new Point(RenderSize.Width / 2d, RenderSize.Height / 2d);
            var multiplier = Math.Min(RenderSize.Width / (radius * 2d), RenderSize.Height / (radius * 2d));
            var pen = new Pen(SystemColors.WindowTextBrush, 1d);
            pen.DashStyle = DashStyles.Dash;

            dc.DrawEllipse(null, pen, center, radius * multiplier, radius * multiplier);

            var angleChild = -angleEach / 2d;

            if (Orientation == RadialPanelOrientation.ByWidth)
                angleChild += 90d;

            foreach (UIElement child in InternalChildren)
            {
                dc.DrawLine(pen, center, new Point(center.X + multiplier * radius * Math.Cos(2d * Math.PI * angleChild / 360d), center.Y + multiplier * radius * Math.Sin(2d * Math.PI * angleChild / 360d)));

                angleChild += angleEach;
            }
        }
    }
}
