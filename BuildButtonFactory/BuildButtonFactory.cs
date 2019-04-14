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

namespace BuildButtonFactory
{
    class BuildButtonFactory : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new BuildButtonFactory());
        }

        public BuildButtonFactory()
        {
            Title = "Build Button Factory";

            var template = new ControlTemplate(typeof(Button));

            var factoryBorder = new FrameworkElementFactory(typeof(Border));
            factoryBorder.Name = "border";

            factoryBorder.SetValue(Border.BorderBrushProperty, Brushes.Red);
            factoryBorder.SetValue(Border.BorderThicknessProperty, new Thickness(3));
            factoryBorder.SetValue(Border.BackgroundProperty, SystemColors.ControlLightBrush);

            var factoryContent = new FrameworkElementFactory(typeof(ContentPresenter));
            factoryContent.Name = "content";

            factoryContent.SetValue(ContentPresenter.ContentProperty, new TemplateBindingExtension(Button.ContentProperty));
            factoryContent.SetValue(ContentPresenter.MarginProperty, new TemplateBindingExtension(Button.PaddingProperty));

            factoryBorder.AppendChild(factoryContent);

            template.VisualTree = factoryBorder;

            var trig = new Trigger();
            trig.Property = UIElement.IsMouseOverProperty;
            trig.Value = true;

            var set = new Setter();
            set.Property = Border.CornerRadiusProperty;
            set.Value = new CornerRadius(24);
            set.TargetName = "border";

            trig.Setters.Add(set);

            set = new Setter();
            set.Property = Control.FontStyleProperty;
            set.Value = FontStyles.Italic;

            trig.Setters.Add(set);

            template.Triggers.Add(trig);

            trig = new Trigger();
            trig.Property = Button.IsPressedProperty;
            trig.Value = true;

            set = new Setter();
            set.Property = Border.BackgroundProperty;
            set.Value = SystemColors.ControlDarkBrush;
            set.TargetName = "border";

            trig.Setters.Add(set);

            template.Triggers.Add(trig);

            var btn = new Button();
            btn.Template = template;

            btn.Content = "Button with custom template";
            btn.Padding = new Thickness(20);
            btn.FontSize = 48;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Click += ButtonOnClick;

            Content = btn;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked the button", Title);
        }
    }
}
