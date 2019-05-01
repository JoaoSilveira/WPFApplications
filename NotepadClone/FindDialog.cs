using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotepadClone
{
    public class FindDialog : FindReplaceDialog
    {
        public FindDialog(Window owner) : base(owner)
        {
            Title = "Find";

            lblReplace.Visibility = Visibility.Collapsed;
            txtboxReplace.Visibility = Visibility.Collapsed;
            btnReplace.Visibility = Visibility.Collapsed;
            btnAll.Visibility = Visibility.Collapsed;
        }
    }
}
