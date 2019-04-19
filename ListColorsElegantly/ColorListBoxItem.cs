using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ListColorsElegantly
{
    class ColorListBoxItem : ListBoxItem
    {
        string str;
        Rectangle rect;
        TextBlock text;

        public ColorListBoxItem()
        {
            var stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            rect = new Rectangle();
            rect.Width = 16;
            rect.Height = 16;
            rect.Margin = new Thickness(2);
            rect.Stroke = SystemColors.WindowTextBrush;
            stack.Children.Add(rect);

            text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(text);
        }

        public string Text
        {
            get { return str; }
            set
            {
                str = value;
                text.Text = Regex.Replace(value, "(?<=[a-z])(?=[A-Z])", " ");
            }
        }

        public Color Color
        {
            get { return (rect.Fill as SolidColorBrush)?.Color ?? Colors.Transparent; }
            set { rect.Fill = new SolidColorBrush(value); }
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            text.FontWeight = FontWeights.Bold;
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            text.FontWeight = FontWeights.Regular;
        }

        public override string ToString() => str;
    }
}
