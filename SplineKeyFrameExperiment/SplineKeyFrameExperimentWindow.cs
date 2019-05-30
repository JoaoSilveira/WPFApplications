using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SplineKeyFrameExperiment
{
    public partial class SplineKeyFrameExperimentWindow : Window
    {
        public static readonly DependencyProperty ControlPoint1Property = DependencyProperty.Register(nameof(ControlPoint1), typeof(Point), typeof(SplineKeyFrameExperimentWindow), new PropertyMetadata(new Point(), ControlPointOnChanged));

        public static readonly DependencyProperty ControlPoint2Property = DependencyProperty.Register(nameof(ControlPoint2), typeof(Point), typeof(SplineKeyFrameExperimentWindow), new PropertyMetadata(new Point(), ControlPointOnChanged));

        public Point ControlPoint1
        {
            get { return (Point)GetValue(ControlPoint1Property); }
            set { SetValue(ControlPoint1Property, value); }
        }

        public Point ControlPoint2
        {
            get { return (Point)GetValue(ControlPoint2Property); }
            set { SetValue(ControlPoint2Property, value); }
        }

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SplineKeyFrameExperimentWindow());
        }

        public SplineKeyFrameExperimentWindow()
        {
            InitializeComponent();

            for (var i = 0; i <= 10; i++)
            {
                var txtblk = new TextBlock();
                txtblk.Text = (i / 10m).ToString("N1");

                canvMain.Children.Add(txtblk);
                Canvas.SetLeft(txtblk, 40 + 48 * i);
                Canvas.SetTop(txtblk, 14);

                var line = new Line();
                line.X1 = 48 * (i + 1);
                line.Y1 = 30;
                line.X2 = line.X1;
                line.Y2 = 528;
                line.Stroke = Brushes.Black;
                canvMain.Children.Add(line);

                txtblk = new TextBlock();
                txtblk.Text = (i / 10m).ToString("N1");

                canvMain.Children.Add(txtblk);
                Canvas.SetLeft(txtblk, 5);
                Canvas.SetTop(txtblk, 40 + 48 * i);

                line = new Line();
                line.X1 = 30;
                line.Y1 = 48 * (i + 1);
                line.X2 = 528;
                line.Y2 = line.Y1;
                line.Stroke = Brushes.Black;
                canvMain.Children.Add(line);
            }

            UpdateLabel();
        }

        private static void ControlPointOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var win = d as SplineKeyFrameExperimentWindow;

            if (e.Property == ControlPoint1Property)
                win.spline.ControlPoint1 = (Point)e.NewValue;
            else if (e.Property == ControlPoint2Property)
                win.spline.ControlPoint2 = (Point)e.NewValue;
        }

        void CanvasOnMouse(object sender, MouseEventArgs e)
        {
            var canv = sender as Canvas;
            var ptMouse = e.GetPosition(canv);
            ptMouse.X = Math.Min(1, Math.Max(0, ptMouse.X / canv.ActualWidth));
            ptMouse.Y = Math.Min(1, Math.Max(0, ptMouse.Y / canv.ActualHeight));

            if (e.LeftButton == MouseButtonState.Pressed)
                ControlPoint1 = ptMouse;
            if (e.RightButton == MouseButtonState.Pressed)
                ControlPoint2 = ptMouse;

            if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
                UpdateLabel();
        }

        private void UpdateLabel() => lblInfo.Content = $"Left mouse button changes ControlPoint1 = ({ControlPoint1:F2})\nRight mouse button changes ControlPoint2 = ({ControlPoint2:F2})";
    }
}
