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
using System.Windows.Shapes;

namespace SelectColorFromGrid
{
    public class ColorGridBox : ListBox
    {
        string[] strColors = 
        {
            "Black", "Brown", "DarkGreen", "MidnightBlue", "Navy",
            "DarkBlue", "Indigo", "DimGray", "DarkRed", "OrangeRed",
            "Olive", "Green", "Teal", "Blue", "SlateGray", "Gray",
            "Red", "Orange", "YellowGreen", "SeaGreen", "Aqua",
            "LightBlue", "Violet", "DarkGray", "Pink", "Gold", "Yellow",
            "Lime", "Turquoise", "SkyBlue", "Plum", "LightGray",
            "LightPink", "Tan", "LightYellow", "LightGreen", "LightCyan",
            "LightSkyBlue", "Lavender", "White"
        };

        public ColorGridBox()
        {
            var factoryUnigrid = new FrameworkElementFactory(typeof(UniformGrid));
            factoryUnigrid.SetValue(UniformGrid.ColumnsProperty, 8);
            ItemsPanel = new ItemsPanelTemplate(factoryUnigrid);

            foreach (var strColor in strColors)
            {
                var rect = new Rectangle();
                rect.Width = 12;
                rect.Height = 12;
                rect.Margin = new Thickness(4);
                rect.Fill = (Brush)typeof(Brushes).GetProperty(strColor).GetValue(null, null);
                Items.Add(rect);

                var tip = new ToolTip();
                tip.Content = strColor;
                rect.ToolTip = tip;
            }

            SelectedValuePath = "Fill";
        }
    }
}
