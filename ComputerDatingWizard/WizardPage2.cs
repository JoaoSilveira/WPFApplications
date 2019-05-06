using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ComputerDatingWizard
{
    public partial class WizardPage2 : Page
    {
        Vitals vitals;

        public WizardPage2(Vitals vitals)
        {
            InitializeComponent();
            this.vitals = vitals;
        }

        void BrowseButtonOnClick(object sender, RoutedEventArgs e)
        {
            var page = new DirectoryPage();
            page.Return += DirPageOnReturn;
            NavigationService.Navigate(page);
        }

        private void DirPageOnReturn(object sender, ReturnEventArgs<DirectoryInfo> e)
        {
            if (e.Result != null)
                txtboxFavoriteDir.Text = e.Result.FullName;
        }

        void PreviousButtonOnClick(object sender, RoutedEventArgs e) => NavigationService.GoBack();

        void NextButtonOnClick(object sender, RoutedEventArgs e)
        {
            vitals.FavoriteOS = txtboxFavoriteOS.Text;
            vitals.Directory = txtboxFavoriteDir.Text;

            if (NavigationService.CanGoForward)
                NavigationService.GoForward();
            else
                NavigationService.Navigate(new WizardPage3(vitals));
        }
    }
}
