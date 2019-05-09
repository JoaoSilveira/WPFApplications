using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FormattedDigitalClock
{
    public class ClockTicker2 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime DateTime => DateTime.Now;

        public ClockTicker2()
        {
            var timer = new DispatcherTimer();
            timer.Tick += TimerOnTick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateTime)));
    }
}
