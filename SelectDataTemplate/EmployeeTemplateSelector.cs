using ContentTemplateDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SelectDataTemplate
{
    public class EmployeeTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var emp = item as Employee;
            var el = container as FrameworkElement;

            return (DataTemplate)el.FindResource(emp.LeftHanded ? "templateLeft" : "templateRight");
        }
    }
}
