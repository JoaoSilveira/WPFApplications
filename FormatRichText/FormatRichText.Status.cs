using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace FormatRichText
{
    public partial class FormatRichText : Window
    {
        StatusBarItem itemDateTime;

        void AddStatusBar(DockPanel dock)
        {
            var status = new StatusBar();
            dock.Children.Add(status);
            DockPanel.SetDock(status, Dock.Bottom);

            itemDateTime = new StatusBarItem();
            itemDateTime.HorizontalAlignment = HorizontalAlignment.Right;
            status.Items.Add(itemDateTime);

            var tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromSeconds(1);
            tmr.Tick += TimerOnTick;
            tmr.Start();
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            var dt = DateTime.Now;
            itemDateTime.Content = $"{dt.ToLongDateString()} {dt.ToLongTimeString()}";
        }
    }
}
