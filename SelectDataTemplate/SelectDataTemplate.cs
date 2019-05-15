using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SelectDataTemplate
{
    public partial class SelectDataTemplate : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SelectDataTemplate());
        }

        public SelectDataTemplate()
        {
            InitializeComponent();
        }
    }
}
