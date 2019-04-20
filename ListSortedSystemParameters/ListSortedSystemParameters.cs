using ListSystemParameters;
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

namespace ListSortedSystemParameters
{
    public class ListSortedSystemParameters : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListSortedSystemParameters());
        }

        public ListSortedSystemParameters()
        {
            Title = "List Sorted System Parameters";

            var lstvue = new ListView();
            Content = lstvue;

            var grdvue = new GridView();
            lstvue.View = grdvue;

            var col = new GridViewColumn();
            col.Header = "Property Name";
            col.Width = 200;
            col.DisplayMemberBinding = new Binding("Name");
            grdvue.Columns.Add(col);

            col = new GridViewColumn();
            col.Header = "Value";
            col.Width = 200;
            grdvue.Columns.Add(col);

            var template = new DataTemplate(typeof(string));
            var factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            factoryTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Value"));
            template.VisualTree = factoryTextBlock;
            col.CellTemplate = template;

            var props = typeof(SystemParameters).GetProperties();
            var sortlist = new SortedList<string, SystemParam>();

            foreach (var prop in props)
                if (prop.PropertyType != typeof(ResourceKey))
                {
                    var sysparam = new SystemParam();
                    sysparam.Name = prop.Name;
                    sysparam.Value = prop.GetValue(null, null);
                    sortlist.Add(prop.Name, sysparam);
                }

            lstvue.ItemsSource = sortlist.Values;
        }
    }
}
