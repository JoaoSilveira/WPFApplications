using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;

namespace YellowPad
{
    public class EraserToolDialog : StylusToolDialog
    {
        public StylusShape EraserShape
        {
            get
            {
                if (radioEllipse.IsChecked.GetValueOrDefault())
                    return new EllipseStylusShape(double.Parse(txtboxWidth.Text) / .75, double.Parse(txtboxHeight.Text) / .75, double.Parse(txtboxAngle.Text));
                else
                    return new RectangleStylusShape(double.Parse(txtboxWidth.Text) / .75, double.Parse(txtboxHeight.Text) / .75, double.Parse(txtboxAngle.Text));
            }
            set
            {
                txtboxWidth.Text = (.75 * value.Width).ToString("F1");
                txtboxHeight.Text = (.75 * value.Height).ToString("F1");
                txtboxAngle.Text = value.Rotation.ToString("F1");

                if (value is EllipseStylusShape)
                    radioEllipse.IsChecked = true;
                else
                    radioRect.IsChecked = true;
            }
        }

        public EraserToolDialog()
        {
            Title = "Eraser Tool";
            chkboxPressure.Visibility = Visibility.Collapsed;
            chkboxHighlighter.Visibility = Visibility.Collapsed;
        }
    }
}
