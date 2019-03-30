using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DockAroundTheBlock
{
    class DockAroundTheBlock : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DockAroundTheBlock());
        }

        public DockAroundTheBlock()
        {
            Title = "Dock Around the Block";

            var dock = new DockPanel();
            Content = dock;

            for (var i = 0; i < 17; i++)
            {
                var btn = new Button();
                btn.Content = $"Button No. {i + 1}";
                dock.Children.Add(btn);
                btn.SetValue(DockPanel.DockProperty, (Dock)(i % 4));
            }
        }
    }
}
