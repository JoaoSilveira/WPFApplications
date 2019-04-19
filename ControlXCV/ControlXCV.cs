using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ControlXCV
{
    class ControlXCV : CutCopyAndPaste.CutCopyAndPaste
    {
        KeyGesture gestCut = new KeyGesture(Key.X, ModifierKeys.Control);
        KeyGesture gestCopy = new KeyGesture(Key.C, ModifierKeys.Control);
        KeyGesture gestPaste = new KeyGesture(Key.V, ModifierKeys.Control);
        KeyGesture gestDelete = new KeyGesture(Key.Delete);

        [STAThread]
        public new static void Main()
        {
            var app = new Application();
            app.Run(new ControlXCV());
        }

        public ControlXCV()
        {
            Title = "Control X, C, and V";

            itemCut.InputGestureText = "Ctrl+X";
            itemCopy.InputGestureText = "Ctrl+C";
            itemPaste.InputGestureText = "Ctrl+V";
            itemDelete.InputGestureText = "Delete";
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            e.Handled = true;

            if (gestCut.Matches(null, e))
                CutOnClick(this, e);
            else if (gestCopy.Matches(null, e))
                CopyOnClick(this, e);
            else if (gestPaste.Matches(null, e))
                PasteOnClick(this, e);
            else if (gestDelete.Matches(null, e))
                DeleteOnClick(this, e);
            else
                e.Handled = false;
        }
    }
}
