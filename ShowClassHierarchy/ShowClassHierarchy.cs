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

namespace ShowClassHierarchy
{
    public class ShowClassHierarchy : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ShowClassHierarchy());
        }

        public ShowClassHierarchy()
        {
            Title = "Show Class Hierarchy";

            Content = new ClassHierarchyTreeView(typeof(System.Windows.Threading.DispatcherObject));
        }
    }
}
