using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NotepadClone
{
    public partial class NotepadClone
    {
        void AddHelpMenu()
        {
            var itemHelp = new MenuItem();
            itemHelp.Header = "_Help";
            itemHelp.SubmenuOpened += ViewOnOpen;
            menu.Items.Add(itemHelp);

            var itemAbout = new MenuItem();
            itemAbout.Header = $"_About {strAppTitle}...";
            itemAbout.Click += AboutOnClick;
            itemHelp.Items.Add(itemAbout);
        }

        private void AboutOnClick(object sender, System.Windows.RoutedEventArgs e) => new AboutDialog(this).ShowDialog();
    }
}
