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
using System.Windows.Media.Imaging;

namespace CraftTheToolbar
{
    class CraftTheToolbar : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CraftTheToolbar());
        }

        public CraftTheToolbar()
        {
            Title = "Craft the Toolbar";

            var comm = new[]
            {
                ApplicationCommands.New,
                ApplicationCommands.Open,
                ApplicationCommands.Save,
                ApplicationCommands.Print,
                ApplicationCommands.Cut,
                ApplicationCommands.Copy,
                ApplicationCommands.Paste,
                ApplicationCommands.Delete
            };

            var images = new[]
            {
                "New.png",
                "Open.png",
                "Save.png",
                "Print.png",
                "Cut.png",
                "Copy.png",
                "Paste.png",
                "Delete.png"
            };

            var dock = new DockPanel();
            dock.LastChildFill = false;
            Content = dock;

            var toolbar = new ToolBar();
            dock.Children.Add(toolbar);
            DockPanel.SetDock(toolbar, Dock.Top);

            for (var i = 0; i < images.Length; i++)
            {
                if (i == 4)
                    toolbar.Items.Add(new Separator());

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

                CommandBindings.Add(new CommandBinding(comm[i], ToolBarButtonOnClick));
            }
        }

        private void ToolBarButtonOnClick(object sender, ExecutedRoutedEventArgs e)
        {
            var comm = e.Command as RoutedUICommand;

            MessageBox.Show($"{comm.Name} command not yet implemented", Title);
        }
    }
}
