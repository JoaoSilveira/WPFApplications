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

namespace ExamineKeystrokes
{
    class ExamineKeystrokes : Window
    {
        StackPanel stack;
        ScrollViewer scroll;
        static readonly string header = $"{"Event",-10}{"Key",-20}{"Sys-Key",-10}{"Text",-10}{"Ctrl-Text",-10}{"Sys-Text",-10}{"Ime",-10}{"KeyStates",-15}{"IsDown",-8}{"IsUp",-7}{"IsToggled",-10}{"IsRepeat",-10}";
        static readonly string formatKey = $"{{0,-10}}{{1,-20}}{{2,-10}}{new string(' ', 30)}{{3,-10}}{{4,-15}}{{5,-8}}{{6,-7}}{{7,-10}}{{8,-10}}";
        static readonly string formatText = $"{{0,-10}}{new string(' ', 30)}{{1,-10}}{{2,-10}}{{3,-10}}";


        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ExamineKeystrokes());
        }

        public ExamineKeystrokes()
        {
            Title = "Examine Keystrokes";
            FontFamily = new FontFamily("Courier New");

            var grid = new Grid();
            Content = grid;

            var rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);
            grid.RowDefinitions.Add(new RowDefinition());

            var textHeader = new TextBlock();
            textHeader.FontWeight = FontWeights.Bold;
            textHeader.Text = header;
            grid.Children.Add(textHeader);

            scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 1);

            stack = new StackPanel();
            scroll.Content = stack;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            DisplayKeyInfo(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            DisplayKeyInfo(e);
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            DisplayInfo(string.Format(formatText, e.RoutedEvent.Name, e.Text, e.ControlText, e.SystemText));
        }

        private void DisplayKeyInfo(KeyEventArgs e) => DisplayInfo(string.Format(formatKey, e.RoutedEvent.Name, e.Key, e.SystemKey, e.ImeProcessedKey, e.KeyStates, e.IsDown, e.IsUp, e.IsToggled, e.IsRepeat));

        private void DisplayInfo(string info)
        {
            var text = new TextBlock();
            text.Text = info;
            stack.Children.Add(text);
            scroll.ScrollToBottom();
        }
    }
}
