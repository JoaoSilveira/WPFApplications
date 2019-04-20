using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace TemplateTheTree
{
    public class TemplateTheTree : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new TemplateTheTree());
        }

        public TemplateTheTree()
        {
            Title = "Template the Tree";

            var treevue = new TreeView();
            Content = treevue;

            var template = new HierarchicalDataTemplate(typeof(DiskDirectory));
            template.ItemsSource = new Binding("Subdirectories");

            var factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Name"));

            template.VisualTree = factoryTextBlock;

            var dir = new DiskDirectory(new DirectoryInfo(Path.GetPathRoot(Environment.SystemDirectory)));

            var item = new TreeViewItem();
            item.Header = dir.Name;
            item.ItemsSource = dir.Subdirectories;
            item.ItemTemplate = template;

            treevue.Items.Add(item);
            item.IsExpanded = true;
        }
    }
}
