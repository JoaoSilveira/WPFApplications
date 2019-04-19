using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CommandTheMenu
{
    class CommandTheMenu : Window
    {
        TextBlock text;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CommandTheMenu());
        }

        public CommandTheMenu()
        {
            Title = "Command the Menu";

            var dock = new DockPanel();
            Content = dock;

            var menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            text = new TextBlock();
            text.Text = "Sample clipboard text";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.FontSize = 32;
            text.TextWrapping = TextWrapping.Wrap;
            dock.Children.Add(text);

            var itemEdit = new MenuItem();
            itemEdit.Header = "_Edit";
            menu.Items.Add(itemEdit);

            var itemCut = new MenuItem();
            itemCut.Header = "Cu_t";
            itemCut.Command = ApplicationCommands.Cut;
            itemEdit.Items.Add(itemCut);

            var itemCopy = new MenuItem();
            itemCopy.Header = "_Copy";
            itemCopy.Command = ApplicationCommands.Copy;
            itemEdit.Items.Add(itemCopy);

            var itemPaste = new MenuItem();
            itemPaste.Header = "_Paste";
            itemPaste.Command = ApplicationCommands.Paste;
            itemEdit.Items.Add(itemPaste);

            var itemDelete = new MenuItem();
            itemDelete.Header = "_Delete";
            itemDelete.Command = ApplicationCommands.Delete;
            itemEdit.Items.Add(itemDelete);

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, CutOnExecute, CutCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, CopyOnExecute, CutCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, PasteOnExecute, PasteCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, DeleteOnExecute, CutCanExecute));

            // 2nd part
            var collGestures = new InputGestureCollection();
            collGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));

            var commRestore = new RoutedUICommand("_Restore", "Restore", GetType(), collGestures);

            var itemRestore = new MenuItem();
            itemRestore.Header = "_Restore";
            itemRestore.Command = commRestore;
            itemEdit.Items.Add(itemRestore);

            CommandBindings.Add(new CommandBinding(commRestore, RestoreOnExecute));
        }

        private void CutCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(text.Text);
        }

        private void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();
        }

        private void CutOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            ApplicationCommands.Copy.Execute(null, this);
            ApplicationCommands.Delete.Execute(null, this);
        }

        private void CopyOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Clipboard.SetText(text.Text);
        }

        private void PasteOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            text.Text = Clipboard.GetText();
        }

        private void DeleteOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            text.Text = null;
        }

        // 2nd part
        private void RestoreOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            text.Text = "Sample clipboard text";
        }
    }
}
