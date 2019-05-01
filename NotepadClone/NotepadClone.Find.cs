using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NotepadClone
{
    public partial class NotepadClone
    {
        string strFindWhat = string.Empty;
        string strReplaceWith = string.Empty;
        StringComparison strcomp = StringComparison.OrdinalIgnoreCase;
        Direction dirFind = Direction.Down;

        void AddFindMenuItems(MenuItem itemEdit)
        {
            var itemFind = new MenuItem();
            itemFind.Header = "_Find...";
            itemFind.Command = ApplicationCommands.Find;
            itemEdit.Items.Add(itemFind);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Find, FindOnExecute, FindCanExecute));

            var coll = new InputGestureCollection();
            coll.Add(new KeyGesture(Key.F3));
            var commFindNext = new RoutedUICommand("Find _Next", "FindNext", GetType(), coll);

            var itemNext = new MenuItem();
            itemNext.Command = commFindNext;
            itemEdit.Items.Add(itemNext);
            CommandBindings.Add(new CommandBinding(commFindNext, FindNextOnExecute, FindNextCanExecute));

            var itemReplace = new MenuItem();
            itemReplace.Header = "_Replace...";
            itemReplace.Command = ApplicationCommands.Replace;
            itemEdit.Items.Add(itemReplace);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Replace, ReplaceOnExecute, FindCanExecute));
        }

        private void FindCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = txtbox.Text.Length > 0 && OwnedWindows.Count == 0;

        private void FindNextCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = txtbox.Text.Length > 0 && strFindWhat.Length > 0;

        private void FindOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new FindDialog(this);

            dlg.FindWhat = strFindWhat;
            dlg.MatchCase = strcomp == StringComparison.Ordinal;
            dlg.Direction = dirFind;

            dlg.FindNext += FindDialogOnFindNext;
            dlg.Show();
        }

        private void FindNextOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(strFindWhat))
                FindOnExecute(sender, e);
            else
                FindNext();
        }

        private void ReplaceOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new ReplaceDialog(this);

            dlg.FindWhat = strFindWhat;
            dlg.ReplaceWith = strReplaceWith;
            dlg.MatchCase = strcomp == StringComparison.Ordinal;
            dlg.Direction = dirFind;

            dlg.FindNext += FindDialogOnFindNext;
            dlg.Replace += ReplaceDialogOnReplace;
            dlg.ReplaceAll += ReplaceDialogOnReplaceAll;

            dlg.Show();
        }

        private void FindDialogOnFindNext(object sender, EventArgs e)
        {
            var dlg = sender as FindReplaceDialog;

            strFindWhat = dlg.FindWhat;
            strcomp = dlg.MatchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            dirFind = dlg.Direction;

            FindNext();
        }

        private void ReplaceDialogOnReplace(object sender, EventArgs e)
        {
            var dlg = sender as FindReplaceDialog;

            strFindWhat = dlg.FindWhat;
            strReplaceWith = dlg.ReplaceWith;
            strcomp = dlg.MatchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            dirFind = dlg.Direction;

            if (strFindWhat.Equals(txtbox.SelectedText, strcomp))
                txtbox.SelectedText = strReplaceWith;

            FindNext();
        }

        private void ReplaceDialogOnReplaceAll(object sender, EventArgs e)
        {
            var dlg = sender as FindReplaceDialog;

            var str = txtbox.Text;
            strFindWhat = dlg.FindWhat;
            strReplaceWith = dlg.ReplaceWith;
            strcomp = dlg.MatchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            dirFind = dlg.Direction;

            var index = 0;
            while (index + strFindWhat.Length < str.Length)
            {
                index = str.IndexOf(strFindWhat, index, strcomp);

                if (index == -1)
                    break;

                str = str.Remove(index, strFindWhat.Length);
                str = str.Insert(index, strReplaceWith);
                index += strReplaceWith.Length;
            }

            txtbox.Text = str;
        }

        private void FindNext()
        {
            int indexStart;
            int indexFind;

            if (dirFind == Direction.Down)
            {
                indexStart = txtbox.SelectionStart + txtbox.SelectionLength;
                indexFind = txtbox.Text.IndexOf(strFindWhat, indexStart, strcomp);
            }
            else
            {
                indexStart = txtbox.SelectionStart;
                indexFind = txtbox.Text.LastIndexOf(strFindWhat, indexStart, strcomp);
            }

            if (indexFind == -1)
                MessageBox.Show($"Cannot find \"{strFindWhat}\"", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                txtbox.Select(indexFind, strFindWhat.Length);
                txtbox.Focus();
            }
        }
    }
}
