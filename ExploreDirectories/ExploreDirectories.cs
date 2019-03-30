using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ExploreDirectories
{
    class ExploreDirectories : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ExploreDirectories());
        }

        public ExploreDirectories()
        {
            Title = "Explore Directories";

            var scroll = new ScrollViewer();
            Content = scroll;

            var wrap = new WrapPanel();
            scroll.Content = wrap;

            wrap.Children.Add(new FileSystemInfoButton());
        }
    }
}
