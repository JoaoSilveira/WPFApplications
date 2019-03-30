using System;
using System.Windows;
using System.Windows.Input;

namespace RecordKeystrokes
{
    class RecordKeystrokes : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new RecordKeystrokes());
        }

        public RecordKeystrokes()
        {
            Title = "Record Keystrokes";
            Content = string.Empty;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            var str = Content as string;

            if (e.Text == "\b")
            {
                if (str.Length > 0)
                {
                    str = str.Substring(0, str.Length - 1);
                }
            }
            else
            {
                str += e.Text;
            }
            Content = str;
        }
    }
}
