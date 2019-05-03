using NotepadClone;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Xml;

namespace XamlCruncher
{
    public class XamlCruncher : NotepadClone.NotepadClone
    {
        Frame frameParent;
        Window win;
        StatusBarItem statusParse;
        int tabSpaces = 4;

        XamlCruncherSettings settingsXaml;

        XamlOrientationMenuItem itemOrientation;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run(new XamlCruncher());
        }

        public bool IsSuspendedParsing { get; set; } = false;

        public XamlCruncher()
        {
            strFilter = "XAML Files (*.xaml)|*.xaml|All Files (*.*)|*.*";

            var dock = txtbox.Parent as DockPanel;
            dock.Children.Remove(txtbox);

            var grid = new Grid();
            dock.Children.Add(grid);

            for (var i = 0; i < 3; i++)
            {
                var rowdef = new RowDefinition();
                rowdef.Height = new GridLength(0);
                grid.RowDefinitions.Add(rowdef);

                var coldef = new ColumnDefinition();
                coldef.Width = new GridLength(0);
                grid.ColumnDefinitions.Add(coldef);
            }

            grid.RowDefinitions[0].Height = new GridLength(100, GridUnitType.Star);
            grid.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Star);

            var split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Stretch;
            split.VerticalAlignment = VerticalAlignment.Center;
            split.Height = 6;
            grid.Children.Add(split);
            Grid.SetRow(split, 1);
            Grid.SetColumn(split, 0);
            Grid.SetColumnSpan(split, 3);

            split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            grid.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);
            Grid.SetRowSpan(split, 3);

            frameParent = new Frame();
            frameParent.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            grid.Children.Add(frameParent);

            txtbox.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtbox);

            settingsXaml = (XamlCruncherSettings)settings;

            var itemXaml = new MenuItem();
            itemXaml.Header = "_Xaml";
            menu.Items.Insert(menu.Items.Count - 1, itemXaml);

            itemOrientation = new XamlOrientationMenuItem(grid, txtbox, frameParent);
            itemOrientation.Orientation = settingsXaml.Orientation;
            itemXaml.Items.Add(itemOrientation);

            var itemTabs = new MenuItem();
            itemTabs.Header = "_Tab Spaces...";
            itemTabs.Click += TabSpacesOnClick;
            itemXaml.Items.Add(itemTabs);

            var itemNoParse = new MenuItem();
            itemNoParse.Header = "_Suspend Parsing";
            itemNoParse.IsCheckable = true;
            itemNoParse.SetBinding(MenuItem.IsCheckedProperty, nameof(IsSuspendedParsing));
            itemNoParse.DataContext = this;
            itemXaml.Items.Add(itemNoParse);

            var collGest = new InputGestureCollection();
            collGest.Add(new KeyGesture(Key.F6));
            var commReparse = new RoutedUICommand("_Reparse", "Reparse", GetType(), collGest);

            var itemReparse = new MenuItem();
            itemReparse.Command = commReparse;
            itemXaml.Items.Add(itemReparse);
            CommandBindings.Add(new CommandBinding(commReparse, ReparseOnExecuted));

            collGest = new InputGestureCollection();
            collGest.Add(new KeyGesture(Key.F7));
            var commShowWin = new RoutedUICommand("Show _Window", "ShowWindow", GetType(), collGest);

            var itemShowWin = new MenuItem();
            itemShowWin.Command = commShowWin;
            itemXaml.Items.Add(itemShowWin);
            CommandBindings.Add(new CommandBinding(commShowWin, ShowWindowOnExecuted, ShowWindowCanExecute));

            var itemTemplate = new MenuItem();
            itemTemplate.Header = "Save as Startup _Document";
            itemTemplate.Click += NewStartupOnClick;
            itemXaml.Items.Add(itemTemplate);

            var itemXamlHelp = new MenuItem();
            itemXamlHelp.Header = "_Help...";
            itemXamlHelp.Click += HelpOnClick;
            var itemHelp = (MenuItem)menu.Items[menu.Items.Count - 1];
            itemHelp.Items.Insert(0, itemXamlHelp);

            statusParse = new StatusBarItem();
            status.Items.Insert(0, statusParse);
            status.Visibility = Visibility.Visible;

            Dispatcher.UnhandledException += DispatcherOnUnhandledException;
        }

        protected override void NewOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            base.NewOnExecute(sender, e);
            var str = ((XamlCruncherSettings)settings).StartupDocument;

            str = str.Replace("\r\n", "\n").Replace("\r", "\r\n");
            txtbox.Text = str;
            isFileDirty = false;
        }

        protected override object LoadSettings() => NotepadCloneSettings.Load<XamlCruncherSettings>(strAppData);

        protected override void OnClosed(EventArgs e)
        {
            settingsXaml.Orientation = itemOrientation.Orientation;
            base.OnClosed(e);
        }

        protected override void SaveSettings() => ((XamlCruncherSettings)settings).Save(strAppData);

        private void TabSpacesOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new XamlTabSpacesDialog();
            dlg.Owner = this;
            dlg.TabSpaces = settingsXaml.TabSpaces;

            if (dlg.ShowDialog().GetValueOrDefault())
                settingsXaml.TabSpaces = dlg.TabSpaces;
        }

        private void ReparseOnExecuted(object sender, ExecutedRoutedEventArgs e) => Parse();

        private void ShowWindowCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = win != null;

        private void ShowWindowOnExecuted(object sender, ExecutedRoutedEventArgs e) => win?.Show();

        private void NewStartupOnClick(object sender, RoutedEventArgs e) => ((XamlCruncherSettings)settings).StartupDocument = txtbox.Text;

        private void HelpOnClick(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("pack://application:,,,/XamlCruncherHelp.xaml");
            var stream = Application.GetResourceStream(uri).Stream;

            var win = new Window();
            win.Title = "XAML Cruncher Help";
            win.Content = XamlReader.Load(stream);
            win.Show();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.Source != txtbox || e.Key != Key.Tab)
                return;

            var strInsert = new string(' ', tabSpaces);
            var iChar = txtbox.SelectionStart;
            int iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);

            if (iLine != -1)
            {
                var iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine);
                strInsert = new string(' ', settingsXaml.TabSpaces - iCol % settingsXaml.TabSpaces);
            }

            txtbox.SelectedText = strInsert;
            txtbox.CaretIndex = txtbox.SelectionStart + txtbox.SelectionLength;
            e.Handled = true;
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsSuspendedParsing)
                txtbox.Foreground = SystemColors.WindowTextBrush;
            else
                Parse();
        }

        void Parse()
        {
            using (var xmlReader = new XmlTextReader(new StringReader(txtbox.Text)))
            {
                try
                {
                    var obj = XamlReader.Load(xmlReader);
                    txtbox.Foreground = SystemColors.WindowTextBrush;

                    if (obj is Window win)
                    {
                        this.win = win;
                        statusParse.Content = "Press F7 to display Window";
                    }
                    else
                    {
                        win = null;
                        frameParent.Content = obj;
                        statusParse.Content = "OK";
                    }
                }
                catch (Exception ex)
                {
                    txtbox.Foreground = Brushes.Red;
                    statusParse.Content = ex.Message;
                }
            }
        }

        private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            statusParse.Content = $"Unhandled Exception: {e.Exception.Message}";
            e.Handled = true;
        }
    }
}
