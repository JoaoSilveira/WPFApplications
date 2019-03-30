using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace EditSomeRichText
{
    class EditSomeRichText : Window
    {
        RichTextBox textbox;

        string filter = "Document Files(*.xaml)|*.xaml|All Files (*.*)|*.*";

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new EditSomeRichText());
        }

        public EditSomeRichText()
        {
            Title = "Edit Some Rich Text";

            textbox = new RichTextBox();
            textbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Content = textbox;

            textbox.Focus();
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (e.ControlText.Length > 0)
            {
                if (e.ControlText[0] == '\x0F')
                {
                    var dlg = new OpenFileDialog();
                    dlg.CheckFileExists = true;
                    dlg.Filter = filter;

                    if (dlg.ShowDialog().Value)
                    {
                        var flow = textbox.Document;
                        var range = new TextRange(flow.ContentStart, flow.ContentEnd);
                        Stream strm = null;

                        try
                        {
                            strm = new FileStream(dlg.FileName, FileMode.Open);
                            range.Load(strm, DataFormats.Xaml);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Title);
                        }
                        finally
                        {
                            strm?.Close();
                        }

                    }

                    e.Handled = true;
                }

                if (e.ControlText[0] == '\x13')
                {
                    var dlg = new SaveFileDialog();
                    dlg.Filter = filter;

                    if (dlg.ShowDialog().Value)
                    {
                        var flow = textbox.Document;
                        var range = new TextRange(flow.ContentStart, flow.ContentEnd);
                        Stream strm = null;

                        try
                        {
                            strm = new FileStream(dlg.FileName, FileMode.Create);
                            range.Save(strm, DataFormats.Xaml);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Title);
                        }
                        finally
                        {
                            strm?.Close();
                        }
                    }

                    e.Handled = true;
                }
            }

            base.OnPreviewTextInput(e);
        }
    }
}
