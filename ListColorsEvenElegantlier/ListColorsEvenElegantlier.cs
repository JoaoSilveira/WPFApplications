using ListNamedBrushes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ListColorsEvenElegantlier
{
    class ListColorsEvenElegantlier : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListColorsEvenElegantlier());
        }

        public ListColorsEvenElegantlier()
        {
            Title = "List Colors Even Elegantlier";

            var template = new DataTemplate(typeof(NamedBrush));

            var factoryStack = new FrameworkElementFactory(typeof(StackPanel));
            factoryStack.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            template.VisualTree = factoryStack;

            var factoryRectangle = new FrameworkElementFactory(typeof(Rectangle));
            factoryRectangle.SetValue(WidthProperty, 16d);
            factoryRectangle.SetValue(HeightProperty, 16d);
            factoryRectangle.SetValue(MarginProperty, new Thickness(2));
            factoryRectangle.SetValue(Shape.StrokeProperty, SystemColors.WindowTextBrush);
            factoryRectangle.SetBinding(Shape.FillProperty, new Binding("Brush"));

            factoryStack.AppendChild(factoryRectangle);

            var factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);
            factoryTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Name"));

            factoryStack.AppendChild(factoryTextBlock);

            var lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            Content = lstbox;

            lstbox.ItemTemplate = template;
            lstbox.ItemsSource = NamedBrush.All;

            lstbox.SelectedValuePath = "Brush";
            lstbox.SetBinding(Selector.SelectedValueProperty, "Background");
            lstbox.DataContext = this;
        }
    }
}
