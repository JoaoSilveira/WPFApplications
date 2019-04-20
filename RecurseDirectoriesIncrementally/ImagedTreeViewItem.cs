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

namespace RecurseDirectoriesIncrementally
{
    public class ImagedTreeViewItem : TreeViewItem
    {
        TextBlock text;
        Image img;
        ImageSource srcSelected;
        ImageSource srcUnselected;

        public string Text
        {
            get { return text.Text; }
            set { text.Text = value; }
        }

        public ImageSource SelectedImage
        {
            get { return srcSelected; }
            set
            {
                srcSelected = value;

                if (IsSelected)
                    img.Source = srcSelected;
            }
        }

        public ImageSource UnselectedImage
        {
            get { return srcUnselected; }
            set
            {
                srcUnselected = value;

                if (!IsSelected)
                    img.Source = srcUnselected;
            }
        }

        public ImagedTreeViewItem()
        {
            var stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Header = stack;

            img = new Image();
            img.VerticalAlignment = VerticalAlignment.Center;
            img.Margin = new Thickness(0, 0, 2, 0);
            stack.Children.Add(img);
            text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(text);
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            img.Source = srcSelected;
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            img.Source = srcUnselected;
        }
    }
}
