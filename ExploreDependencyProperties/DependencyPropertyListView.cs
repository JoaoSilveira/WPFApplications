using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ExploreDependencyProperties
{
    public class DependencyPropertyListView : ListView
    {
        public static readonly DependencyProperty TypeProperty;

        static DependencyPropertyListView()
        {
            TypeProperty = DependencyProperty.Register(nameof(Type), typeof(Type), typeof(DependencyPropertyListView), new PropertyMetadata(null, new PropertyChangedCallback(OnTypePropertyChanged)));
        }

        private static void OnTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var lstvue = d as DependencyPropertyListView;
            var type = e.NewValue as Type;

            lstvue.ItemsSource = null;

            if (type != null)
            {
                var list = new SortedList<string, DependencyProperty>();

                foreach (var prop in type.GetFields())
                    if (prop.FieldType == typeof(DependencyProperty))
                        list.Add(prop.Name, (DependencyProperty)prop.GetValue(null));

                lstvue.ItemsSource = list.Values;
            }
        }

        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public DependencyPropertyListView()
        {
            var grdvue = new GridView();
            View = grdvue;

            var col = new GridViewColumn();
            col.Header = "Name";
            col.Width = 150;
            col.DisplayMemberBinding = new Binding("Name");
            grdvue.Columns.Add(col);

            col = new GridViewColumn();
            col.Header = "Ownew";
            col.Width = 100;
            grdvue.Columns.Add(col);

            var template = new DataTemplate();
            col.CellTemplate = template;

            var elTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            template.VisualTree = elTextBlock;

            var bind = new Binding("OwnerType");
            bind.Converter = new TypeToString();
            elTextBlock.SetBinding(TextBlock.TextProperty, bind);

            col = new GridViewColumn();
            col.Header = "Type";
            col.Width = 100;
            grdvue.Columns.Add(col);

            template = new DataTemplate();
            col.CellTemplate = template;

            elTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            template.VisualTree = elTextBlock;

            bind = new Binding("PropertyType");
            bind.Converter = new TypeToString();
            elTextBlock.SetBinding(TextBlock.TextProperty, bind);

            col = new GridViewColumn();
            col.Header = "Default";
            col.Width = 75;
            col.DisplayMemberBinding = new Binding("DefaultMetadata.DefaultValue");
            grdvue.Columns.Add(col);

            col = new GridViewColumn();
            col.Header = "Read-Only";
            col.Width = 75;
            col.DisplayMemberBinding = new Binding("DefaultMetadata.ReadOnly");
            grdvue.Columns.Add(col);

            col = new GridViewColumn();
            col.Header = "Usage";
            col.Width = 75;
            col.DisplayMemberBinding = new Binding("DefaultMetadata.AttachedPropertyUsage");
            grdvue.Columns.Add(col);

            col = new GridViewColumn();
            col.Header = "Flags";
            col.Width = 250;
            grdvue.Columns.Add(col);

            template = new DataTemplate();
            col.CellTemplate = template;

            elTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            template.VisualTree = elTextBlock;

            bind = new Binding("DefaultMetadata");
            bind.Converter = new MetadataToFlags();
            elTextBlock.SetBinding(TextBlock.TextProperty, bind);
        }
    }
}
