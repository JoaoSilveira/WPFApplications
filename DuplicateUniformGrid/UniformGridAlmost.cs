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

namespace DuplicateUniformGrid
{
    class UniformGridAlmost : Panel
    {
        public static DependencyProperty ColumnsProperty;

        static UniformGridAlmost()
        {
            ColumnsProperty = DependencyProperty.Register(nameof(Columns), typeof(int), typeof(UniformGridAlmost), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsParentMeasure));
        }

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public int Rows => (InternalChildren.Count + Columns - 1) / Columns;

        protected override Size MeasureOverride(Size availableSize)
        {
            var sizeChildren = new Size(availableSize.Width / Columns, availableSize.Height / Rows);

            var maxWidth = double.MinValue;
            var maxHeight = double.MinValue;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(sizeChildren);

                maxWidth = Math.Max(maxWidth, child.DesiredSize.Width);
                maxHeight = Math.Max(maxHeight, child.DesiredSize.Height);
            }

            return new Size(Columns * maxWidth, Rows * maxHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var sizeChildren = new Size(finalSize.Width / Columns, finalSize.Height / Rows);

            for (var index = 0; index < InternalChildren.Count; index++)
            {
                int row = index / Columns;
                int col = index % Columns;

                var rectChild = new Rect(new Point(col * sizeChildren.Width, row * sizeChildren.Height), sizeChildren);

                InternalChildren[index].Arrange(rectChild);
            }

            return finalSize;
        }
    }
}
