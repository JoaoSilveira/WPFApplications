using SingleRecordDataEntry;
using System;
using System.Windows;
using System.Windows.Input;

namespace MultiRecordDataEntry
{
    public partial class MultiRecordDataEntryWindow : Window
    {
        People people;
        int index;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new MultiRecordDataEntryWindow());
        }

        public MultiRecordDataEntryWindow()
        {
            InitializeComponent();

            ApplicationCommands.New.Execute(null, this);

            pnlPerson.Children[1].Focus();
        }

        void NewOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            people = new People();
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
            index = 0;

            if (people.Count == 0)
                people.Insert(0, new Person());

            pnlPerson.DataContext = people[0];
            EnableAndDisableButtons();
        }

        void FirstOnClick(object sender, RoutedEventArgs e)
        {
            pnlPerson.DataContext = people[index = 0];
            EnableAndDisableButtons();
        }

        void PreviousOnClick(object sender, RoutedEventArgs e)
        {
            pnlPerson.DataContext = people[--index];
            EnableAndDisableButtons();
        }

        void NextOnClick(object sender, RoutedEventArgs e)
        {
            pnlPerson.DataContext = people[++index];
            EnableAndDisableButtons();
        }

        void LastOnClick(object sender, RoutedEventArgs e)
        {
            pnlPerson.DataContext = people[index = people.Count - 1];
            EnableAndDisableButtons();
        }

        void AddOnClick(object sender, RoutedEventArgs e)
        {
            people.Insert(index = people.Count, new Person());
            pnlPerson.DataContext = people[index];
            EnableAndDisableButtons();
        }

        void DeleteOnClick(object sender, RoutedEventArgs e)
        {
            people.RemoveAt(index);
            if (people.Count == 0)
                people.Insert(0, new Person());

            if (index > people.Count)
                index--;

            pnlPerson.DataContext = people[index];
            EnableAndDisableButtons();
        }

        void EnableAndDisableButtons()
        {
            btnPrev.IsEnabled = index > 0;
            btnNext.IsEnabled = index < people.Count - 1;
            pnlPerson.Children[1].Focus();
        }
    }
}
