using CircleTheButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SelectColorFromWheel
{
    class ColorWheel : ListBox
    {
        public ColorWheel()
        {
            var factoryRadialPanel = new FrameworkElementFactory(typeof(RadialPanel));
            ItemsPanel = new ItemsPanelTemplate(factoryRadialPanel);

            var template = new DataTemplate(typeof(Brush));
            ItemTemplate = template;

            var elRectangle = new FrameworkElementFactory(typeof(Rectangle));
            elRectangle.SetValue(WidthProperty, 4d);
            elRectangle.SetValue(HeightProperty, 12d);
            elRectangle.SetValue(MarginProperty, new Thickness(1, 8, 1, 8));
            elRectangle.SetBinding(Shape.FillProperty, new Binding(""));

            template.VisualTree = elRectangle;

            foreach (var prop in typeof(Brushes).GetProperties())
            {
                Items.Add((Brush)prop.GetValue(null, null));
            }
        }
    }
}
