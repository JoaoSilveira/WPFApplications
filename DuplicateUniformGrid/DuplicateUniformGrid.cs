using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DuplicateUniformGrid
{
    class DuplicateUniformGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DuplicateUniformGrid());
        }

        public DuplicateUniformGrid()
        {
            Title = "Duplicate Uniform Grid";

            var unigrid = new UniformGridAlmost();
            unigrid.Columns = 5;
            Content = unigrid;

            var rand = new Random();

            for (var index = 0; index < 48; index++)
            {
                var btn = new Button();
                btn.Name = $"Button{index}";
                btn.Content = btn.Name;
                btn.FontSize += rand.Next(10);
                unigrid.Children.Add(btn);
            }

            AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ButtonOnClick));
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as Button;
            MessageBox.Show($"{btn.Name} has been clicked", Title);
        }
    }
}
