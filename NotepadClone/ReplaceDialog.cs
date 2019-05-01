using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotepadClone
{
    public class ReplaceDialog : FindReplaceDialog
    {
        public ReplaceDialog(Window owner) : base(owner)
        {
            Title = "Replace";
        }
    }
}
