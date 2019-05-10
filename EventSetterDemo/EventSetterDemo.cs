using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EventSetterDemo
{
    public partial class EventSetterDemo : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new EventSetterDemo());
        }

        public EventSetterDemo()
        {
            InitializeComponent();
        }

        void ButtonOnClick(object sender, RoutedEventArgs e) => MessageBox.Show($"The button labeled {(e.Source as Button).Content} has been clicked", Title);
    }
}
