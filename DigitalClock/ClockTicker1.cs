using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace DigitalClock
{
    public class ClockTicker1 : DependencyObject
    {
        public static readonly DependencyProperty DateTimeProperty = DependencyProperty.Register(nameof(DateTime), typeof(DateTime), typeof(ClockTicker1));

        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }

        public ClockTicker1()
        {
            var timer = new DispatcherTimer();
            timer.Tick += TimerOnTick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs e) => DateTime = DateTime.Now;
    }
}
