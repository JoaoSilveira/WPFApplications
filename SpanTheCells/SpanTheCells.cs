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

namespace SpanTheCells
{
    class SpanTheCells : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SpanTheCells());
        }

        public SpanTheCells()
        {
            Title = "Span the Cells";
            SizeToContent = SizeToContent.WidthAndHeight;

            var grid = new Grid();
            grid.Margin = new Thickness(5);
            Content = grid;

            for (var i = 0; i < 6; i++)
            {
                var rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            for (var i = 0; i < 4; i++)
            {
                var coldef = new ColumnDefinition();

                if (i == 1)
                    coldef.Width = new GridLength(1, GridUnitType.Star);
                else
                    coldef.Width = GridLength.Auto;

                grid.ColumnDefinitions.Add(coldef);
            }

            var labels = new[]
            {
                "_First name:",
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
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, i);
                Grid.SetColumn(lbl, 0);

                var txtbox = new TextBox();
                txtbox.Margin = new Thickness(5);
                grid.Children.Add(txtbox);
                Grid.SetRow(txtbox, i);
                Grid.SetColumn(txtbox, 1);
                Grid.SetColumnSpan(txtbox, 3);
            }

            var btn = new Button();
            btn.Content = "Submit";
            btn.Margin = new Thickness(5);
            btn.IsDefault = true;
            btn.Click += (sender, e) => Close();
            grid.Children.Add(btn);
            Grid.SetRow(btn, 5);
            Grid.SetColumn(btn, 2);

            btn = new Button();
            btn.Content = "Cancel";
            btn.Margin = new Thickness(5);
            btn.IsCancel = true;
            btn.Click += (sender, e) => Close();
            grid.Children.Add(btn);
            Grid.SetRow(btn, 5);
            Grid.SetColumn(btn, 3);

            grid.Children[0].Focus();
        }

    }
}
