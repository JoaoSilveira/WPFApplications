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

namespace PaintOnCanvasClone
{
    public class CanvasClone : Panel
    {
        public static readonly DependencyProperty LeftProperty;
        public static readonly DependencyProperty TopProperty;

        static CanvasClone()
        {
            LeftProperty = DependencyProperty.RegisterAttached("Left", typeof(double), typeof(CanvasClone), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsParentArrange));
            TopProperty = DependencyProperty.RegisterAttached("Top", typeof(double), typeof(CanvasClone), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsParentArrange));
        }
        
        public static void SetLeft(DependencyObject obj, double value)
        {
            obj.SetValue(LeftProperty, value);
        }

        public static double GetLeft(DependencyObject obj) => (double)obj.GetValue(LeftProperty);
        
        public static void SetTop(DependencyObject obj, double value)
        {
            obj.SetValue(TopProperty, value);
        }

        public static double GetTop(DependencyObject obj) => (double)obj.GetValue(TopProperty);

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(new Point(GetLeft(child), GetTop(child)), child.DesiredSize));
            }

            return finalSize;
        }
    }
}
