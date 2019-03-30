using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ToggleBoldAndItalic
{
    class ToggleBoldAndItalic : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ToggleBoldAndItalic());
        }

        public ToggleBoldAndItalic()
        {
            Title = "Toggle Bold and Italic";

            var text = new TextBlock();
            text.FontSize = 32;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            Content = text;

            string quote = "To be, or not to be, that is the question";

            foreach(var word in quote.Split())
            {
                var run = new Run(word);
                run.MouseDown += RunOnMouseDown;
                text.Inlines.Add(run);
                text.Inlines.Add(" ");
            }
        }

        private void RunOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var run = sender as Run;

            if (e.ChangedButton == MouseButton.Left)
            {
                run.FontStyle = run.FontStyle == FontStyles.Italic ? FontStyles.Normal : FontStyles.Italic;
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                run.FontWeight = run.FontWeight == FontWeights.Bold ? FontWeights.Normal : FontWeights.Bold;
            }
        }
    }
}
