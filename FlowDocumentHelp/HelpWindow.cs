using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml;

namespace FlowDocumentHelp
{
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        public HelpWindow(string topic) : this()
        {
            if (!string.IsNullOrEmpty(topic))
                frame.Source = new Uri(topic, UriKind.Relative);
        }

        void TreeViewOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treevue.SelectedValue != null)
                frame.Source = new Uri(treevue.SelectedValue as string, UriKind.Relative);
        }

        void FrameOnNavigated(object sender, NavigationEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Uri?.OriginalString ?? string.Empty))
                FindItemToSelect(treevue, e.Uri.OriginalString);
        }

        bool FindItemToSelect(ItemsControl ctrl, string source)
        {
            foreach (var obj in ctrl.Items)
            {
                var xml = obj as XmlElement;
                var attribute = xml.GetAttribute("Source");
                var item = (TreeViewItem)ctrl.ItemContainerGenerator.ContainerFromItem(obj);

                if (!string.IsNullOrEmpty(attribute) && source.EndsWith(attribute))
                {
                    if (item != null && !item.IsSelected)
                        item.IsSelected = true;

                    return true;
                }

                if (item != null)
                {
                    var isExpanded = item.IsExpanded;
                    item.IsExpanded = true;

                    if (item.HasItems && FindItemToSelect(item, source))
                        return true;

                    item.IsExpanded = isExpanded;
                }
            }

            return false;
        }
    }
}
