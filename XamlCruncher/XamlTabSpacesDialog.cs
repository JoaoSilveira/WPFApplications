using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace XamlCruncher
{
    public partial class XamlTabSpacesDialog : Window
    {
        public int TabSpaces
        {
            get { return int.Parse(txtbox.Text); }
            set { txtbox.Text = value.ToString(); }
        }
        public XamlTabSpacesDialog()
        {
            InitializeComponent();
            txtbox.Focus();
        }

        void TextBoxOnTextChanged(object sender, TextChangedEventArgs e) => btnOk.IsEnabled = int.TryParse(txtbox.Text, out var tabs) && tabs > 0 && tabs < 11;

        void OkOnClick(object sender, RoutedEventArgs e) => DialogResult = true;
    }
}
