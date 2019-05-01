using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        protected string strFilter = "Text Documents (*.txt)|*.txt|All Files (*.*)|*.*";

        void AddFileMenu()
        {
            var itemFile = new MenuItem();
            itemFile.Header = "_File";
            menu.Items.Add(itemFile);

            var itemNew = new MenuItem();
            itemNew.Header = "_New";
            itemNew.Command = ApplicationCommands.New;
            itemFile.Items.Add(itemNew);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, NewOnExecute));

            var itemOpen = new MenuItem();
            itemOpen.Header = "_Open...";
            itemOpen.Command = ApplicationCommands.Open;
            itemFile.Items.Add(itemOpen);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenOnExecute));

            var itemSave = new MenuItem();
            itemSave.Header = "_Save";
            itemSave.Command = ApplicationCommands.Save;
            itemFile.Items.Add(itemSave);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveOnExecute));

            var itemSaveAs = new MenuItem();
            itemSaveAs.Header = "Save _As...";
            itemSaveAs.Command = ApplicationCommands.SaveAs;
            itemFile.Items.Add(itemSaveAs);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, SaveAsOnExecute));

            itemFile.Items.Add(new Separator());
            AddPrintMenuItems(itemFile);
            itemFile.Items.Add(new Separator());

            var itemExit = new MenuItem();
            itemExit.Header = "E_xit";
            itemExit.Click += ExitOnClick;
            itemFile.Items.Add(itemExit);
        }

        protected virtual void NewOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!OkToTrash())
                return;

            txtbox.Clear();
            strLoadedFile = string.Empty;
            isFileDirty = false;
            UpdateTitle();
        }

        private void OpenOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!OkToTrash())
                return;

            var dlg = new OpenFileDialog();
            dlg.Filter = strFilter;

            if (dlg.ShowDialog().GetValueOrDefault())
                LoadFile(dlg.FileName);
        }

        private void SaveOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(strLoadedFile))
                DisplaySaveDialog("");
            else
                SaveFile(strLoadedFile);
        }

        private void SaveAsOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            DisplaySaveDialog(strLoadedFile);
        }

        bool DisplaySaveDialog(string strFileName)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = strFilter;
            dlg.FileName = strFileName;

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                SaveFile(dlg.FileName);
                return true;
            }
            return false;
        }

        private void ExitOnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        bool OkToTrash()
        {
            if (!isFileDirty)
                return true;

            var result = MessageBox.Show($"The text in the file {strLoadedFile ?? "Untitled"} has changed{Environment.NewLine}{Environment.NewLine}Do you want to save the changes?", strAppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrEmpty(strLoadedFile))
                    return SaveFile(strLoadedFile);
                return DisplaySaveDialog(string.Empty);
            }

            return result == MessageBoxResult.No;
        }

        void LoadFile(string strFileName)
        {
            try
            {
                txtbox.Text = File.ReadAllText(strFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error on File Open: {ex.Message}", strAppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            strLoadedFile = strFileName;
            UpdateTitle();
            txtbox.SelectionStart = 0;
            txtbox.SelectionLength = 0;
            isFileDirty = false;
        }

        bool SaveFile(string strFileName)
        {
            try
            {
                File.WriteAllText(strFileName, txtbox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error on File Save: {ex.Message}", strAppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }

            strLoadedFile = strFileName;
            UpdateTitle();
            isFileDirty = false;
            return true;
        }
    }
}
