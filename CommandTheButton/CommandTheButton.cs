using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommandTheButton
{
    class CommandTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CommandTheButton());
        }

        public CommandTheButton()
        {
            Title = "Command the Button";

            var button = new Button();
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Command = ApplicationCommands.Paste;
            button.Content = ApplicationCommands.Paste.Text;
            Content = button;

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, PasteOnExecute, PasteCanExecute));
        }

        private void PasteOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Title = Clipboard.GetText();
        }

        private void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Title = "Command the Button";
        }
    }
}
