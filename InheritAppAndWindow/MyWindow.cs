using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InheritAppAndWindow
{
    class MyWindow : Window
    {
        public MyWindow()
        {
            Title = "Inherit App & Window";
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            MessageBox.Show($"Window clicked with {e.ChangedButton} button at point ({e.GetPosition(this)})", Title);
        }
    }
}
