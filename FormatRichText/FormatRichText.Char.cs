using SelectColorFromGrid;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FormatRichText
{
    public partial class FormatRichText : Window
    {
        ComboBox comboFamily;
        ComboBox comboSize;
        ToggleButton btnBold;
        ToggleButton btnItalic;
        ColorGridBox clrboxBackground;
        ColorGridBox clrboxForeground;
        string strOriginal;

        void AddCharToolBar(ToolBarTray tray, int band, int index)
        {
            var toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            comboFamily = new ComboBox();
            comboFamily.Width = 144;
            comboFamily.ItemsSource = Fonts.SystemFontFamilies;
            comboFamily.SelectedItem = txtbox.FontFamily;
            comboFamily.SelectionChanged += FamilyComboOnSelection;
            toolbar.Items.Add(comboFamily);

            var tip = new ToolTip();
            tip.Content = "Font Family";
            comboFamily.ToolTip = tip;

            comboSize = new ComboBox();
            comboSize.Width = 48;
            comboSize.IsEditable = true;
            comboSize.Text = (.75 * txtbox.FontSize).ToString();
            comboSize.ItemsSource = new[] { 8d, 9d, 10d, 11d, 12d, 14d, 16d, 18d, 20d, 22d, 24d, 26d, 28d, 36d, 48d, 72d };
            comboSize.SelectionChanged += SizeComboOnSelection;
            comboSize.GotKeyboardFocus += SizeComboOnGotFocus;
            comboSize.LostKeyboardFocus += SizeComboOnLostFocus;
            comboSize.PreviewKeyDown += SizeComboOnKeyDown;
            toolbar.Items.Add(comboSize);

            tip = new ToolTip();
            tip.Content = "Font Size";
            comboSize.ToolTip = tip;

            btnBold = new ToggleButton();
            btnBold.Checked += BoldButtonOnChecked;
            btnBold.Unchecked += BoldButtonOnChecked;
            toolbar.Items.Add(btnBold);

            var img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Bold.png"));
            img.Stretch = Stretch.None;
            btnBold.Content = img;

            tip = new ToolTip();
            tip.Content = "Bold";
            btnBold.ToolTip = tip;

            btnItalic = new ToggleButton();
            btnItalic.Checked += ItalicButtonOnChecked;
            btnItalic.Unchecked += ItalicButtonOnChecked;
            toolbar.Items.Add(btnItalic);

            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Italic.png"));
            img.Stretch = Stretch.None;
            btnItalic.Content = img;

            tip = new ToolTip();
            tip.Content = "Italic";
            btnItalic.ToolTip = tip;

            toolbar.Items.Add(new Separator());

            var menu = new Menu();
            toolbar.Items.Add(menu);

            var item = new MenuItem();
            menu.Items.Add(item);

            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,,/Images/BackgroundColor.png"));
            img.Stretch = Stretch.None;
            item.Header = img;

            clrboxBackground = new ColorGridBox();
            clrboxBackground.SelectionChanged += BackgroundOnSelectionChanged;
            item.Items.Add(clrboxBackground);

            tip = new ToolTip();
            tip.Content = "Background Color";
            item.ToolTip = tip;

            item = new MenuItem();
            menu.Items.Add(item);

            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,,/Images/ForegroundColor.png"));
            img.Stretch = Stretch.None;
            item.Header = img;

            clrboxForeground = new ColorGridBox();
            clrboxForeground.SelectionChanged += ForegroundOnSelectionChanged;
            item.Items.Add(clrboxForeground);

            tip = new ToolTip();
            tip.Content = "Foreground Color";
            item.ToolTip = tip;

            txtbox.SelectionChanged += TextBoxOnSelectionChanged;
        }

        void TextBoxOnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontFamilyProperty);

            if (obj is FontFamily)
                comboFamily.SelectedItem = (FontFamily)obj;
            else
                comboFamily.SelectedIndex = -1;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontSizeProperty);

            if (obj is double)
                comboSize.Text = (.75 * (double)obj).ToString();
            else
                comboSize.SelectedIndex = -1;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontWeightProperty);

            if (obj is FontWeight)
                btnBold.IsChecked = (FontWeight)obj == FontWeights.Bold;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontStyleProperty);

            if (obj is FontStyle)
                btnItalic.IsChecked = (FontStyle)obj == FontStyles.Italic;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.BackgroundProperty);

            if (obj != null && obj is Brush)
                clrboxBackground.SelectedValue = (Brush)obj;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.ForegroundProperty);

            if (obj != null && obj is Brush)
                clrboxForeground.SelectedValue = (Brush)obj;
        }

        void FamilyComboOnSelection(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;

            if (combo.SelectedItem is FontFamily family)
                txtbox.Selection.ApplyPropertyValue(FlowDocument.FontFamilyProperty, family);

            txtbox.Focus();
        }

        void SizeComboOnGotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            strOriginal = (sender as ComboBox).Text;
        }

        void SizeComboOnLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (double.TryParse((sender as ComboBox).Text, out var size))
                txtbox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, size / .75);
            else
                (sender as ComboBox).Text = strOriginal;
        }

        void SizeComboOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                (sender as ComboBox).Text = strOriginal;

                e.Handled = true;
                txtbox.Focus();
            }
            else if (e.Key == Key.Enter)
            {
                e.Handled = true;
                txtbox.Focus();
            }
        }

        void SizeComboOnSelection(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;

            if (combo.SelectedIndex != -1)
            {
                var size = (double)combo.SelectedValue;
                txtbox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, size / .75);

                txtbox.Focus();
            }
        }

        void BoldButtonOnChecked(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as ToggleButton;

            txtbox.Selection.ApplyPropertyValue(FlowDocument.FontWeightProperty, (bool)btn.IsChecked ? FontWeights.Bold : FontWeights.Regular);
        }

        void ItalicButtonOnChecked(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as ToggleButton;

            txtbox.Selection.ApplyPropertyValue(FlowDocument.FontStyleProperty, (bool)btn.IsChecked ? FontStyles.Italic : FontStyles.Normal);
        }

        void BackgroundOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clrbox = e.Source as ColorGridBox;

            txtbox.Selection.ApplyPropertyValue(FlowDocument.BackgroundProperty, clrbox.SelectedValue);
        }

        void ForegroundOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clrbox = e.Source as ColorGridBox;

            txtbox.Selection.ApplyPropertyValue(FlowDocument.ForegroundProperty, clrbox.SelectedValue);
        }
    }
}
