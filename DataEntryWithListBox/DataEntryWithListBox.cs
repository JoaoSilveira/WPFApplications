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

namespace DataEntryWithListBox
{
    public partial class DataEntryWithListBox : Window
    {
        ListCollectionView collview;
        People people;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DataEntryWithListBox());
        }

        public DataEntryWithListBox()
        {
            InitializeComponent();

            ApplicationCommands.New.Execute(null, this);

            pnlPerson.Children[1].Focus();
        }

        void NewOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            people = new People();
            people.Add(new Person());
            InitializeNewPeopleObject();
        }

        void OpenOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            people = People.Load(this);

            if (people != null)
                InitializeNewPeopleObject();
        }

        void SaveOnExecuted(object sender, ExecutedRoutedEventArgs e) => people.Save(this);

        private void InitializeNewPeopleObject()
        {
            collview = new ListCollectionView(people);

            collview.SortDescriptions.Add(new SortDescription("LastName", ListSortDirection.Ascending));

            lstbox.ItemsSource = collview;
            pnlPerson.DataContext = collview;

            if (lstbox.Items.Count > 0)
                lstbox.SelectedIndex = 0;
        }

        void AddOnClick(object sender, RoutedEventArgs e)
        {
            var person = new Person();
            people.Add(person);
            lstbox.SelectedItem = person;
            pnlPerson.Children[1].Focus();
            collview.Refresh();
        }

        void DeleteOnClick(object sender, RoutedEventArgs e)
        {
            if (lstbox.SelectedItem == null)
                return;

            people.Remove(lstbox.SelectedItem as Person);
            if (lstbox.Items.Count > 0)
                lstbox.SelectedIndex = 0;
            else
                AddOnClick(sender, e);
        }
    }
}
