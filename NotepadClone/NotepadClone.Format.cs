using ChooseFont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace NotepadClone
{
    public partial class NotepadClone
    {
        void AddFormatMenu()
        {
            var itemFormat = new MenuItem();
            itemFormat.Header = "F_ormat";
            menu.Items.Add(itemFormat);

            var itemWrap = new WordWrapMenuItem();
            itemFormat.Items.Add(itemWrap);

            var bind = new Binding();
            bind.Path = new PropertyPath(TextBox.TextWrappingProperty);
            bind.Source = txtbox;
            bind.Mode = BindingMode.TwoWay;
            itemWrap.SetBinding(WordWrapMenuItem.WordWrapProperty, bind);

            var itemFont = new MenuItem();
            itemFont.Header = "_Font...";
            itemFont.Click += FontOnClick;
            itemFormat.Items.Add(itemFont);
        }

        private void FontOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new FontDialog();
            dlg.Owner = this;

            dlg.Typeface = new Typeface(txtbox.FontFamily, txtbox.FontStyle, txtbox.FontWeight, txtbox.FontStretch);

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            txtbox.FontFamily = dlg.Typeface.FontFamily;
            txtbox.FontStyle = dlg.Typeface.Style;
            txtbox.FontWeight = dlg.Typeface.Weight;
            txtbox.FontStretch = dlg.Typeface.Stretch;
        }
    }
}
