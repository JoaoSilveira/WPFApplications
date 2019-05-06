using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace YellowPad
{
    public partial class YellowPadWindow : Window
    {
        void HelpOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var win = new YellowPadHelp();
            win.Owner = this;
            win.Show();
        }

        void AboutOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new YellowPadAboutDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
        }
    }
}
