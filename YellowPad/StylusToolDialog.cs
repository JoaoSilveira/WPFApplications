using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;

namespace YellowPad
{
    public partial class StylusToolDialog : Window
    {
        public DrawingAttributes DrawingAttributes
        {
            get
            {
                var drawattr = new DrawingAttributes();

                drawattr.Width = double.Parse(txtboxWidth.Text) / .75;
                drawattr.Height = double.Parse(txtboxHeight.Text) / .75;
                drawattr.StylusTipTransform = new RotateTransform(double.Parse(txtboxAngle.Text)).Value;
                drawattr.IgnorePressure = chkboxPressure.IsChecked.GetValueOrDefault();
                drawattr.IsHighlighter = chkboxHighlighter.IsChecked.GetValueOrDefault();
                drawattr.StylusTip = radioEllipse.IsChecked.GetValueOrDefault() ? StylusTip.Ellipse : StylusTip.Rectangle;
                drawattr.Color = lstboxColor.SelectedColor;

                return drawattr;
            }
            set
            {
                txtboxWidth.Text = (.75 * value.Width).ToString("F1");
                txtboxHeight.Text = (.75 * value.Height).ToString("F1");
                txtboxAngle.Text = (180 * Math.Acos(value.StylusTipTransform.M11) / Math.PI).ToString("F1");
                chkboxPressure.IsChecked = value.IgnorePressure;
                chkboxHighlighter.IsChecked = value.IsHighlighter;

                if (value.StylusTip == StylusTip.Ellipse)
                    radioEllipse.IsChecked = true;
                else
                    radioRect.IsChecked = true;

                lstboxColor.SelectedColor = value.Color;
                lstboxColor.ScrollIntoView(lstboxColor.SelectedColor);
            }
        }

        public StylusToolDialog()
        {
            InitializeComponent();

            txtboxWidth.TextChanged += TextBoxOnTextChanged;
            txtboxHeight.TextChanged += TextBoxOnTextChanged;
            txtboxAngle.TextChanged += TextBoxOnTextChanged;

            txtboxWidth.Focus();
        }

        private void TextBoxOnTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            btnOk.IsEnabled =
                double.TryParse(txtboxWidth.Text, out var value) && value / .75 >= DrawingAttributes.MinWidth && value <= DrawingAttributes.MaxWidth &&
                double.TryParse(txtboxHeight.Text, out value) && value / .75 >= DrawingAttributes.MinHeight && value <= DrawingAttributes.MaxHeight &&
                double.TryParse(txtboxAngle.Text, out _);
        }

        void OkOnClick(object sender, RoutedEventArgs e) => DialogResult = true;
    }
}
