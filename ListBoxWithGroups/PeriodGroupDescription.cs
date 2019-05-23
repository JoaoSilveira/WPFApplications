using SingleRecordDataEntry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ListBoxWithGroups
{
    public class PeriodGroupDescription : GroupDescription
    {
        public override object GroupNameFromItem(object item, int level, CultureInfo culture)
        {
            var person = item as Person;

            if (!person.BirthDate.HasValue)
                return "Unknown";

            var year = person.BirthDate.Value.Year;

            if (year < 1575)
                return "Pre-Baroque";

            if (year < 1725)
                return "Baroque";

            if (year < 1795)
                return "Classical";

            if (year < 1870)
                return "Romantic";

            if (year < 1910)
                return "20th Century";

            return "Post-War";
        }
    }
}
