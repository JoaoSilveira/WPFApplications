using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ListColorsElegantly
{
    public class ColorListBox : ListBox
    {
        public ColorListBox()
        {
            foreach (var prop in typeof(Colors).GetProperties())
            {
                var item = new ColorListBoxItem();
                item.Text = prop.Name;
                item.Color = (Color)prop.GetValue(null, null);
                Items.Add(item);
            }
            SelectedValuePath = "Color";
        }

        public Color SelectedColor
        {
            get { return (Color)SelectedValue; }
            set { SelectedValue = value; }
        }
    }
}
