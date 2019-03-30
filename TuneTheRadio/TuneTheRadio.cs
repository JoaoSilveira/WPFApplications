using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TuneTheRadio
{
    class TuneTheRadio : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new TuneTheRadio());
        }

        public TuneTheRadio()
        {
            Title = "Tune the Radio";
            SizeToContent = SizeToContent.WidthAndHeight;

            var group = new GroupBox();
            group.Header = "Window Style";
            group.Margin = new Thickness(96);
            group.Padding = new Thickness(5);
            Content = group;

            var stack = new StackPanel();
            group.Content = stack;

            stack.Children.Add(CreateRadioButton("No border or caption", WindowStyle.None));
            stack.Children.Add(CreateRadioButton("Single-border window", WindowStyle.SingleBorderWindow));
            stack.Children.Add(CreateRadioButton("3D-border window", WindowStyle.ThreeDBorderWindow));
            stack.Children.Add(CreateRadioButton("Tool window", WindowStyle.ToolWindow));

            AddHandler(RadioButton.CheckedEvent, new RoutedEventHandler(RadioOnChecked));
        }

        private RadioButton CreateRadioButton(string text, WindowStyle windowStyle)
        {
            var radio = new RadioButton();
            radio.Content = text;
            radio.Tag = windowStyle;
            radio.Margin = new Thickness(5);
            radio.IsChecked = windowStyle == WindowStyle;

            return radio;
        }

        private void RadioOnChecked(object sender, RoutedEventArgs e)
        {
            var radio = e.Source as RadioButton;

            WindowStyle = (WindowStyle)radio.Tag;
        }
    }
}
