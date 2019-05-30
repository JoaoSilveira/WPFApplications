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

namespace XamlClock
{
    public partial class XamlClock : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new XamlClock());
        }

        public XamlClock()
        {
            InitializeComponent();

            storyboard.BeginTime = -DateTime.Now.TimeOfDay;
        }
    }
}
