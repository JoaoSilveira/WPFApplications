using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YellowPad
{
    public partial class YellowPadWindow : Window
    {
        void StylusToolOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new StylusToolDialog();
            dlg.Owner = this;
            dlg.DrawingAttributes = inkcanv.DefaultDrawingAttributes;

            if (dlg.ShowDialog().GetValueOrDefault())
                inkcanv.DefaultDrawingAttributes = dlg.DrawingAttributes;
        }

        void EraserToolOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new EraserToolDialog();
            dlg.Owner = this;
            dlg.EraserShape = inkcanv.EraserShape;

            if (dlg.ShowDialog().GetValueOrDefault())
                inkcanv.EraserShape = dlg.EraserShape;
        }
    }
}
