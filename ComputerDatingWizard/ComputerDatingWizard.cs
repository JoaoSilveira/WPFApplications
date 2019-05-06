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

namespace ComputerDatingWizard
{
    public partial class ComputerDatingWizard : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ComputerDatingWizard());
        }

        public ComputerDatingWizard()
        {
            InitializeComponent();

            frame.Navigate(new WizardPage0());
        }
    }
}
