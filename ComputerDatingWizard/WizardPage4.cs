using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComputerDatingWizard
{
    public partial class WizardPage4 : Page
    {
        public WizardPage4(Vitals vitals)
        {
            InitializeComponent();

            runName.Text = vitals.Name;
            runHome.Text = vitals.Home;
            runGender.Text = vitals.Gender;
            runOS.Text = vitals.FavoriteOS;
            runDirectory.Text = vitals.Directory;
            runMomsMaidenName.Text = vitals.MomsMaidenName;
            runPet.Text = vitals.Pet;
            runIncome.Text = vitals.Income;
        }

        void PreviousButtonOnClick(object sender, RoutedEventArgs e) => NavigationService.GoBack();

        void SubmitButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Thank you!\n\nYou will be contacted by email in four to six months.", Application.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Information);

            Application.Current.Shutdown();
        }
    }
}
