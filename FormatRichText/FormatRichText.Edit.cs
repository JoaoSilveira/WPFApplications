using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FormatRichText
{
    public partial class FormatRichText : Window
    {
        void AddEditToolBar(ToolBarTray tray, int band, int index)
        {
            var toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            RoutedUICommand[] comm =
            {
                ApplicationCommands.Cut,
                ApplicationCommands.Copy,
                ApplicationCommands.Paste,
                ApplicationCommands.Delete,
                ApplicationCommands.Undo,
                ApplicationCommands.Redo
            };

            string[] images =
            {
                "Cut.png",
                "Copy.png",
                "Paste.png",
                "Delete.png",
                "Undo.png",
                "Redo.png"
            };

            for (var i = 0; i < 6; i++)
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

            CommandBindings.Add(new CommandBinding(comm[0]));
            CommandBindings.Add(new CommandBinding(comm[1]));
            CommandBindings.Add(new CommandBinding(comm[2]));
            CommandBindings.Add(new CommandBinding(comm[3], OnDelete, OnCanDelete));
            CommandBindings.Add(new CommandBinding(comm[4]));
            CommandBindings.Add(new CommandBinding(comm[5]));
        }

        private void OnCanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !txtbox.Selection.IsEmpty;
        }

        private void OnDelete(object sender, ExecutedRoutedEventArgs e)
        {
            txtbox.Selection.Text = string.Empty;
        }
    }
}
