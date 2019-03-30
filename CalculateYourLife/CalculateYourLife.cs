using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CalculateYourLife
{
    class CalculateYourLife : Window
    {
        TextBox txtboxBegin;
        TextBox txtboxEnd;
        Label lifeYears;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CalculateYourLife());
        }

        public CalculateYourLife()
        {
            Title = "Calculate Your Life";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;

            var grid = new Grid();
            Content = grid;

            for (int i = 0; i < 3; i++)
            {
                var rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            for (int i = 0; i < 2; i++)
            {
                var coldef = new ColumnDefinition();
                coldef.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(coldef);
            }

            var lbl = new Label();
            lbl.Content = "Begin date: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);

            txtboxBegin = new TextBox();
            txtboxBegin.Text = new DateTime(1980, 1, 1).ToShortDateString();
            txtboxBegin.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtboxBegin);
            Grid.SetRow(txtboxBegin, 0);
            Grid.SetColumn(txtboxBegin, 1);

            lbl = new Label();
            lbl.Content = "End date: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 1);
            Grid.SetColumn(lbl, 0);

            txtboxEnd = new TextBox();
            txtboxEnd.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtboxEnd);
            Grid.SetRow(txtboxEnd, 1);
            Grid.SetColumn(txtboxEnd, 1);

            lbl = new Label();
            lbl.Content = "Life Years: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 2);
            Grid.SetColumn(lbl, 0);

            lifeYears = new Label();
            grid.Children.Add(lifeYears);
            Grid.SetRow(lifeYears, 2);
            Grid.SetColumn(lifeYears, 1);

            var thick = new Thickness(5);
            grid.Margin = thick;

            foreach (Control child in grid.Children)
            {
                child.Margin = thick;
            }

            txtboxBegin.Focus();
            txtboxEnd.Text = DateTime.Now.ToShortDateString();
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DateTime.TryParse(txtboxBegin.Text, out DateTime begin) && DateTime.TryParse(txtboxEnd.Text, out DateTime end))
            {
                int years = end.Year - begin.Year;
                int months = end.Month - begin.Month;
                int days = end.Day - begin.Day;

                if (days < 0)
                {
                    days += DateTime.DaysInMonth(end.Year, 1 + (end.Month + 10) % 12);
                    months -= 1;
                }

                if (months < 0)
                {
                    months += 12;
                    years -= 1;
                }

                lifeYears.Content = $"{years} year{(years > 0 ? "s" : string.Empty)}, {months} month{(months > 0 ? "s" : string.Empty)}, {days} day{(days > 0 ? "s" : string.Empty)}";
            }
            else
            {
                lifeYears.Content = string.Empty;
            }
        }
    }
}
