using RecurseDirectoriesIncrementally;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

namespace ComputerDatingWizard
{
    public partial class DirectoryPage : PageFunction<DirectoryInfo>
    {
        public DirectoryPage()
        {
            InitializeComponent();
            treevue.SelectedItemChanged += TreeViewOnSelectedItemChanged;
        }

        private void TreeViewOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) => btnOk.IsEnabled = e.NewValue != null;

        void CancelButtonOnClick(object sender, RoutedEventArgs e) => OnReturn(new ReturnEventArgs<DirectoryInfo>());

        void OkButtonOnClick(object sender, RoutedEventArgs e)
        {
            var dirinfo = (treevue.SelectedItem as DirectoryTreeViewItem).DirectoryInfo;
            OnReturn(new ReturnEventArgs<DirectoryInfo>(dirinfo));
        }
    }
}
