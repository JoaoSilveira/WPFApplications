using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace DataEntry
{
    public partial class NavigationBar : ToolBar
    {
        IList coll;
        ICollectionView collview;
        string strOriginal;

        public IList Collection
        {
            get { return coll; }
            set
            {
                coll = value;

                collview = CollectionViewSource.GetDefaultView(coll);
                collview.CurrentChanged += CollectionViewOnCurrentChanged;
                collview.CollectionChanged += CollectionViewOnCollectionChanged;

                CollectionViewOnCollectionChanged(null, null);

                txtblkTotal.Text = coll.Count.ToString();
            }
        }

        public Type ItemType { get; set; }

        public NavigationBar()
        {
            InitializeComponent();
        }

        private void CollectionViewOnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => txtblkTotal.Text = coll.Count.ToString();

        private void CollectionViewOnCurrentChanged(object sender, EventArgs e)
        {
            txtboxCurrent.Text = (1 + collview.CurrentPosition).ToString();
            btnPrevious.IsEnabled = collview.CurrentPosition > 0;
            btnNext.IsEnabled = collview.CurrentPosition < coll.Count - 1;
            btnDelete.IsEnabled = coll.Count > 1;
        }

        void FirstOnClick(object sender, RoutedEventArgs e) => collview.MoveCurrentToFirst();

        void PreviousOnClick(object sender, RoutedEventArgs e) => collview.MoveCurrentToPrevious();

        void NextOnClick(object sender, RoutedEventArgs e) => collview.MoveCurrentToNext();

        void LastOnClick(object sender, RoutedEventArgs e) => collview.MoveCurrentToLast();

        void AddOnClick(object sender, RoutedEventArgs e)
        {
            var info = ItemType.GetConstructor(Type.EmptyTypes);
            coll.Add(info.Invoke(null));
            collview.MoveCurrentToLast();
        }

        void DeleteOnClick(object sender, RoutedEventArgs e) => coll.RemoveAt(collview.CurrentPosition);

        void TextBoxOnGotFocus(object sender, KeyboardFocusChangedEventArgs e) => strOriginal = txtboxCurrent.Text;

        void TextBoxOnLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!int.TryParse(txtboxCurrent.Text, out var current))
                txtboxCurrent.Text = strOriginal;
            else if (current > 0 && current <= coll.Count)
                collview.MoveCurrentTo(current - 1);
        }

        void TextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    txtboxCurrent.Text = strOriginal;
                    goto case Key.Enter;
                case Key.Enter:
                    e.Handled = true;
                    MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
                    break;
            }
        }
    }
}
