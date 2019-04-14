using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DiagonalizeTheButtons
{
    class DiagonalPanel : FrameworkElement
    {
        public static readonly DependencyProperty BackgroundProperty;

        static DiagonalPanel()
        {
            BackgroundProperty = DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(DiagonalPanel), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        List<UIElement> children = new List<UIElement>();

        Size sizeChildrenTotal;

        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public void Add(UIElement child)
        {
            children.Add(child);
            AddVisualChild(child);
            AddLogicalChild(child);
            InvalidateMeasure();
        }

        public void Remove(UIElement child)
        {
            children.Remove(child);
            RemoveVisualChild(child);
            RemoveLogicalChild(child);
            InvalidateMeasure();
        }

        public int IndexOf(UIElement child) => children.IndexOf(child);

        protected override int VisualChildrenCount => children.Count;

        protected override Visual GetVisualChild(int index)
        {
            if (index >= children.Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return children[index];
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            sizeChildrenTotal = new Size();

            foreach (var child in children)
            {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                sizeChildrenTotal.Width += child.DesiredSize.Width;
                sizeChildrenTotal.Height += child.DesiredSize.Height;
            }

            return sizeChildrenTotal;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var upperCorner = new Point();

            foreach (var child in children)
            {
                var sizeChild = new Size();

                sizeChild.Width = child.DesiredSize.Width * (finalSize.Width / sizeChildrenTotal.Width);
                sizeChild.Height = child.DesiredSize.Height * (finalSize.Height / sizeChildrenTotal.Height);

                child.Arrange(new Rect(upperCorner, sizeChild));
                upperCorner.X += sizeChild.Width;
                upperCorner.Y += sizeChild.Height;
            }

            return finalSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Background, null, new Rect(RenderSize));
        }
    }
}
