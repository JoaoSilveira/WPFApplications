using SelectColorFromGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UseCustomClass
{
    public partial class UseCustomClass : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new UseCustomClass());
        }

        public UseCustomClass()
        {
            InitializeComponent();
        }

        void ColorGridBoxOnSelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            Background = (Brush)(e.Source as ColorGridBox).SelectedValue;
        }
    }
}
