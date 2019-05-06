using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComputerDatingWizard
{
    public partial class WizardPage1 : Page
    {
        Vitals vitals;

        public WizardPage1(Vitals vitals)
        {
            InitializeComponent();
            this.vitals = vitals;
        }

        void PreviousButtonOnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        void NextButtonOnClick(object sender, RoutedEventArgs e)
        {
            vitals.Name = txtboxName.Name;
            vitals.Home = Vitals.GetCheckedRadioButton(grpboxHome).Content as string;
            vitals.Gender = Vitals.GetCheckedRadioButton(grpboxGender).Content as string;

            if (NavigationService.CanGoForward)
                NavigationService.GoForward();
            else
                NavigationService.Navigate(new WizardPage2(vitals));
        }
    }
}
