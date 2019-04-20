using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FormatRichText
{
    public partial class FormatRichText : Window
    {
        string[] formats =
        {
            DataFormats.Xaml,
            DataFormats.XamlPackage,
            DataFormats.Rtf,
            DataFormats.Text,
            DataFormats.Text
        };

        string filter = "XAML Document Files (*.xaml)|*.xaml|XAML Package Files (*.zip)|*.zip|Rich Text Format Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

        void AddFileToolBar(ToolBarTray tray, int band, int index)
        {
            var toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            RoutedUICommand[] comm =
            {
                ApplicationCommands.New,
                ApplicationCommands.Open,
                ApplicationCommands.Save
            };

            string[] images =
            {
                "New.png",
                "Open.png",
                "Save.png"
            };

            for (var i = 0; i < 3; i++)
            {
                var btn = new Button();
                btn.Command = comm[i];
                toolbar.Items.Add(btn);

                var img = new Image();
                img.Source = new BitmapImage(new Uri($"pack://application:,,,/Images/{images[i]}"));
                img.Stretch = Stretch.None;
                btn.Content = img;

                var tip = new ToolTip();
                tip.Content = comm[i].Text;
                btn.ToolTip = tip;
            }

            CommandBindings.Add(new CommandBinding(comm[0], OnNew));
            CommandBindings.Add(new CommandBinding(comm[1], OnOpen));
            CommandBindings.Add(new CommandBinding(comm[2], OnSave));
        }

        private void OnNew(object sender, ExecutedRoutedEventArgs e)
        {
            var flow = txtbox.Document;
            var range = new TextRange(flow.ContentStart, flow.ContentEnd);
            range.Text = "";
        }

        private void OnOpen(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = filter;

            if ((bool)dlg.ShowDialog(this))
            {
                var flow = txtbox.Document;
                var range = new TextRange(flow.ContentStart, flow.ContentEnd);

                try
                {
                    using (var strm = new FileStream(dlg.FileName, FileMode.Open))
                    {
                        range.Load(strm, formats[dlg.FilterIndex - 1]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Title);
                }
            }
        }

        private void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = filter;

            if ((bool)dlg.ShowDialog(this))
            {
                var flow = txtbox.Document;
                var range = new TextRange(flow.ContentStart, flow.ContentEnd);

                try
                {
                    using (var strm = new FileStream(dlg.FileName, FileMode.Create))
                    {
                        range.Save(strm, formats[dlg.FilterIndex - 1]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Title);
                }
            }
        }
    }
}
