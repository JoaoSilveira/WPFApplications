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

namespace MeetTheDockers
{
    class MeetTheDockers : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new MeetTheDockers());
        }

        public MeetTheDockers()
        {
            Title = "Meet the Dockers";

            var dock = new DockPanel();
            Content = dock;

            var menu = new Menu();
            var item = new MenuItem();
            item.Header = "Menu";
            menu.Items.Add(item);

            DockPanel.SetDock(menu, Dock.Top);
            dock.Children.Add(menu);

            var tool = new ToolBar();
            tool.Header = "Toolbar";

            DockPanel.SetDock(tool, Dock.Top);
            dock.Children.Add(tool);

            var status = new StatusBar();
            var statitem = new StatusBarItem();
            statitem.Content = "Status";
            status.Items.Add(statitem);

            DockPanel.SetDock(status, Dock.Bottom);
            dock.Children.Add(status);

            var lstbox = new ListBox();
            lstbox.Items.Add("List Box Item");

            DockPanel.SetDock(lstbox, Dock.Left);
            dock.Children.Add(lstbox);

            var txtbox = new TextBox();
            txtbox.AcceptsReturn = true;

            dock.Children.Add(txtbox);
            txtbox.Focus();
        }
    }
}
