using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace YellowPad
{
    public partial class YellowPadWindow : Window
    {
        void EditOnOpened(object sender, RoutedEventArgs e) => itemFormat.IsEnabled = inkcanv.GetSelectedStrokes().Count > 0;

        void CutCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = inkcanv.GetSelectedStrokes().Count > 0;

        void CutOnExecuted(object sender, ExecutedRoutedEventArgs e) => inkcanv.CutSelection();

        void CopyOnExecuted(object sender, ExecutedRoutedEventArgs e) => inkcanv.CopySelection();

        void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = inkcanv.CanPaste();

        void PasteOnExecuted(object sender, ExecutedRoutedEventArgs e) => inkcanv.Paste();

        void DeleteOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (var strk in inkcanv.GetSelectedStrokes())
                inkcanv.Strokes.Remove(strk);
        }

        void SelectAllOnExecuted(object sender, ExecutedRoutedEventArgs e) => inkcanv.Select(inkcanv.Strokes);

        void FormatOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new StylusToolDialog();
            dlg.Owner = this;
            dlg.Title = "Format Selection";

            var strokes = inkcanv.GetSelectedStrokes();

            dlg.DrawingAttributes = strokes.Count > 0 ? strokes[0].DrawingAttributes : inkcanv.DefaultDrawingAttributes;

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            foreach (var strk in strokes)
                strk.DrawingAttributes = dlg.DrawingAttributes;
        }
    }
}
