using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccessStaticFields
{
    public partial class AccessStaticFields : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new Application();
            app.Run(new AccessStaticFields());
        }

        public AccessStaticFields()
        {
            InitializeComponent();
        }
    }
}
