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
    class RoundedButton : Control
    {
        public static readonly RoutedEvent ClickEvent;

        RoundedButtonDecorator decorator;

        static RoundedButton()
        {
            ClickEvent = EventManager.RegisterRoutedEvent(
                nameof(Click),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(RoundedButton));
        }

        public RoundedButton()
        {
            decorator = new RoundedButtonDecorator();
            AddVisualChild(decorator);
            AddLogicalChild(decorator);
        }

        public UIElement Child
        {
            get { return decorator.Child; }
            set { decorator.Child = value; }
        }

        public bool IsPressed
        {
            get { return decorator.IsPressed; }
            set { decorator.IsPressed = value; }
        }

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected override int VisualChildrenCount => 1;

        public bool IsMouseReallyOver
        {
            get
            {
                var pt = Mouse.GetPosition(this);
                return pt.X >= 0 && pt.X < ActualWidth && pt.Y >= 0 && pt.Y < ActualHeight;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return decorator;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            decorator.Measure(constraint);
            return decorator.DesiredSize;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            decorator.Arrange(new Rect(new Point(), arrangeBounds));
            return arrangeBounds;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsMouseCaptured)
                IsPressed = IsMouseReallyOver;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            CaptureMouse();
            IsPressed = true;
            e.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (IsMouseCaptured)
            {
                if (IsMouseReallyOver)
                    OnClick();

                Mouse.Capture(null);
                IsPressed = false;
                e.Handled = true;
            }
        }

        private void OnClick()
        {
            var args = new RoutedEventArgs();
            args.RoutedEvent = ClickEvent;
            args.Source = this;

            RaiseEvent(args);
        }
    }
}
