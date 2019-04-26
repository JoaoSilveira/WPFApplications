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

namespace PrintWithMargins
{
    class PageMarginsDialog : Window
    {
        enum Side
        {
            Left, Right, Top, Bottom
        }

        TextBox[] txtbox = new TextBox[4];
        Button btnOk;

        public Thickness PageMargins
        {
            get
            {
                return new Thickness(
                    double.Parse(txtbox[(int)Side.Left].Text) * 96,
                    double.Parse(txtbox[(int)Side.Top].Text) * 96,
                    double.Parse(txtbox[(int)Side.Right].Text) * 96,
                    double.Parse(txtbox[(int)Side.Bottom].Text) * 96);
            }
            set
            {
                txtbox[(int)Side.Left].Text = (value.Left / 96).ToString("F3");
                txtbox[(int)Side.Top].Text = (value.Top / 96).ToString("F3");
                txtbox[(int)Side.Right].Text = (value.Right / 96).ToString("F3");
                txtbox[(int)Side.Bottom].Text = (value.Bottom / 96).ToString("F3");
            }
        }

        public PageMarginsDialog()
        {
            Title = "Page Setup";
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;

            var stack = new StackPanel();
            Content = stack;

            var grpbox = new GroupBox();
            grpbox.Header = "Margins (inches)";
            grpbox.Margin = new Thickness(12);
            stack.Children.Add(grpbox);

            var grid = new Grid();
            grid.Margin = new Thickness(6);
            grpbox.Content = grid;

            for (var i = 0; i < 2; i++)
            {
                var rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            for (var i = 0; i < 4; i++)
            {
                var coldef = new ColumnDefinition();
                coldef.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(coldef);
            }

            for (var i = 0; i < 4; i++)
            {
                var lbl = new Label();
                lbl.Content = $"_{Enum.GetName(typeof(Side), i)}:";
                lbl.Margin = new Thickness(6);
                lbl.VerticalAlignment = VerticalAlignment.Center;
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, i >> 1);
                Grid.SetColumn(lbl, (i & 1) << 1);

                txtbox[i] = new TextBox();
                txtbox[i].TextChanged += TextBoxOnTextChanged;
                txtbox[i].MinWidth = 48;
                txtbox[i].Margin = new Thickness(6);
                grid.Children.Add(txtbox[i]);
                Grid.SetRow(txtbox[i], i >> 1);
                Grid.SetColumn(txtbox[i], ((i & 1) << 1) + 1);
            }

            var unigrid = new UniformGrid();
            unigrid.Rows = 1;
            unigrid.Columns = 2;
            stack.Children.Add(unigrid);

            btnOk = new Button();
            btnOk.Content = "OK";
            btnOk.IsDefault = true;
            btnOk.IsEnabled = false;
            btnOk.MinWidth = 60;
            btnOk.Margin = new Thickness(12);
            btnOk.HorizontalAlignment = HorizontalAlignment.Center;
            btnOk.Click += OkButtonOnClick;
            unigrid.Children.Add(btnOk);

            var btnCancel = new Button();
            btnCancel.Content = "Cancel";
            btnCancel.IsCancel = true;
            btnCancel.MinWidth = 60;
            btnCancel.Margin = new Thickness(12);
            btnCancel.HorizontalAlignment = HorizontalAlignment.Center;
            unigrid.Children.Add(btnCancel);
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            btnOk.IsEnabled =
                double.TryParse(txtbox[(int)Side.Left].Text, out var result) &&
                double.TryParse(txtbox[(int)Side.Top].Text, out result) &&
                double.TryParse(txtbox[(int)Side.Right].Text, out result) &&
                double.TryParse(txtbox[(int)Side.Bottom].Text, out result);
        }

        private void OkButtonOnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
