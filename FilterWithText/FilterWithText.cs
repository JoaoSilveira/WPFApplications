using MultiRecordDataEntry;
using SingleRecordDataEntry;
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

namespace FilterWithText
{
    public partial class FilterWithText : Window
    {
        ListCollectionView collview;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new FilterWithText());
        }

        public FilterWithText()
        {
            InitializeComponent();
        }

        void OpenOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var people = People.Load(this);

            if (people == null)
                return;

            collview = new ListCollectionView(people);
            collview.SortDescriptions.Add(new SortDescription("LastName", ListSortDirection.Ascending));
            collview.Filter = LastNameFilter;

            txtboxFilter.Text = string.Empty;
            lstbox.ItemsSource = collview;

            if (lstbox.Items.Count > 0)
                lstbox.SelectedIndex = 0;
        }

        private bool LastNameFilter(object obj) => (obj as Person).LastName.StartsWith(txtboxFilter.Text, StringComparison.CurrentCultureIgnoreCase);

        void TextBoxOnTextChanged(object sender, TextChangedEventArgs e) => collview?.Refresh();
    }
}
