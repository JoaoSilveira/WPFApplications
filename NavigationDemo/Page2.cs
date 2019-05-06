using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace NavigationDemo
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        void ButtonOnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));

        void HyperlinkOnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            NavigationService.Navigate(e.Uri);
            e.Handled = true;
        }
    }
}
