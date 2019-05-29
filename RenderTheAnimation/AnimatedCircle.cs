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
using System.Windows.Media.Animation;

namespace RenderTheAnimation
{
    public class AnimatedCircle : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            var anima = new DoubleAnimation();
            anima.From = 0;
            anima.To = 100;
            anima.Duration = new Duration(TimeSpan.FromSeconds(1));
            anima.AutoReverse = true;
            anima.RepeatBehavior = RepeatBehavior.Forever;

            var clock = anima.CreateClock();
            drawingContext.DrawEllipse(Brushes.Blue, new Pen(Brushes.Red, 3), new Point(125, 125), null, 0, clock, 0, clock);
        }
    }
}
