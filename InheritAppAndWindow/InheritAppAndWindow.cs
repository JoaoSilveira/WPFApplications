using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritAppAndWindow
{
    class InheritAppAndWindow
    {
        [STAThread]
        public static void Main()
        {
            var app = new MyApplication();

            app.Run();
        }
    }
}
