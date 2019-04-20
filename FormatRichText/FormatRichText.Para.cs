using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FormatRichText
{
    public partial class FormatRichText : Window
    {
        ToggleButton[] btnAlignment = new ToggleButton[4];

        void AddParaToolBar(ToolBarTray tray, int band, int index)
        {
            var toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            toolbar.Items.Add(btnAlignment[0] = CreateButton(TextAlignment.Left, "Align Left", 0, 4));
            toolbar.Items.Add(btnAlignment[1] = CreateButton(TextAlignment.Center, "Center", 2, 2));
            toolbar.Items.Add(btnAlignment[2] = CreateButton(TextAlignment.Right, "Align Right", 4, 0));
            toolbar.Items.Add(btnAlignment[3] = CreateButton(TextAlignment.Justify, "Justify", 0, 0));

            txtbox.SelectionChanged += TextBoxOnSelectionChanged2;
        }

        ToggleButton CreateButton(TextAlignment align, string tooltip, int offsetLeft, int offsetRight)
        {
            var btn = new ToggleButton();
            btn.Tag = align;
            btn.Click += ButtonOnClick;

            var canv = new Canvas();
            canv.Width = 16;
            canv.Height = 16;
            btn.Content = canv;

            for (var i = 0; i < 5; i++)
            {
                var poly = new Polyline();
                poly.Stroke = SystemColors.WindowTextBrush;
                poly.StrokeThickness = 1;

                if ((i & 1) == 0)
                    poly.Points = new PointCollection(new[]
                    {
                        new Point(2, 2 + 3 * i),
                        new Point(14, 2 + 3 * i)
                    });
                else
                    poly.Points = new PointCollection(new[]
                    {
                        new Point(2 + offsetLeft, 2 + 3 * i),
                        new Point(14 - offsetRight, 2 + 3 * i)
                    });

                canv.Children.Add(poly);
            }

            var tip = new ToolTip();
            tip.Content = tooltip;
            btn.ToolTip = tip;

            return btn;
        }

        private void TextBoxOnSelectionChanged2(object sender, RoutedEventArgs e)
        {
            var obj = txtbox.Selection.GetPropertyValue(Block.TextAlignmentProperty);

            if (obj is TextAlignment align)
                foreach (var btn in btnAlignment)
                    btn.IsChecked = (TextAlignment)btn.Tag == align;
            else
                foreach (var btn in btnAlignment)
                    btn.IsChecked = false;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as ToggleButton;

            foreach (var btnAlign in btnAlignment)
                btnAlign.IsChecked = btnAlign == btn;

            txtbox.Selection.ApplyPropertyValue(Block.TextAlignmentProperty, btn.Tag);
        }
    }
}
