using MultiRecordDataEntry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ListBoxWithGroups
{
    public partial class ListBoxWithGroups : Window
    {
        ListCollectionView collview;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListBoxWithGroups());
        }

        public ListBoxWithGroups()
        {
            InitializeComponent();
        }

        void OpenOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var people = People.Load(this);

            if (people == null)
                return;

            collview = new ListCollectionView(people);
            collview.SortDescriptions.Add(new SortDescription("BirthDate", ListSortDirection.Ascending));
            collview.GroupDescriptions.Add(new PeriodGroupDescription());

            lstbox.ItemsSource = collview;

            if (lstbox.Items.Count > 0)
                lstbox.SelectedIndex = 0;
        }
    }
}
