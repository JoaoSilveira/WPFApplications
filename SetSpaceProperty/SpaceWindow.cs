using System.Windows;

namespace SetSpaceProperty
{
    class SpaceWindow : Window
    {
        public static readonly DependencyProperty SpaceProperty;

        public int Space
        {
            get { return (int)GetValue(SpaceProperty); }
            set { SetValue(SpaceProperty, value); }
        }

        static SpaceWindow()
        {
            var metadata = new FrameworkPropertyMetadata();
            metadata.Inherits = true;

            SpaceProperty = SpaceButton.SpaceProperty.AddOwner(typeof(SpaceWindow));
            SpaceProperty.OverrideMetadata(typeof(SpaceWindow), metadata);
        }
    }
}
