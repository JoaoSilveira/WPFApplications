using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComputerDatingWizard
{
    public partial class WizardPage3 : Page
    {
        Vitals vitals;

        public WizardPage3(Vitals vitals)
        {
            InitializeComponent();
            this.vitals = vitals;
        }

        void PreviousButtonOnClick(object sender, RoutedEventArgs e) => NavigationService.GoBack();

        void FinishButtonOnClick(object sender, RoutedEventArgs e)
        {
            vitals.MomsMaidenName = txtboxMom.Text;
            vitals.Pet = Vitals.GetCheckedRadioButton(grpboxPet).Content as string;
            vitals.Income = Vitals.GetCheckedRadioButton(grpboxIncome).Content as string;

            NavigationService.Navigate(new WizardPage4(vitals));
        }
    }
}
