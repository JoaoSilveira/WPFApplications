using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NavigateTheWeb
{
    class UriDialog : Window
    {
        TextBox textbox;

        public UriDialog()
        {
            Title = "Enter a URI";
            ShowInTaskbar = false;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            textbox = new TextBox();
            textbox.Margin = new Thickness(48);
            Content = textbox;

            textbox.Focus();
        }

        public string Text
        {
            get
            {
                return textbox.Text;
            }
            set
            {
                textbox.Text = value;
                textbox.SelectionStart = textbox.Text.Length;
            }
        }
    }
}
