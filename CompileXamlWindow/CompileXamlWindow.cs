using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CompileXamlWindow
{
    public partial class CompileXamlWindow : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CompileXamlWindow());
        }

        public CompileXamlWindow()
        {
            InitializeComponent();

            foreach (var prop in typeof(Brushes).GetProperties())
                lstbox.Items.Add(prop.Name);
        }

        void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"The button labeled '{(sender as Button).Content}' has been clicked");
        }

        void ListBoxOnSelection(object sender, SelectionChangedEventArgs e)
        {
            var lstbox = sender as ListBox;
            var strItem = lstbox.SelectedItem as string;
            var prop = typeof(Brushes).GetProperty(strItem);

            elips.Fill = (Brush)prop.GetValue(null, null);
        }
    }
}
