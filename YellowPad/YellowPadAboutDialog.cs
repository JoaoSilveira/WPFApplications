using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;

namespace YellowPad
{
    public partial class YellowPadAboutDialog : Window
    {
        public YellowPadAboutDialog()
        {
            InitializeComponent();

            var uri = new Uri("pack://application:,,,/Images/Signature.xaml");
            using (var stream = Application.GetResourceStream(uri).Stream)
            {
                var drawing = (Drawing)XamlReader.Load(stream);
                imgSignature.Source = new DrawingImage(drawing);
            }
        }

        void LinkOnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.OriginalString);
            e.Handled = true;
        }
    }
}
