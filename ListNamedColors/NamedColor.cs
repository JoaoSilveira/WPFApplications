using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ListNamedColors
{
    class NamedColor
    {
        string str;

        private NamedColor(string name, Color color)
        {
            Color = color;
            str = name;
        }

        public static NamedColor[] All { get; } = typeof(Colors).GetProperties().Select(clr => new NamedColor(clr.Name, (Color)clr.GetValue(null, null))).ToArray();

        public Color Color { get; }

        public string Name => Regex.Replace(str, "(?<=[a-z])(?=[A-Z])", " ");

        public override string ToString() => str;
    }
}
