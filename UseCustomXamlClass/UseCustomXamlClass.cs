using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UseCustomXamlClass
{
    public partial class UseCustomXamlClass : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new UseCustomXamlClass());
        }

        public UseCustomXamlClass()
        {
            InitializeComponent();

            for (var i = 0; i < 5; i++)
            {
                var btn = new CenteredButton();
                btn.Content = $"Button No. {i + 1}";
                stack.Children.Add(btn);
            }
        }
    }
}
