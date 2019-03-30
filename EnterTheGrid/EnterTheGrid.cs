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

namespace EnterTheGrid
{
    class EnterTheGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new EnterTheGrid());
        }

        public EnterTheGrid()
        {
            Title = "Enter the Grid";
            MinWidth = 300;
            SizeToContent = SizeToContent.WidthAndHeight;

            var stack = new StackPanel();
            Content = stack;

            var grid1 = new Grid();
            grid1.Margin = new Thickness(5);
            stack.Children.Add(grid1);

            for (var i = 0; i < 5; i++)
            {
                var rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid1.RowDefinitions.Add(rowdef);
            }

            var coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid1.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            grid1.ColumnDefinitions.Add(coldef);

            var labels = new[]
            {
                "_Fist name:",
                "_Last name:",
                "_Social security number:",
                "_Credit card number:",
                "_Other personal stuff:",
            };

            for (var i = 0; i < labels.Length; i++)
            {
                var lbl = new Label();
                lbl.Content = labels[i];
                lbl.VerticalAlignment = VerticalAlignment.Center;
                grid1.Children.Add(lbl);
                Grid.SetRow(lbl, i);
                Grid.SetColumn(lbl, 0);

                var txtbox = new TextBox();
                txtbox.Margin = new Thickness(5);
                grid1.Children.Add(txtbox);
                Grid.SetRow(txtbox, i);
                Grid.SetColumn(txtbox, 1);
            }

            var grid2 = new Grid();
            grid2.Margin = new Thickness(10);
            stack.Children.Add(grid2);

            grid2.ColumnDefinitions.Add(new ColumnDefinition());
            grid2.ColumnDefinitions.Add(new ColumnDefinition());

            var btn = new Button();
            btn.Content = "Submit";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.IsDefault = true;
            btn.Click += (sender, e) => Close();
            grid2.Children.Add(btn);

            btn = new Button();
            btn.Content = "Cancel";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.IsCancel = true;
            btn.Click += (sender, e) => Close();
            grid2.Children.Add(btn);
            Grid.SetColumn(btn, 1);

            (stack.Children[0] as Panel).Children[0].Focus();
        }

    }
}
