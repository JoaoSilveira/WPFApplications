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

namespace BindTheButton
{
    class BindTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new BindTheButton());
        }

        public BindTheButton()
        {
            Title = "Bind the Button";

            var button = new ToggleButton();
            button.Content = "Make _Topmost";
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.SetBinding(ToggleButton.IsCheckedProperty, "Topmost");
            button.DataContext = this;
            Content = button;

            var tip = new ToolTip();
            tip.Content = "Toggle the button on to make the window topmost on the desktop";
            button.ToolTip = tip;
        }
    }
}
