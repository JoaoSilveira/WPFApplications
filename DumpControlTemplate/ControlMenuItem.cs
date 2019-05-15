using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

// if the namespace is the same name as the window (DumpControlTemplate) the code won't compile, not sure why
namespace DumpControlTemplateMenuItem
{
    public class ControlMenuItem : MenuItem
    {
        public ControlMenuItem()
        {
            var asbly = Assembly.GetAssembly(typeof(Control));
            var types = asbly.GetTypes();
            var sortlst = new SortedList<string, MenuItem>();

            Header = "Control";
            Tag = typeof(Control);
            sortlst.Add("Control", this);

            foreach (var type in types)
            {
                if (!type.IsPublic || !type.IsSubclassOf(typeof(Control)))
                    continue;

                var item = new MenuItem();
                item.Header = type.Name;
                item.Tag = type;
                sortlst.Add(type.Name, item);
            }

            foreach (var kvp in sortlst)
            {
                if (kvp.Key == "Control")
                    continue;

                var strParent = ((Type)kvp.Value.Tag).BaseType.Name;
                var itemParent = sortlst[strParent];
                itemParent.Items.Add(kvp.Value);
            }

            foreach (var kvp in sortlst)
            {
                var type = (Type)kvp.Value.Tag;

                if (type.IsAbstract && kvp.Value.Items.Count == 0)
                    kvp.Value.IsEnabled = false;

                if (type.IsAbstract || kvp.Value.Items.Count == 0)
                    continue;

                var item = new MenuItem();
                item.Header = kvp.Value.Header as string;
                item.Tag = type;
                kvp.Value.Items.Insert(0, item);
            }
        }
    }
}
