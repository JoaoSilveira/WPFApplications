using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace FormatTheButton
{
    class FormatTheButton : Window
    {
        Run runButton;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new FormatTheButton());
        }

        public FormatTheButton()
        {
            Title = "Format the Button";

            var btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.MouseEnter += ButtonOnMouseEnter;
            btn.MouseLeave += ButtonOnMouseLeave;

            Content = btn;

            var txt = new TextBlock();
            txt.FontSize = 48;
            txt.TextAlignment = TextAlignment.Center;
            btn.Content = txt;

            txt.Inlines.Add(new Italic(new Run("Click")));
            txt.Inlines.Add(" the ");
            txt.Inlines.Add(runButton = new Run("Button"));
            txt.Inlines.Add(new LineBreak());
            txt.Inlines.Add("to launch the ");
            txt.Inlines.Add(new Bold(new Run("rocket")));
        }

        private void ButtonOnMouseLeave(object sender, MouseEventArgs e)
        {
            runButton.Foreground = SystemColors.ControlTextBrush;
        }

        private void ButtonOnMouseEnter(object sender, MouseEventArgs e)
        {
            runButton.Foreground = Brushes.Red;
        }
    }
}
