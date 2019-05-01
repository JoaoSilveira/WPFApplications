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
    public partial class NotepadClone : Window
    {
        void AddEditMenu()
        {
            var itemEdit = new MenuItem();
            itemEdit.Header = "_Edit";
            menu.Items.Add(itemEdit);

            var itemUndo = new MenuItem();
            itemUndo.Header = "_Undo";
            itemUndo.Command = ApplicationCommands.Undo;
            itemEdit.Items.Add(itemUndo);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, UndoOnExecute, UndoCanExecute));

            var itemRedo = new MenuItem();
            itemRedo.Header = "_Redo";
            itemRedo.Command = ApplicationCommands.Redo;
            itemEdit.Items.Add(itemRedo);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo, RedoOnExecute, RedoCanExecute));

            itemEdit.Items.Add(new Separator());

            var itemCut = new MenuItem();
            itemCut.Header = "Cu_t";
            itemCut.Command = ApplicationCommands.Cut;
            itemEdit.Items.Add(itemCut);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, CutOnExecute, CutCanExecute));

            var itemCopy = new MenuItem();
            itemCopy.Header = "_Copy";
            itemCopy.Command = ApplicationCommands.Copy;
            itemEdit.Items.Add(itemCopy);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, CopyOnExecute, CutCanExecute));

            var itemPaste = new MenuItem();
            itemPaste.Header = "_Paste";
            itemPaste.Command = ApplicationCommands.Paste;
            itemEdit.Items.Add(itemPaste);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, PasteOnExecute, PasteCanExecute));

            var itemDelete = new MenuItem();
            itemDelete.Header = "De_lete";
            itemDelete.Command = ApplicationCommands.Delete;
            itemEdit.Items.Add(itemDelete);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, DeleteOnExecute, CutCanExecute));

            itemEdit.Items.Add(new Separator());

            AddFindMenuItems(itemEdit);

            itemEdit.Items.Add(new Separator());

            var itemAll = new MenuItem();
            itemAll.Header = "SelectAll";
            itemAll.Command = ApplicationCommands.SelectAll;
            itemEdit.Items.Add(itemAll);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SelectAll, SelectAllOnExecute));

            var coll = new InputGestureCollection();
            coll.Add(new KeyGesture(Key.F5));
            var commDateTime = new RoutedUICommand("Time/_Date", "DateTime", GetType(), coll);

            var itemDate = new MenuItem();
            itemDate.Command = commDateTime;
            itemEdit.Items.Add(itemDate);
            CommandBindings.Add(new CommandBinding(commDateTime, TimeDateOnExecute));
        }

        private void RedoCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = txtbox.CanRedo;

        private void RedoOnExecute(object sender, ExecutedRoutedEventArgs e) => txtbox.Redo();

        private void UndoCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = txtbox.CanUndo;

        private void UndoOnExecute(object sender, ExecutedRoutedEventArgs e) => txtbox.Undo();

        private void CutCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = txtbox.SelectedText.Length > 0;

        private void CutOnExecute(object sender, ExecutedRoutedEventArgs e) => txtbox.Cut();

        private void CopyOnExecute(object sender, ExecutedRoutedEventArgs e) => txtbox.Copy();

        private void DeleteOnExecute(object sender, ExecutedRoutedEventArgs e) => txtbox.SelectedText = string.Empty;

        private void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = Clipboard.ContainsText();

        private void PasteOnExecute(object sender, ExecutedRoutedEventArgs e) => txtbox.Paste();

        private void SelectAllOnExecute(object sender, ExecutedRoutedEventArgs e) => txtbox.SelectAll();

        private void TimeDateOnExecute(object sender, ExecutedRoutedEventArgs e) => txtbox.SelectedText = DateTime.Now.ToString();
    }
}
