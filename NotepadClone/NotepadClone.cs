using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace NotepadClone
{
    public partial class NotepadClone : Window
    {
        protected string strAppTitle;
        protected string strAppData;
        protected NotepadCloneSettings settings;
        protected bool isFileDirty = false;

        protected Menu menu;
        protected TextBox txtbox;
        protected StatusBar status;

        string strLoadedFile;
        StatusBarItem statLineCol;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run(new NotepadClone());
        }

        public NotepadClone()
        {
            var asmbly = Assembly.GetExecutingAssembly();
            var title = (AssemblyTitleAttribute)asmbly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
            strAppTitle = title.Title;
            var product = (AssemblyProductAttribute)asmbly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0];
            strAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Petzold", product.Product, $"{product.Product}.Settings.xml");

            var dock = new DockPanel();
            Content = dock;

            menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            status = new StatusBar();
            dock.Children.Add(status);
            DockPanel.SetDock(status, Dock.Bottom);

            statLineCol = new StatusBarItem();
            statLineCol.HorizontalAlignment = HorizontalAlignment.Right;
            status.Items.Add(statLineCol);
            DockPanel.SetDock(statLineCol, Dock.Right);

            txtbox = new TextBox();
            txtbox.AcceptsReturn = true;
            txtbox.AcceptsTab = true;
            txtbox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtbox.TextChanged += TextBoxOnTextChanged;
            txtbox.SelectionChanged += TextBoxOnSelectionChanged;
            dock.Children.Add(txtbox);

            AddFileMenu();
            AddEditMenu();
            AddFormatMenu();
            AddViewMenu();
            AddHelpMenu();

            settings = (NotepadCloneSettings)LoadSettings();

            WindowState = settings.WindowState;
            if (settings.RestoreBounds != Rect.Empty)
            {
                Left = settings.RestoreBounds.Left;
                Top = settings.RestoreBounds.Top;
                Width = settings.RestoreBounds.Width;
                Height = settings.RestoreBounds.Height;
            }

            txtbox.TextWrapping = settings.TextWrapping;
            txtbox.FontFamily = new FontFamily(settings.FontFamily);
            txtbox.FontStyle = (FontStyle)new FontStyleConverter().ConvertFromString(settings.FontStyle);
            txtbox.FontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(settings.FontWeight);
            txtbox.FontStretch = (FontStretch)new FontStretchConverter().ConvertFromString(settings.FontStretch);
            txtbox.FontSize = settings.FontSize;

            Loaded += WindowOnLoaded;
            txtbox.Focus();
        }

        protected virtual object LoadSettings() => NotepadCloneSettings.Load<NotepadCloneSettings>(strAppData);

        private void WindowOnLoaded(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.New.Execute(null, this);

            var strArgs = Environment.GetCommandLineArgs();

            if (strArgs.Length <= 1)
                return;

            if (File.Exists(strArgs[1]))
            {
                LoadFile(strArgs[1]);
                return;
            }

            var result = MessageBox.Show($"Cannot find the {Path.GetFileName(strArgs[1])} file.{Environment.NewLine}{Environment.NewLine}Do you want to create a new file?", strAppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel)
                Close();
            else if (result == MessageBoxResult.No)
                return;

            try
            {
                File.Create(strLoadedFile = strArgs[1]).Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error on File Creation: {ex.Message}", strAppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            UpdateTitle();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = !OkToTrash();
            settings.RestoreBounds = RestoreBounds;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            settings.WindowState = WindowState;
            settings.TextWrapping = txtbox.TextWrapping;
            settings.FontFamily = new FontFamilyConverter().ConvertToString(txtbox.FontFamily);
            settings.FontStyle = new FontStyleConverter().ConvertToString(txtbox.FontStyle);
            settings.FontWeight = new FontWeightConverter().ConvertToString(txtbox.FontWeight);
            settings.FontStretch = new FontStretchConverter().ConvertToString(txtbox.FontStretch);
            settings.FontSize = txtbox.FontSize;

            SaveSettings();
        }

        protected virtual void SaveSettings()
        {
            settings.Save(strAppData);
        }

        protected void UpdateTitle()
        {
            Title = $"{strLoadedFile ?? "Untitled"} - {strAppTitle}";
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            isFileDirty = true;
        }

        private void TextBoxOnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var iChar = txtbox.SelectionStart;
            var iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);

            if (iLine == -1)
            {
                statLineCol.Content = string.Empty;
                return;
            }

            iChar -= txtbox.GetCharacterIndexFromLineIndex(iLine);

            var str = $"Line {iLine + 1} Col {iChar + 1}";
            if (txtbox.SelectionLength > 0)
            {
                iChar += txtbox.SelectionLength;
                iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);
                iChar -= txtbox.GetCharacterIndexFromLineIndex(iLine);

                str += $" - Line {iLine + 1} Col {iChar + 1}";
            }

            statLineCol.Content = str;
        }
    }
}
