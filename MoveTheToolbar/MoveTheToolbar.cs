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

namespace MoveTheToolbar
{
    class MoveTheToolbar : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new MoveTheToolbar());
        }

        public MoveTheToolbar()
        {
            Title = "Move the Toolbar";

            var dock = new DockPanel();
            Content = dock;

            var trayTop = new ToolBarTray();
            dock.Children.Add(trayTop);
            DockPanel.SetDock(trayTop, Dock.Top);

            var trayLeft = new ToolBarTray();
            trayLeft.Orientation = Orientation.Vertical;
            dock.Children.Add(trayLeft);
            DockPanel.SetDock(trayLeft, Dock.Left);

            var txtbox = new TextBox();
            dock.Children.Add(txtbox);

            for (var i = 0; i < 6; i++)
            {
                var toolbar = new ToolBar();
                toolbar.Header = $"Toolbar {i + 1}";

                if (i < 3)
                    trayTop.ToolBars.Add(toolbar);
                else
                    trayLeft.ToolBars.Add(toolbar);

                for (var j = 0; j < 6; j++)
                {
                    var btn = new Button();
                    btn.FontSize = 16;
                    btn.Content = (char)('A' + j);
                    toolbar.Items.Add(btn);
                }
            }
        }

    }
}
