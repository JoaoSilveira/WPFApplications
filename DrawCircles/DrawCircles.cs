using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawCircles
{
    class DrawCircles : Window
    {
        Canvas canv;
        bool isDrawing;
        Ellipse elips;
        Point center;
        bool isDragging;
        FrameworkElement elDragging;
        Point mouseStart;
        Point elementStart;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DrawCircles());
        }

        public DrawCircles()
        {
            Title = "Draw Circles";
            Content = canv = new Canvas();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (isDrawing)
                return;

            center = e.GetPosition(canv);
            elips = new Ellipse();
            elips.Stroke = SystemColors.WindowTextBrush;
            elips.StrokeThickness = 1;
            elips.Width = 0;
            elips.Height = 0;
            canv.Children.Add(elips);
            Canvas.SetLeft(elips, center.X);
            Canvas.SetTop(elips, center.Y);

            CaptureMouse();
            isDrawing = true;
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);

            if (isDrawing)
                return;

            mouseStart = e.GetPosition(canv);
            elDragging = canv.InputHitTest(mouseStart) as FrameworkElement;

            if (elDragging == null)
                return;

            elementStart = new Point(Canvas.GetLeft(elDragging), Canvas.GetTop(elDragging));
            isDragging = true;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.ChangedButton != MouseButton.Middle)
                return;

            var shape = canv.InputHitTest(e.GetPosition(canv)) as Shape;

            if (shape == null)
                return;

            shape.Fill = shape.Fill == Brushes.Red ? Brushes.Transparent : Brushes.Red;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var mouse = e.GetPosition(canv);

            if (isDrawing)
            {
                var radius = Math.Sqrt(Math.Pow(mouse.X - center.X, 2) + Math.Pow(mouse.Y - center.Y, 2));

                Canvas.SetLeft(elips, center.X - radius);
                Canvas.SetTop(elips, center.Y - radius);

                elips.Width = radius * 2;
                elips.Height = radius * 2;
            }
            else if (isDragging)
            {
                Canvas.SetLeft(elDragging, elementStart.X + mouse.X - mouseStart.X);
                Canvas.SetTop(elDragging, elementStart.Y + mouse.Y - mouseStart.Y);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);


            if (isDrawing && e.ChangedButton == MouseButton.Left)
            {
                elips.Stroke = Brushes.Blue;
                elips.StrokeThickness = Math.Min(24, elips.Width / 2);
                elips.Fill = Brushes.Red;

                isDrawing = false;
                ReleaseMouseCapture();
            }
            else if (isDragging && e.ChangedButton == MouseButton.Right)
            {
                isDragging = false;
            }
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            if (e.Text.IndexOf('\x1b') != -1)
            {
                if (isDrawing)
                    ReleaseMouseCapture();
                else if (isDragging)
                {
                    Canvas.SetLeft(elDragging, elementStart.X);
                    Canvas.SetTop(elDragging, elementStart.Y);

                    isDragging = false;
                }
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);

            if (isDrawing)
            {
                canv.Children.Remove(elips);
                isDrawing = false;
            }
        }
    }
}
