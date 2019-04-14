using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CalculateInHex
{
    class RoundedButtonDecorator : Decorator
    {
        public static readonly DependencyProperty IsPressedProperty;

        static RoundedButtonDecorator()
        {
            IsPressedProperty = DependencyProperty.Register(
                nameof(IsPressed),
                typeof(bool),
                typeof(RoundedButtonDecorator),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var desired = new Size(2, 2);
            constraint.Width -= 2;
            constraint.Height -= 2;

            if (Child != null)
            {
                Child.Measure(constraint);
                desired.Width += Child.DesiredSize.Width;
                desired.Height += Child.DesiredSize.Height;
            }

            return desired;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if(Child != null)
            {
                var ptChild = new Point(
                    Math.Max(1, (arrangeSize.Width - Child.DesiredSize.Width) / 2),
                    Math.Max(1, (arrangeSize.Height - Child.DesiredSize.Height) / 2));

                Child.Arrange(new Rect(ptChild, Child.DesiredSize));
            }
            return arrangeSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var brush = new RadialGradientBrush(
                IsPressed ? SystemColors.ControlDarkColor : SystemColors.ControlLightLightColor,
                SystemColors.ControlColor);

            brush.GradientOrigin = IsPressed ? new Point(.75, .75) : new Point(.25, .25);

            drawingContext.DrawRoundedRectangle(
                brush,
                new Pen(SystemColors.ControlDarkDarkBrush, 1),
                new Rect(new Point(), RenderSize),
                RenderSize.Height / 2,
                RenderSize.Height / 2);
        }
    }
}
