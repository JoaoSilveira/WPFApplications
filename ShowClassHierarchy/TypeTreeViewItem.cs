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

namespace ShowClassHierarchy
{
    public class TypeTreeViewItem : TreeViewItem
    {
        private Type typ;

        public Type Type
        {
            get { return typ; }
            set
            {
                typ = value;

                if (typ.IsAbstract)
                    Header = $"{typ.Name} (abstract)";
                else
                    Header = typ.Name;
            }
        }

        public TypeTreeViewItem()
        {

        }

        public TypeTreeViewItem(Type typ)
        {
            Type = typ;
        }
    }
}
