using System;
using System.Collections;
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
    class Lister : ContentControl
    {
        ScrollViewer scroll;
        StackPanel stack;
        ArrayList list = new ArrayList();
        int indexSelected = -1;

        public event EventHandler SelectionChanged;

        public int Count => list.Count;

        public int SelectedIndex
        {
            get { return indexSelected; }
            set
            {
                if (value < -1 || value >= Count)
                    throw new ArgumentOutOfRangeException(nameof(SelectedIndex));

                if (value == SelectedIndex)
                    return;

                if (indexSelected != -1)
                {
                    var txtblk = stack.Children[indexSelected] as TextBlock;
                    txtblk.Background = SystemColors.WindowBrush;
                    txtblk.Foreground = SystemColors.WindowTextBrush;
                }

                indexSelected = value;

                if (indexSelected > -1)
                {
                    var txtblk = stack.Children[indexSelected] as TextBlock;
                    txtblk.Background = SystemColors.HighlightBrush;
                    txtblk.Foreground = SystemColors.HighlightTextBrush;
                }

                ScrollIntoView();

                OnSelectionChanged(EventArgs.Empty);
            }
        }

        public object SelectedItem
        {
            get { return SelectedIndex > -1 ? list[SelectedIndex] : null; }
            set { SelectedIndex = list.IndexOf(value); }
        }

        public Lister()
        {
            Focusable = false;

            var bord = new Border();
            bord.BorderThickness = new Thickness(1);
            bord.BorderBrush = SystemColors.ActiveBorderBrush;
            bord.Background = SystemColors.WindowBrush;
            Content = bord;

            scroll = new ScrollViewer();
            scroll.Focusable = false;
            scroll.Padding = new Thickness(2, 0, 0, 0);
            bord.Child = scroll;

            stack = new StackPanel();
            scroll.Content = stack;

            AddHandler(TextBlock.MouseLeftButtonDownEvent, new MouseButtonEventHandler(TextBlockOnMouseLeftButtonDown));
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ScrollIntoView();
        }

        public void Add(object obj)
        {
            list.Add(obj);
            var txtblk = new TextBlock();
            txtblk.Text = obj.ToString();
            stack.Children.Add(txtblk);
        }

        public void Insert(int index, object obj)
        {
            list.Insert(index, obj);
            var txtblk = new TextBlock();
            txtblk.Text = obj.ToString();
            stack.Children.Insert(index, txtblk);
        }

        public void Clear()
        {
            SelectedIndex = -1;
            stack.Children.Clear();
            list.Clear();
        }

        public bool Contains(object obj)
        {
            return list.Contains(obj);
        }

        public void GotToLetter(char ch)
        {
            int offset = SelectedIndex + 1;

            for (var i = 0; i < Count; i++)
            {
                var index = (i + offset) % Count;

                if (char.ToUpper(ch) == char.ToUpper(list[index].ToString()[0]))
                {
                    SelectedIndex = index;
                    break;
                }
            }
        }

        public void PageUp()
        {
            if (SelectedIndex == -1 || Count == 0)
                return;

            var index = SelectedIndex - (int)(Count * scroll.ViewportHeight / scroll.ExtentHeight);

            SelectedIndex = index < 0 ? 0 : index;
        }

        public void PageDown()
        {
            if (SelectedIndex == -1 || Count == 0)
                return;

            var index = SelectedIndex + (int)(Count * scroll.ViewportHeight / scroll.ExtentHeight);

            SelectedIndex = index == (Count - 1) ? Count - 1 : index;
        }

        private void ScrollIntoView()
        {
            if (Count == 0 || SelectedIndex == -1 || scroll.ViewportHeight > scroll.ExtentHeight)
                return;

            var heightPerItem = scroll.ExtentHeight / Count;
            var offsetItemTop = SelectedIndex * heightPerItem;
            var offsetItemBot = offsetItemTop + heightPerItem;

            if (offsetItemTop < scroll.VerticalOffset)
                scroll.ScrollToVerticalOffset(offsetItemTop);
            else if (offsetItemBot > scroll.VerticalOffset + scroll.ViewportHeight)
                scroll.ScrollToVerticalOffset(offsetItemBot - scroll.ViewportHeight);
        }

        private void TextBlockOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is TextBlock txtblk)
                SelectedIndex = stack.Children.IndexOf(txtblk);
        }

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
        }
    }
}
