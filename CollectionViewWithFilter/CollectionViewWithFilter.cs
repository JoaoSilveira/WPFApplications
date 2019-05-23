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

namespace CollectionViewWithFilter
{
    public partial class CollectionViewWithFilter : Window
    {
        ListCollectionView collview;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CollectionViewWithFilter());
        }

        public CollectionViewWithFilter()
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
            lstbox.ItemsSource = collview;

            if (lstbox.Items.Count > 0)
                lstbox.SelectedIndex = 0;

            radioAll.IsChecked = true;
        }

        void RadioOnChecked(object sender, RoutedEventArgs e)
        {
            if (collview == null)
                return;

            var radio = e.Source as RadioButton;

            switch (radio.Name)
            {
                case "radioLiving":
                    collview.Filter = PersonIsLiving;
                    break;
                case "radioDead":
                    collview.Filter = PersonIsDead;
                    break;
                case "radioAll":
                    collview.Filter = null;
                    break;
            }
        }

        private bool PersonIsLiving(object obj) => (obj as Person).DeathDate == null;

        private bool PersonIsDead(object obj) => (obj as Person).DeathDate != null;
    }
}
