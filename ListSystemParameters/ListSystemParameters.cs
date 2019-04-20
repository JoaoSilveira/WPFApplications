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

namespace ListSystemParameters
{
    public class ListSystemParameters : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ListSystemParameters());
        }

        public ListSystemParameters()
        {
            Title = "List System Parameters";

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
            col.DisplayMemberBinding = new Binding("Value");
            grdvue.Columns.Add(col);

            foreach (var prop in typeof(SystemParameters).GetProperties())
                if (prop.PropertyType != typeof(ResourceKey))
                {
                    var sysparam = new SystemParam();
                    sysparam.Name = prop.Name;
                    sysparam.Value = prop.GetValue(null, null);
                    lstvue.Items.Add(sysparam);
                }
        }
    }
}
