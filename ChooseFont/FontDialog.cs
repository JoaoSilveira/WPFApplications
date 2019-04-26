using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChooseFont
{
    public class FontDialog : Window
    {
        TextBoxWithLister boxFamily;
        TextBoxWithLister boxStyle;
        TextBoxWithLister boxWeight;
        TextBoxWithLister boxStretch;
        TextBoxWithLister boxSize;
        Label lblDisplay;
        bool isUpdateSupressed = true;

        public Typeface Typeface
        {
            get
            {
                return new Typeface(
                    (FontFamily)boxFamily.SelectedItem,
                    (FontStyle)boxStyle.SelectedItem,
                    (FontWeight)boxWeight.SelectedItem,
                    (FontStretch)boxStretch.SelectedItem);
            }
            set
            {
                if (boxFamily.Contains(value.FontFamily))
                    boxFamily.SelectedItem = value.FontFamily;
                else
                    boxFamily.SelectedIndex = 0;

                if (boxStyle.Contains(value.Style))
                    boxStyle.SelectedItem = value.Style;
                else
                    boxStyle.SelectedIndex = 0;

                if (boxWeight.Contains(value.Weight))
                    boxWeight.SelectedItem = value.Weight;
                else
                    boxWeight.SelectedIndex = 0;

                if (boxStretch.Contains(value.Stretch))
                    boxStretch.SelectedItem = value.Stretch;
                else
                    boxStretch.SelectedIndex = 0;
            }
        }

        public double FaceSize
        {
            get
            {
                if (!double.TryParse(boxSize.Text, out var size))
                    size = 8.25;

                return size / .75;
            }
            set
            {
                var size = .75 * value;
                boxSize.Text = size.ToString();

                if (!boxSize.Contains(size))
                    boxSize.Insert(0, size);

                boxSize.SelectedItem = size;
            }
        }

        public FontDialog()
        {
            Title = "Font";
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;

            var gridMain = new Grid();
            Content = gridMain;

            var rowdef = new RowDefinition();
            rowdef.Height = new GridLength(200, GridUnitType.Pixel);
            gridMain.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(150, GridUnitType.Pixel);
            gridMain.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            gridMain.RowDefinitions.Add(rowdef);

            var coldef = new ColumnDefinition();
            coldef.Width = new GridLength(650, GridUnitType.Pixel);
            gridMain.ColumnDefinitions.Add(coldef);

            var gridBoxes = new Grid();
            gridMain.Children.Add(gridBoxes);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            gridBoxes.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(100, GridUnitType.Star);
            gridBoxes.RowDefinitions.Add(rowdef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(175, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(75, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            var lbl = new Label();
            lbl.Content = "Font Family";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);

            boxFamily = new TextBoxWithLister();
            boxFamily.IsReadOnly = true;
            boxFamily.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxFamily);
            Grid.SetRow(boxFamily, 1);
            Grid.SetColumn(boxFamily, 0);

            lbl = new Label();
            lbl.Content = "Style";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 1);

            boxStyle = new TextBoxWithLister();
            boxStyle.IsReadOnly = true;
            boxStyle.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxStyle);
            Grid.SetRow(boxStyle, 1);
            Grid.SetColumn(boxStyle, 1);

            lbl = new Label();
            lbl.Content = "Weight";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 2);

            boxWeight = new TextBoxWithLister();
            boxWeight.IsReadOnly = true;
            boxWeight.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxWeight);
            Grid.SetRow(boxWeight, 1);
            Grid.SetColumn(boxWeight, 2);

            lbl = new Label();
            lbl.Content = "Stretch";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 3);

            boxStretch = new TextBoxWithLister();
            boxStretch.IsReadOnly = true;
            boxStretch.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxStretch);
            Grid.SetRow(boxStretch, 1);
            Grid.SetColumn(boxStretch, 3);

            lbl = new Label();
            lbl.Content = "Size";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 4);

            boxSize = new TextBoxWithLister();
            boxSize.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxSize);
            Grid.SetRow(boxSize, 1);
            Grid.SetColumn(boxSize, 4);

            lblDisplay = new Label();
            lblDisplay.Content = "AaBbCc XxYyZz 012345";
            lblDisplay.HorizontalAlignment = HorizontalAlignment.Center;
            lblDisplay.VerticalAlignment = VerticalAlignment.Center;
            gridMain.Children.Add(lblDisplay);
            Grid.SetRow(lblDisplay, 1);

            var gridButtons = new Grid();
            gridMain.Children.Add(gridButtons);
            Grid.SetRow(gridButtons, 2);

            for (var i = 0; i < 5; i++)
                gridButtons.ColumnDefinitions.Add(new ColumnDefinition());

            var btn = new Button();
            btn.Content = "OK";
            btn.IsDefault = true;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.MinWidth = 60;
            btn.Margin = new Thickness(12);
            btn.Click += OkOnClick;
            gridButtons.Children.Add(btn);
            Grid.SetColumn(btn, 1);

            btn = new Button();
            btn.Content = "Cancel";
            btn.IsCancel = true;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.MinWidth = 60;
            btn.Margin = new Thickness(12);
            gridButtons.Children.Add(btn);
            Grid.SetColumn(btn, 3);

            foreach (var fam in Fonts.SystemFontFamilies)
                boxFamily.Add(fam);

            foreach (var size in new double[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 })
                boxSize.Add(size);

            boxFamily.SelectionChanged += FamilyOnSelectionChanged;
            boxStyle.SelectionChanged += StyleOnSelectionChanged;
            boxWeight.SelectionChanged += StyleOnSelectionChanged;
            boxStretch.SelectionChanged += StyleOnSelectionChanged;
            boxSize.TextChanged += SizeOnTextChanged;

            Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            FaceSize = FontSize;

            boxFamily.Focus();
            isUpdateSupressed = false;
            UpdateSample();
        }

        private void FamilyOnSelectionChanged(object sender, EventArgs e)
        {
            var fntfam = (FontFamily)boxFamily.SelectedItem;

            var fntstyPrevious = boxStyle.SelectedItem as FontStyle?;
            var fntwtPrevious = boxWeight.SelectedItem as FontWeight?;
            var fntstrPrevious = boxStretch.SelectedItem as FontStretch?;

            isUpdateSupressed = true;

            boxStyle.Clear();
            boxWeight.Clear();
            boxStretch.Clear();

            foreach (var ftf in fntfam.FamilyTypefaces)
            {
                if (!boxStyle.Contains(ftf.Style))
                {
                    if (ftf.Style == FontStyles.Normal)
                        boxStyle.Insert(0, ftf.Style);
                    else
                        boxStyle.Add(ftf.Style);
                }

                if (!boxWeight.Contains(ftf.Weight))
                {
                    if (ftf.Weight == FontWeights.Regular)
                        boxWeight.Insert(0, ftf.Weight);
                    else
                        boxWeight.Add(ftf.Weight);
                }

                if (!boxStretch.Contains(ftf.Stretch))
                {
                    if (ftf.Stretch == FontStretches.Normal)
                        boxStretch.Insert(0, ftf.Stretch);
                    else
                        boxStretch.Add(ftf.Stretch);
                }
            }

            if (boxStyle.Contains(fntstyPrevious))
                boxStyle.SelectedItem = fntstyPrevious;
            else
                boxStyle.SelectedIndex = 0;

            if (boxWeight.Contains(fntwtPrevious))
                boxWeight.SelectedItem = fntwtPrevious;
            else
                boxWeight.SelectedIndex = 0;

            if (boxStretch.Contains(fntstrPrevious))
                boxStretch.SelectedItem = fntstrPrevious;
            else
                boxStretch.SelectedIndex = 0;

            isUpdateSupressed = false;
            UpdateSample();
        }

        private void StyleOnSelectionChanged(object sender, EventArgs e)
        {
            UpdateSample();
        }

        private void SizeOnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSample();
        }

        private void UpdateSample()
        {
            if (isUpdateSupressed)
                return;

            lblDisplay.FontFamily = (FontFamily)boxFamily.SelectedItem;
            lblDisplay.FontStyle = (FontStyle)boxStyle.SelectedItem;
            lblDisplay.FontWeight = (FontWeight)boxWeight.SelectedItem;
            lblDisplay.FontStretch = (FontStretch)boxStretch.SelectedItem;

            if (!double.TryParse(boxSize.Text, out var size))
                size = 8.25;

            lblDisplay.FontSize = size / .75;
        }

        private void OkOnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
