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

namespace ListNamedBrushes
{
    public class NamedBrush
    {
        public static NamedBrush[] All { get; } = typeof(Brushes).GetProperties().Select(b => new NamedBrush(b.Name, (Brush)b.GetValue(null, null))).ToArray();

        string str;

        public string Name => Regex.Replace(str, "(?<=[a-z])(?=[A-Z])", " ");

        public Brush Brush { get; }

        private NamedBrush(string name, Brush brush)
        {
            str = name;
            Brush = brush;
        }

        public override string ToString() => str;
    }
}
