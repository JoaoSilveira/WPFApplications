using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace DumpControlTemplate
{
    public partial class DumpControlTemplate : Window
    {
        Control ctrl;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DumpControlTemplate());
        }

        public DumpControlTemplate()
        {
            InitializeComponent();
        }

        void ControlItemOnClick(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < grid.Children.Count; i++)
                if (Grid.GetRow(grid.Children[i]) == 0)
                {
                    grid.Children.RemoveAt(i);
                    break;
                }

            txtbox.Clear();

            var item = e.Source as MenuItem;
            var type = (Type)item.Tag;
            var info = type.GetConstructor(Type.EmptyTypes);

            try
            {
                ctrl = (Control)info.Invoke(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title);
                return;
            }

            try
            {
                grid.Children.Add(ctrl);
            }
            catch 
            {
                if (ctrl is Window win)
                    win.Show();
                else
                    return;
            }

            Title = Title.Remove(Title.IndexOf('-')) + $"- {type.Name}";
        }

        void DumpOnOpened(object sender, RoutedEventArgs e)
        {
            itemTemplate.IsEnabled = ctrl != null;
            itemItemsPanel.IsEnabled = ctrl != null && ctrl is ItemsControl;
        }

        void DumpTemplateOnClick(object sender, RoutedEventArgs e)
        {
            if (ctrl != null)
                Dump(ctrl.Template);
        }

        void DumpItemsPanelOnClick(object sender, RoutedEventArgs e)
        {
            if (ctrl is ItemsControl itemCtrl)
                Dump(itemCtrl.ItemsPanel);
        }

        void Dump(FrameworkTemplate template)
        {
            if (template is null)
            {
                txtbox.Text = "No Template";
                return;
            }

            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = new string(' ', 4);
            settings.NewLineOnAttributes = true;

            var strbuild = new StringBuilder();
            using (var xmlwriter = XmlWriter.Create(strbuild, settings))
            {
                try
                {
                    XamlWriter.Save(template, xmlwriter);
                    txtbox.Text = strbuild.ToString();
                }
                catch (Exception ex)
                {
                    txtbox.Text = ex.Message;
                    throw;
                }
            }
        }
    }
}
