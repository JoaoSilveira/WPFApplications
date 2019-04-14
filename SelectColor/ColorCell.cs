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

namespace SelectColor
{
    class ColorCell : FrameworkElement
    {
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IsHighlightedProperty;
        static readonly Size sizeCell = new Size(20, 20);

        DrawingVisual visColor;
        Brush brush;

        static ColorCell()
        {
            IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(ColorCell), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
            IsHighlightedProperty = DependencyProperty.Register(nameof(IsHighlighted), typeof(bool), typeof(ColorCell), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        public ColorCell(Color clr)
        {
            visColor = new DrawingVisual();
            var dc = visColor.RenderOpen();

            var rect = new Rect(new Point(), sizeCell);
            rect.Inflate(-4, -4);

            var pen = new Pen(SystemColors.ControlTextBrush, 1);
            brush = new SolidColorBrush(clr);
            dc.DrawRectangle(brush, pen, rect);
            dc.Close();

            AddVisualChild(visColor);
            AddLogicalChild(visColor);
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public bool IsHighlighted
        {
            get { return (bool)GetValue(IsHighlightedProperty); }
            set { SetValue(IsHighlightedProperty, value); }
        }

        public Brush Brush => brush;

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return visColor;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return sizeCell;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var rect = new Rect(new Point(), RenderSize);
            rect.Inflate(-1, -1);

            var pen = new Pen(SystemColors.HighlightBrush, 1);

            if (IsHighlighted)
                drawingContext.DrawRectangle(SystemColors.ControlDarkBrush, pen, rect);
            else if (IsSelected)
                drawingContext.DrawRectangle(SystemColors.ControlLightBrush, pen, rect);
            else
                drawingContext.DrawRectangle(Brushes.Transparent, null, rect);
        }
    }
}
