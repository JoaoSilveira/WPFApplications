using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChooseFont
{
    class ChooseFont : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ChooseFont());
        }

        public ChooseFont()
        {
            Title = "Choose Font";

            var btn = new Button();
            btn.Content = Title;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new FontDialog();
            dlg.Owner = this;
            dlg.Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            dlg.FaceSize = FontSize;

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            FontFamily = dlg.Typeface.FontFamily;
            FontStyle = dlg.Typeface.Style;
            FontWeight = dlg.Typeface.Weight;
            FontStretch = dlg.Typeface.Stretch;
            FontSize = dlg.FaceSize;
        }
    }
}
