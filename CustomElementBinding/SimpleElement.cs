using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomElementBinding
{
    public class SimpleElement : FrameworkElement
    {
        public static readonly DependencyProperty NumberProperty;

        static SimpleElement()
        {
            NumberProperty = DependencyProperty.Register(nameof(Number), typeof(double), typeof(SimpleElement), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        public double Number
        {
            get { return (double)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize) => new Size(200, 50);

        protected override void OnRender(DrawingContext dc) 
            => dc.DrawText(new FormattedText(
                Number.ToString(),
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Times New Roman"),
                12,
                SystemColors.WindowTextBrush,
                VisualTreeHelper.GetDpi(this).PixelsPerDip),
                new Point());
    }
}
