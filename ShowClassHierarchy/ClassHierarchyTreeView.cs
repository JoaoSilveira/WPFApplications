using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShowClassHierarchy
{
    public class ClassHierarchyTreeView : TreeView
    {
        public ClassHierarchyTreeView(Type typeRoot)
        {
            var dummy = new UIElement();

            var assemblies = new List<Assembly>();

            AssemblyName[] anames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

            foreach (var aname in anames)
                assemblies.Add(Assembly.Load(aname));

            var classes = new SortedList<string, Type>();
            classes.Add(typeRoot.Name, typeRoot);

            foreach (var assembly in assemblies)
                foreach (var type in assembly.GetTypes())
                    if (type.IsPublic && type.IsSubclassOf(typeRoot))
                        classes.Add(type.Name, type);

            var item = new TypeTreeViewItem(typeRoot);
            Items.Add(item);

            CreateLinkedItems(item, classes);
        }

        private void CreateLinkedItems(TypeTreeViewItem itemBase, SortedList<string, Type> list)
        {
            foreach (var kvp in list)
                if (kvp.Value.BaseType == itemBase.Type)
                {
                    var item = new TypeTreeViewItem(kvp.Value);
                    itemBase.Items.Add(item);
                    CreateLinkedItems(item, list);
                }
        }
    }
}
