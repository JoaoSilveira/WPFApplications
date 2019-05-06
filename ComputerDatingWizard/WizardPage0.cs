using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComputerDatingWizard
{
    public partial class WizardPage0 : Page
    {
        public WizardPage0()
        {
            InitializeComponent();
        }

        void BeginButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoForward)
                NavigationService.GoForward();
            else
            {
                var vitals = new Vitals();
                var page1 = new WizardPage1(vitals);
                NavigationService.Navigate(page1);
            }
        }
    }
}
