using RenderTheBetterEllipse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EncloseElementInEllipse
{
    class EllipseWithChild : BetterEllipse
    {
        UIElement child;

        public UIElement Child
        {
            get { return child; }
            set
            {
                if (child != null)
                {
                    RemoveVisualChild(child);
                    RemoveLogicalChild(child);
                }

                if ((child = value) != null)
                {
                    AddVisualChild(child);
                    AddLogicalChild(child);
                }
            }
        }

        protected override int VisualChildrenCount => child == null ? 0 : 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0 || child == null)
                throw new ArgumentOutOfRangeException(nameof(index));

            return child;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var sizeDesired = new Size(0, 0);

            if (Stroke != null)
            {
                sizeDesired.Width = 2 * Stroke.Thickness;
                sizeDesired.Height = 2 * Stroke.Thickness;

                availableSize.Width = Math.Max(0, availableSize.Width - 2 * Stroke.Thickness);
                availableSize.Height = Math.Max(0, availableSize.Height - 2 * Stroke.Thickness);
            }

            if (child != null)
            {
                child.Measure(availableSize);
                sizeDesired.Width += child.DesiredSize.Width;
                sizeDesired.Height += child.DesiredSize.Height;
            }

            return sizeDesired;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (child != null)
            {
                var rect = new Rect(new Point(
                    (finalSize.Width - child.DesiredSize.Width) / 2,
                    (finalSize.Height - child.DesiredSize.Height) / 2),
                    child.DesiredSize);

                child.Arrange(rect);
            }
            return finalSize;
        }
    }
}
