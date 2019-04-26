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
    class TextBoxWithLister : ContentControl
    {
        TextBox txtbox;
        Lister lister;

        public event EventHandler SelectionChanged;
        public event TextChangedEventHandler TextChanged;

        public string Text
        {
            get { return txtbox.Text; }
            set { txtbox.Text = value; }
        }

        public bool IsReadOnly { get; set; }

        public object SelectedItem
        {
            get { return lister.SelectedItem; }
            set { lister.SelectedItem = value; }
        }

        public int SelectedIndex
        {
            get { return lister.SelectedIndex; }
            set { lister.SelectedIndex = value; }
        }

        public TextBoxWithLister()
        {
            var dock = new DockPanel();
            Content = dock;

            txtbox = new TextBox();
            txtbox.TextChanged += TextBoxOnTextChanged;
            dock.Children.Add(txtbox);
            DockPanel.SetDock(txtbox, Dock.Top);

            lister = new Lister();
            lister.SelectionChanged += ListerOnSelectionChanged;
            dock.Children.Add(lister);
        }

        public void Add(object obj)
        {
            lister.Add(obj);
        }

        public void Insert(int index, object obj)
        {
            lister.Insert(index, obj);
        }

        public void Clear()
        {
            lister.Clear();
        }

        public bool Contains(object obj)
        {
            return lister.Contains(obj);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);

            if (e.NewFocus != this)
                return;

            txtbox.Focus();
            if (SelectedIndex == -1 && lister.Count > 0)
                SelectedIndex = 0;
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);

            if (!IsReadOnly)
                return;

            lister.GotToLetter(e.Text[0]);
            e.Handled = true;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (SelectedIndex == -1)
                return;

            switch (e.Key)
            {
                case Key.Home: if (lister.Count > 0) SelectedIndex = 0; break;
                case Key.End: if (lister.Count > 0) SelectedIndex = lister.Count - 1; break;
                case Key.Up: if (SelectedIndex > 0) SelectedIndex--; break;
                case Key.Down: if (SelectedIndex < lister.Count - 1) SelectedIndex++; break;
                case Key.PageUp: lister.PageUp(); break;
                case Key.PageDown: lister.PageDown(); break;
                default:
                    return;
            }
            e.Handled = true;
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }

        private void ListerOnSelectionChanged(object sender, EventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
            txtbox.Text = lister.SelectedItem?.ToString() ?? string.Empty;
        }
    }
}
