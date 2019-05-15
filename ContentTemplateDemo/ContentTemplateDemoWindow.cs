using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ContentTemplateDemo
{
    public partial class ContentTemplateDemoWindow : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ContentTemplateDemoWindow());
        }

        public ContentTemplateDemoWindow()
        {
            InitializeComponent();

            var btn = new EmployeeButton();
            btn.Content = new Employee("Jim", "Jim.png", new DateTime(1975, 6, 15), false);
            stack.Children.Add(btn);
        }

        void EmployeeButtonOnClick(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as Button;
            var emp = btn.Content as Employee;
            MessageBox.Show($"{emp.Name} button clicked!", Title);
        }
    }
}
