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
using System.Windows.Media;
using System.Xml.Serialization;

namespace SingleRecordDataEntry
{
    public partial class SingleRecordDataEntryWindow : Window
    {
        const string strFilter = "Person XML Files (*.PersonXml)|*.PersonXml|All Files (*.*)|*.*";

        XmlSerializer xml = new XmlSerializer(typeof(Person));

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SingleRecordDataEntryWindow());
        }

        public SingleRecordDataEntryWindow()
        {
            InitializeComponent();

            ApplicationCommands.New.Execute(null, this);
            pnlPerson.Children[1].Focus();
        }

        void NewOnExecuted(object sender, ExecutedRoutedEventArgs e) => pnlPerson.DataContext = new Person();

        void OpenOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = strFilter;

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            try
            {
                using (var reader = new StreamReader(dlg.FileName))
                {
                    pnlPerson.DataContext = (Person)xml.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load file: {ex.Message}", Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        void SaveOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = strFilter;

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            try
            {
                using (var writer = new StreamWriter(dlg.FileName))
                {
                    xml.Serialize(writer, pnlPerson.DataContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save file: {ex.Message}", Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
