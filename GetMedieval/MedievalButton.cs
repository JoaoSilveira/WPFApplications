using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace GetMedieval
{
    class MedievalButton : Control
    {
        public static readonly DependencyProperty TextProperty;
        public static readonly RoutedEvent KnockEvent;
        public static readonly RoutedEvent PreviewKnockEvent;

        FormattedText formtext;
        bool isMouseReallyOver;

        static MedievalButton()
        {
            TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(MedievalButton), new FrameworkPropertyMetadata(" ", FrameworkPropertyMetadataOptions.AffectsMeasure));
            KnockEvent = EventManager.RegisterRoutedEvent(nameof(Knock), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MedievalButton));
            PreviewKnockEvent = EventManager.RegisterRoutedEvent(nameof(PreviewKnock), RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(MedievalButton));
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value ?? " "); }
        }

        public event RoutedEventHandler Knock
        {
            add { AddHandler(KnockEvent, value); }
            remove { RemoveHandler(KnockEvent, value); }
        }

        public event RoutedEventHandler PreviewKnock
        {
            add { AddHandler(PreviewKnockEvent, value); }
            remove { RemoveHandler(PreviewKnockEvent, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            formtext = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection, new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Foreground, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            var desiredSize = new Size(Math.Max(48, formtext.Width) + 4, formtext.Height + 4);
            desiredSize.Width += Padding.Left + Padding.Right;
            desiredSize.Height += Padding.Top + Padding.Bottom;

            desiredSize.Width = Math.Min(desiredSize.Width, constraint.Width);
            desiredSize.Height = Math.Min(desiredSize.Height, constraint.Height);

            return desiredSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var brushBackground = SystemColors.ControlBrush;

            if (isMouseReallyOver && IsMouseCaptured)
                brushBackground = SystemColors.ControlDarkBrush;

            drawingContext.PushClip(new RectangleGeometry(new Rect(RenderSize)));

            var pen = new Pen(Foreground, IsMouseOver ? 2 : 1);

            drawingContext.DrawRoundedRectangle(brushBackground, pen, new Rect(new Point(), RenderSize), 4, 4);

            formtext.SetForegroundBrush(IsEnabled ? Foreground : SystemColors.ControlDarkBrush);

            var ptText = new Point(2, 2);

            switch (HorizontalContentAlignment)
            {
                case HorizontalAlignment.Left:
                    ptText.X += Padding.Left;
                    break;
                case HorizontalAlignment.Right:
                    ptText.X += RenderSize.Width - formtext.Width - Padding.Right - 4;
                    break;
                case HorizontalAlignment.Center:
                case HorizontalAlignment.Stretch:
                    ptText.X += (RenderSize.Width - formtext.Width - Padding.Left - Padding.Right) / 2;
                    break;
                default:
                    break;
            }

            switch (VerticalContentAlignment)
            {
                case VerticalAlignment.Top:
                    ptText.Y += Padding.Top;
                    break;
                case VerticalAlignment.Bottom:
                    ptText.Y += RenderSize.Height - formtext.Height - Padding.Bottom - 4;
                    break;
                case VerticalAlignment.Center:
                case VerticalAlignment.Stretch:
                    ptText.Y += (RenderSize.Height - formtext.Height - Padding.Top - Padding.Bottom) / 2;
                    break;
                default:
                    break;
            }

            drawingContext.DrawText(formtext, ptText);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            InvalidateVisual();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            InvalidateVisual();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var pt = e.GetPosition(this);
            var isReallyOverNow = pt.X >= 0 && pt.X < ActualWidth && pt.Y >= 0 && pt.Y < ActualHeight;

            if (isReallyOverNow ^ isMouseReallyOver)
            {
                isMouseReallyOver = isReallyOverNow;
                InvalidateVisual();
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            CaptureMouse();
            InvalidateVisual();
            e.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (IsMouseCaptured)
            {
                if (isMouseReallyOver)
                {
                    OnPreviewKnock();
                    OnKnock();
                }
                e.Handled = true;
                Mouse.Capture(null);
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            InvalidateVisual();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            e.Handled = e.Key == Key.Space || e.Key == Key.Enter;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                OnPreviewKnock();
                OnKnock();
                e.Handled = true;
            }
        }

        protected virtual void OnKnock()
        {
            var argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = KnockEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }

        protected virtual void OnPreviewKnock()
        {
            var argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = PreviewKnockEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }
    }
}
