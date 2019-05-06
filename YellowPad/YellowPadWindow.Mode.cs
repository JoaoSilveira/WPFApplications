using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YellowPad
{
    public partial class YellowPadWindow : Window
    {
        void StylusModeOnOpened(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;

            foreach (MenuItem child in item.Items)
                child.IsChecked = inkcanv.EditingMode == (InkCanvasEditingMode)child.Tag;
        }

        void StylusModeOnClick(object sender, RoutedEventArgs e) => inkcanv.EditingMode = (InkCanvasEditingMode)(sender as MenuItem).Tag;

        void EraserModeOnOpened(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;

            foreach (MenuItem child in item.Items)
                child.IsChecked = inkcanv.EditingModeInverted == (InkCanvasEditingMode)child.Tag;
        }

        void EraserModeOnClick(object sender, RoutedEventArgs e) => inkcanv.EditingModeInverted = (InkCanvasEditingMode)(sender as MenuItem).Tag;
    }
}
