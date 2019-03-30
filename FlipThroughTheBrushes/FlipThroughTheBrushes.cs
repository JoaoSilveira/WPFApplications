using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FlipThroughTheBrushes
{
    class FlipThroughTheBrushes : Window
    {
        int index;
        PropertyInfo[] properties;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new FlipThroughTheBrushes());
        }

        public FlipThroughTheBrushes()
        {
            index = 0;
            properties = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static);

            SetTitleAndBackground();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Down || e.Key == Key.Up)
            {
                index += e.Key == Key.Up ? 1 : properties.Length - 1;
                index %= properties.Length;
                SetTitleAndBackground();
            }
            base.OnKeyDown(e);
        }

        void SetTitleAndBackground()
        {
            Title = "Flip Through the Brushes - " + properties[index].Name;
            Background = (Brush)properties[index].GetValue(null, null);
        }
    }
}
