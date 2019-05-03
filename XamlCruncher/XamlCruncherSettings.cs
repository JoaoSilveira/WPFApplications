using NotepadClone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace XamlCruncher
{
    public class XamlCruncherSettings : NotepadCloneSettings
    {
        public Dock Orientation = Dock.Left;
        public int TabSpaces = 4;
        public string StartupDocument = "<Button xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">\r\n    Hello, XAML!\r\n</Button>";

        public XamlCruncherSettings()
        {
            FontFamily = "Lucida Console";
            FontSize = 10 / .75;
        }
    }
}
