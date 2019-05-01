using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace IncludeApplicationDefinition
{
    public partial class MyWindow : Window
    {
        public MyWindow()
        {
            InitializeComponent();
        }

        void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"The button labeled '{(sender as Button).Content}' has been clicked");
        }
    }
}
