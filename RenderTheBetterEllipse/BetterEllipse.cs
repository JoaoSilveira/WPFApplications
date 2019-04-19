using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace RenderTheBetterEllipse
{
    public class BetterEllipse : FrameworkElement
    {
        public static readonly DependencyProperty FillProperty;
        public static readonly DependencyProperty StrokeProperty;

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public Pen Stroke
        {
            get { return (Pen)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        static BetterEllipse()
        {
            FillProperty = DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(BetterEllipse), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
            StrokeProperty = DependencyProperty.Register(nameof(Stroke), typeof(Pen), typeof(BetterEllipse), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Stroke == null)
                return base.MeasureOverride(availableSize);

            return new Size(Stroke.Thickness, Stroke.Thickness);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var size = RenderSize;

            if (Stroke != null)
            {
                size.Width = Math.Max(0, size.Width - Stroke.Thickness);
                size.Height = Math.Max(0, size.Height - Stroke.Thickness);
            }

            drawingContext.DrawEllipse(Fill, Stroke, new Point(RenderSize.Width / 2, RenderSize.Height / 2), size.Width / 2, size.Height / 2);
        }
    }
}
