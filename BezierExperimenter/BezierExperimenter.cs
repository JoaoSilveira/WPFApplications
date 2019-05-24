using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BezierExperimenter
{
    public partial class BezierExperimenter : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new BezierExperimenter());
        }

        public BezierExperimenter()
        {
            InitializeComponent();
            canvas.SizeChanged += CanvasOnSizeChanged;
        }

        protected virtual void CanvasOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ptStart.Center = new Point(e.NewSize.Width / 4, e.NewSize.Height / 2);
            ptCtrl1.Center = new Point(e.NewSize.Width / 2, e.NewSize.Height / 4);
            ptCtrl2.Center = new Point(e.NewSize.Width / 2, 3 * e.NewSize.Height / 4);
            ptEnd.Center = new Point(3 * e.NewSize.Width / 4, e.NewSize.Height / 2);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            var pt = e.GetPosition(canvas);

            if (e.ChangedButton == MouseButton.Left)
                ptCtrl1.Center = pt;

            if (e.ChangedButton == MouseButton.Right)
                ptCtrl2.Center = pt;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var pt = e.GetPosition(canvas);

            if (e.LeftButton == MouseButtonState.Pressed)
                ptCtrl1.Center = pt;
            if (e.RightButton == MouseButtonState.Pressed)
                ptCtrl2.Center = pt;
        }
    }
}
