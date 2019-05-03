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
using System.Windows.Markup;
using System.Windows.Media;

namespace DumpContentPropertyAttributes
{
    public class DumpContentPropertyAttributes
    {
        [STAThread]
        public static void Main()
        {
            var dummy1 = new UIElement();
            var dummy2 = new FrameworkElement();

            var listClass = new SortedList<string, string>();
            var strFormat = "{0,-35}{1}";

            foreach (var asmblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
                foreach (var type in Assembly.Load(asmblyName).GetTypes())
                    foreach (var obj in type.GetCustomAttributes(typeof(ContentPropertyAttribute), true))
                        if (type.IsPublic && obj is ContentPropertyAttribute attr)
                            listClass.Add(type.Name, attr.Name);

            Console.WriteLine(strFormat, "Class", "Content Property");
            Console.WriteLine(strFormat, "-----", "----------------");

            foreach (var strClass in listClass.Keys)
                Console.WriteLine(strFormat, strClass, listClass[strClass]);
        }
    }
}
