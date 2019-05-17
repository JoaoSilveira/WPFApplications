using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace SingleRecordDataEntry
{
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string strFirstName = "<first name>";
        string strMiddleName = string.Empty;
        string strLastName = "<last name>";
        DateTime? dtBirthDate = new DateTime(1800, 1, 1);
        DateTime? dtDeathDate = new DateTime(1900, 12, 31);

        public string FirstName
        {
            get { return strFirstName; }
            set
            {
                strFirstName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get { return strMiddleName; }
            set
            {
                strMiddleName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return strLastName; }
            set
            {
                strLastName = value;
                OnPropertyChanged();
            }
        }

        [XmlElement(DataType = "date")]
        public DateTime? BirthDate
        {
            get { return dtBirthDate; }
            set
            {
                dtBirthDate = value;
                OnPropertyChanged();
            }
        }

        [XmlElement(DataType = "date")]
        public DateTime? DeathDate
        {
            get { return dtDeathDate; }
            set
            {
                dtDeathDate = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string strPropertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
    }
}
