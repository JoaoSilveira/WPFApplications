using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EditSomeText
{
    class EditSomeText : Window
    {
        static string fileName = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Joao Corporation\\EditSomeText\\EditSomeText.txt");

        TextBox textbox;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new EditSomeText());
        }

        public EditSomeText()
        {
            Title = "Edit Some Text";

            textbox = new TextBox();
            textbox.AcceptsReturn = true;
            textbox.TextWrapping = TextWrapping.Wrap;
            textbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textbox.KeyDown += TextBoxOnKeyDown;
            Content = textbox;

            try
            {
                textbox.Text = File.ReadAllText(fileName);
            }
            catch { }

            textbox.CaretIndex = textbox.Text.Length;
            textbox.Focus();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                File.WriteAllText(fileName, textbox.Text);
            }
            catch (Exception ex)
            {
                e.Cancel = MessageBoxResult.No == MessageBox.Show(
                    $"File could not be saved: {ex.Message}\nClose program anyway?",
                    Title,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Exclamation);
            }
        }

        private void TextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                textbox.SelectedText = DateTime.Now.ToString();
                textbox.CaretIndex = textbox.SelectionStart + textbox.SelectionLength;
            }
        }
    }
}
