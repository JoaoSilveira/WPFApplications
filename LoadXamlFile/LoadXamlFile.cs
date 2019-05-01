using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace LoadXamlFile
{
    class LoadXamlFile : Window
    {
        Frame frame;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new LoadXamlFile());
        }

        public LoadXamlFile()
        {
            Title = "Load Xaml File";

            var dock = new DockPanel();
            Content = dock;

            var btn = new Button();
            btn.Content = "Open File...";
            btn.Margin = new Thickness(12);
            btn.HorizontalAlignment = HorizontalAlignment.Left;
            btn.Click += ButtonOnClick;
            dock.Children.Add(btn);
            DockPanel.SetDock(btn, Dock.Top);

            frame = new Frame();
            dock.Children.Add(frame);
        }

        void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "XAML Files (*.xaml)|*.xaml|All Files (*.*)/|*.*";

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            try
            {
                var xmlreader = new XmlTextReader(dlg.FileName);

                var obj = XamlReader.Load(xmlreader);
                if (obj is Window win)
                {
                    win.Owner = this;
                    win.Show();
                }
                else
                {
                    frame.Content = obj;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title);
            }
        }
    }
}
