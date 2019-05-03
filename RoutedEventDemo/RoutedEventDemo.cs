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

namespace RoutedEventDemo
{
    public partial class RoutedEventDemo : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new RoutedEventDemo());
        }

        public RoutedEventDemo()
        {
            InitializeComponent();
        }

        void MenuItemOnClick(object sender, RoutedEventArgs e)
        {
            var str = (e.Source as MenuItem).Header as string;
            var clr = (Color)ColorConverter.ConvertFromString(str);
            txtblk.Foreground = new SolidColorBrush(clr);
        }
    }
}
