using Microsoft.Win32;
using SingleRecordDataEntry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MultiRecordDataEntry
{
    public class People : ObservableCollection<Person>
    {
        const string strFilter = "People XML files (*.PeopleXml)|*.PeopleXml|All Files (*.*)|*.*";

        public static People Load(Window win)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = strFilter;

            if (!dlg.ShowDialog().GetValueOrDefault())
                return null;

            try
            {
                using (var reader = new StreamReader(dlg.FileName))
                {
                    return (People)new XmlSerializer(typeof(People)).Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load file: {ex.Message}", win.Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        public bool Save(Window win)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = strFilter;

            if (!dlg.ShowDialog().GetValueOrDefault())
                return true;

            try
            {
                using (var writer = new StreamWriter(dlg.FileName))
                {
                    new XmlSerializer(typeof(People)).Serialize(writer, this);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save file: {ex.Message}", win.Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
        }
    }
}
