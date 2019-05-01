using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NotepadClone
{
    public class FindReplaceDialog : Window
    {
        public event EventHandler FindNext;
        public event EventHandler Replace;
        public event EventHandler ReplaceAll;

        protected Label lblReplace;
        protected TextBox txtboxFind;
        protected TextBox txtboxReplace;
        protected CheckBox checkMatch;
        protected GroupBox groupDirection;
        protected RadioButton radioDown;
        protected RadioButton radioUp;
        protected Button btnFind;
        protected Button btnReplace;
        protected Button btnAll;

        public string FindWhat
        {
            get { return txtboxFind.Text; }
            set { txtboxFind.Text = value; }
        }

        public string ReplaceWith
        {
            get { return txtboxReplace.Text; }
            set { txtboxReplace.Text = value; }
        }

        public bool MatchCase
        {
            get { return checkMatch.IsChecked.GetValueOrDefault(); }
            set { checkMatch.IsChecked = value; }
        }

        public Direction Direction
        {
            get { return radioDown.IsChecked.GetValueOrDefault() ? Direction.Down : Direction.Up; }
            set
            {
                if (value == Direction.Up)
                    radioUp.IsChecked = true;
                else
                    radioDown.IsChecked = true;
            }
        }

        protected FindReplaceDialog(Window owner)
        {
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = owner;

            var grid = new Grid();
            Content = grid;

            for (var i = 0; i < 3; i++)
            {
                var rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);

                var coldef = new ColumnDefinition();
                coldef.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(coldef);
            }

            var lbl = new Label();
            lbl.Content = "Fi_nd what:";
            lbl.VerticalAlignment = VerticalAlignment.Center;
            lbl.Margin = new Thickness(12);
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);

            txtboxFind = new TextBox();
            txtboxFind.Margin = new Thickness(12);
            txtboxFind.TextChanged += FindTextBoxOnTextChanged;
            grid.Children.Add(txtboxFind);
            Grid.SetRow(txtboxFind, 0);
            Grid.SetColumn(txtboxFind, 1);

            lblReplace = new Label();
            lblReplace.Content = "Re_place with:";
            lblReplace.VerticalAlignment = VerticalAlignment.Center;
            lblReplace.Margin = new Thickness(12);
            grid.Children.Add(lblReplace);
            Grid.SetRow(lblReplace, 1);
            Grid.SetColumn(lblReplace, 0);

            txtboxReplace = new TextBox();
            txtboxReplace.Margin = new Thickness(12);
            grid.Children.Add(txtboxReplace);
            Grid.SetRow(txtboxReplace, 1);
            Grid.SetColumn(txtboxReplace, 1);

            checkMatch = new CheckBox();
            checkMatch.Content = "Match _case";
            checkMatch.VerticalAlignment = VerticalAlignment.Center;
            checkMatch.Margin = new Thickness(12);
            grid.Children.Add(checkMatch);
            Grid.SetRow(checkMatch, 2);
            Grid.SetColumn(checkMatch, 0);

            groupDirection = new GroupBox();
            groupDirection.Header = "Direction";
            groupDirection.Margin = new Thickness(12);
            groupDirection.HorizontalAlignment = HorizontalAlignment.Left;
            grid.Children.Add(groupDirection);
            Grid.SetRow(groupDirection, 2);
            Grid.SetColumn(groupDirection, 1);

            var stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            groupDirection.Content = stack;

            radioUp = new RadioButton();
            radioUp.Content = "_Up";
            radioUp.Margin = new Thickness(6);
            stack.Children.Add(radioUp);

            radioDown = new RadioButton();
            radioDown.Content = "_Down";
            radioDown.Margin = new Thickness(6);
            stack.Children.Add(radioDown);

            stack = new StackPanel();
            stack.Margin = new Thickness(6);
            grid.Children.Add(stack);
            Grid.SetRow(stack, 0);
            Grid.SetColumn(stack, 2);
            Grid.SetRowSpan(stack, 3);

            btnFind = new Button();
            btnFind.Content = "_Find Next";
            btnFind.Margin = new Thickness(6);
            btnFind.IsDefault = true;
            btnFind.Click += FindNextOnClick;
            stack.Children.Add(btnFind);

            btnReplace = new Button();
            btnReplace.Content = "_Replace";
            btnReplace.Margin = new Thickness(6);
            btnReplace.Click += ReplaceOnClick;
            stack.Children.Add(btnReplace);

            btnAll = new Button();
            btnAll.Content = "Replace _All";
            btnAll.Margin = new Thickness(6);
            btnAll.Click += ReplaceAllOnClick;
            stack.Children.Add(btnAll);

            var btn = new Button();
            btn.Content = "Cancel";
            btn.Margin = new Thickness(6);
            btn.IsCancel = true;
            btn.Click += CancelOnClick;
            stack.Children.Add(btn);

            txtboxFind.Focus();

        }

        private void FindTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var txtbox = e.Source as TextBox;
            btnFind.IsEnabled = btnReplace.IsEnabled = btnAll.IsEnabled = txtbox.Text.Length > 0;
        }

        private void FindNextOnClick(object sender, RoutedEventArgs e) => OnFindNext(new EventArgs());

        protected virtual void OnFindNext(EventArgs e) => FindNext?.Invoke(this, e);

        private void ReplaceOnClick(object sender, RoutedEventArgs e) => OnReplace(new EventArgs());

        protected virtual void OnReplace(EventArgs e) => Replace?.Invoke(this, e);

        private void ReplaceAllOnClick(object sender, RoutedEventArgs e) => OnReplaceAll(new EventArgs());

        private void OnReplaceAll(EventArgs e) => ReplaceAll?.Invoke(this, e);

        private void CancelOnClick(object sender, RoutedEventArgs e) => Close();
    }
}
