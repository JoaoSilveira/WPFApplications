using FlowDocumentHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Generic
{
    public partial class GenericWindow : Window
    {
        public GenericWindow()
        {
            InitializeComponent();
        }

        void HelpOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var win = new HelpWindow();
            win.Owner = this;
            win.Title = "Help for Generic";
            win.Show();
        }
    }
}
