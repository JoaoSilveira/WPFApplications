using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace HybridClock
{
    public class ClockTicker : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string DateTime => System.DateTime.Now.ToString(Format);

        public string Format { get; set; }

        public ClockTicker()
        {
            var timer = new DispatcherTimer();
            timer.Tick += TimerOnTick;
            timer.Interval = TimeSpan.FromSeconds(0.10);
            timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateTime)));
    }
}
