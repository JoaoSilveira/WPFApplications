using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SetSpaceProperty
{
    class SpaceButton : Button
    {
        public static readonly DependencyProperty SpaceProperty;
        string _text;

        public string Text
        {
            set { _text = value; Content = SpaceOutText(_text); }
            get { return _text; }
        }

        public int Space
        {
            set { SetValue(SpaceProperty, value); }
            get { return (int)GetValue(SpaceProperty); }
        }

        static SpaceButton()
        {
            var metadata = new FrameworkPropertyMetadata();
            metadata.DefaultValue = 1;
            metadata.AffectsMeasure = true;
            metadata.Inherits = true;
            metadata.PropertyChangedCallback += OnSpacePropertyChanged;
            SpaceProperty = DependencyProperty.Register(nameof(Space), typeof(int), typeof(SpaceButton), metadata, ValidateSpaceValue);
        }

        private static bool ValidateSpaceValue(object value) => (int)value >= 0;

        private static void OnSpacePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var btn = d as SpaceButton;
            btn.Content = btn.SpaceOutText(btn._text);
        }

        private object SpaceOutText(string text)
        {
            var sb = new StringBuilder();
            text = text ?? string.Empty;

            return string.Join(new string(' ', Space), text.ToCharArray());
        }
    }
}
