using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace FormatTheText
{
    class FormatTheText : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new FormatTheText());
        }

        public FormatTheText()
        {
            Title = "Format the Text";

            var txt = new TextBlock();

            txt.FontSize = 32;
            txt.Foreground = Brushes.CornflowerBlue;

            txt.Inlines.Add("This is some ");
            txt.Inlines.Add(new Italic(new Run("italic")));
            txt.Inlines.Add(" text, and this is some ");
            txt.Inlines.Add(new Bold(new Run("bold")));
            txt.Inlines.Add(" text, and let's cap it off with some ");
            txt.Inlines.Add(new Bold(new Italic(new Run("bold italic"))));
            txt.Inlines.Add(" text.");

            Content = txt;
        }
    }
}
