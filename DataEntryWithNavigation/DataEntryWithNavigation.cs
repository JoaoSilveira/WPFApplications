using MultiRecordDataEntry;
using SingleRecordDataEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DataEntryWithNavigation
{
    public partial class DataEntryWithNavigation : Window
    {
        People people;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DataEntryWithNavigation());
        }

        public DataEntryWithNavigation()
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
            InitializeNewPeopleObject();
        }

        void SaveOnExecuted(object sender, ExecutedRoutedEventArgs e) => people.Save(this);

        void InitializeNewPeopleObject()
        {
            navbar.Collection = people;
            navbar.ItemType = typeof(Person);
            pnlPerson.DataContext = people;
        }
    }
}
