using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace LoadEmbeddedXaml
{
    class LoadEmbeddedXaml : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new LoadEmbeddedXaml());
        }

        public LoadEmbeddedXaml()
        {
            Title = "Load EmbeddedXaml";

            string strXaml =
                @"<Button xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" Foreground=""LightSeaGreen"" FontSize=""24pt"">
            Hello, XAML!
        </Button>";

            Content = XamlReader.Load(new XmlTextReader(new StringReader(strXaml)));
        }
    }
}
