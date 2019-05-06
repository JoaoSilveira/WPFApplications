using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace YellowPad
{
    public partial class YellowPadWindow : Window
    {
        void NewOnExecuted(object sender, ExecutedRoutedEventArgs e) => inkcanv.Strokes.Clear();

        void OpenOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = "Ink Serialized Format(*.isf)|*.isf|All Files (*.*)|*.*";

            if (!dlg.ShowDialog(this).GetValueOrDefault())
                return;

            try
            {
                using (var file = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                {
                    inkcanv.Strokes = new StrokeCollection(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title);
            }
        }

        void SaveOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "Ink Serialized Format(*.isf)|*.isf|XAML Drawing File (*.xaml)|*.xaml|All Files (*.*)|*.*";

            if (!dlg.ShowDialog(this).GetValueOrDefault())
                return;

            try
            {
                using (var file = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
                {
                    if (dlg.FilterIndex == 1 || dlg.FilterIndex == 3)
                        inkcanv.Strokes.Save(file);
                    else
                    {
                        var drawgrp = new DrawingGroup();

                        foreach (var strk in inkcanv.Strokes)
                        {
                            var clr = strk.DrawingAttributes.Color;

                            if (strk.DrawingAttributes.IsHighlighter)
                                clr = Color.FromArgb(128, clr.R, clr.G, clr.B);

                            drawgrp.Children.Add(new GeometryDrawing(new SolidColorBrush(clr), null, strk.GetGeometry()));
                        }

                        XamlWriter.Save(drawgrp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title);
            }
        }

        void CloseOnExecuted(object sender, ExecutedRoutedEventArgs e) => Close();
    }
}
