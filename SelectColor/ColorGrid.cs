using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SelectColor
{
    class ColorGrid : Control
    {
        const int yNum = 5;
        const int xNum = 8;

        string[,] colors = new string[yNum, xNum]
        {
            { "Black", "Brown", "DarkGreen", "MidnightBlue", "Navy", "DarkBlue", "Indigo", "DimGray" },
            { "DarkRed", "OrangeRed", "Olive", "Green", "Teal", "Blue", "SlateGray", "Gray" },
            { "Red", "Orange", "YellowGreen", "SeaGreen", "Aqua", "LightBlue", "Violet", "DarkGray" },
            { "Pink", "Gold", "Yellow", "Lime", "Turquoise", "SkyBlue", "Plum", "LightGray" },
            { "LightPink", "Tan", "LightYellow", "LightGreen", "LightCyan", "LightSkyBlue", "Lavender", "White" }
        };

        ColorCell[,] cells = new ColorCell[yNum, xNum];
        ColorCell cellSelected;
        ColorCell cellHighlighted;

        Border bord;
        UniformGrid unigrid;

        Color clrSelected = Colors.Black;

        public event EventHandler SelectedColorChanged;

        protected override int VisualChildrenCount => 1;

        public Color SelectedColor => clrSelected;

        public ColorGrid()
        {
            bord = new Border();
            bord.BorderBrush = SystemColors.ControlDarkBrush;
            bord.BorderThickness = new Thickness(1);
            AddVisualChild(bord);
            AddLogicalChild(bord);

            unigrid = new UniformGrid();
            unigrid.Background = SystemColors.WindowBrush;
            unigrid.Columns = xNum;
            bord.Child = unigrid;

            for (var y = 0; y < yNum; y++)
            {
                for (var x = 0; x < xNum; x++)
                {
                    var clr = (Color)typeof(Colors).GetProperty(colors[y, x]).GetValue(null, null);
                    cells[y, x] = new ColorCell(clr);
                    unigrid.Children.Add(cells[y, x]);

                    if (clr == clrSelected)
                    {
                        cellSelected = cells[y, x];
                        cellSelected.IsSelected = true;
                    }

                    var tip = new ToolTip();
                    tip.Content = colors[y, x];
                    cells[y, x].ToolTip = tip;
                }
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException();

            return bord;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            bord.Measure(constraint);
            return bord.DesiredSize;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            bord.Arrange(new Rect(new Point(), arrangeBounds));
            return arrangeBounds;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            if (cellHighlighted != null)
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = null;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var cell = e.Source as ColorCell;

            if (cell != null)
            {
                if (cellHighlighted != null)
                    cellHighlighted.IsHighlighted = false;

                cellHighlighted = cell;
                cellHighlighted.IsHighlighted = true;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (cellHighlighted != null)
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = null;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            var cell = e.Source as ColorCell;

            if (cell != null)
            {
                if (cellHighlighted != null)
                    cellHighlighted.IsSelected = false;

                cellHighlighted = cell;
                cellHighlighted.IsSelected = true;
            }
            Focus();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            var cell = e.Source as ColorCell;

            if (cell != null)
            {
                if (cellSelected != null)
                    cellSelected.IsSelected = false;

                cellSelected = cell;
                cellSelected.IsSelected = true;

                clrSelected = (cellSelected.Brush as SolidColorBrush).Color;
                OnSelectedColorChanged(EventArgs.Empty);
            }
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);

            if (cellHighlighted == null)
            {
                if (cellSelected != null)
                    cellHighlighted = cellSelected;
                else
                    cellHighlighted = cells[0, 0];

                cellHighlighted.IsHighlighted = true;
            }
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);

            if (cellHighlighted != null)
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = null;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            var index = unigrid.Children.IndexOf(cellHighlighted);
            int y = index / xNum;
            int x = index % xNum;

            switch (e.Key)
            {
                case Key.Home:
                    y = 0;
                    x = 0;
                    break;
                case Key.End:
                    y = yNum - 1;
                    x = xNum + 1;
                    break;
                case Key.Down:
                    if ((y = (y + 1) % yNum) == 0)
                        x++;
                    break;
                case Key.Up:
                    if ((y = (y + yNum - 1) % yNum) == yNum - 1)
                        x--;
                    break;
                case Key.Right:
                    if ((x = (x + 1) % xNum) == 0)
                        y++;
                    break;
                case Key.Left:
                    if ((x = (x + xNum - 1) % xNum) == xNum - 1)
                        y--;
                    break;
                case Key.Enter:
                case Key.Space:
                    if (cellSelected != null)
                        cellSelected.IsSelected = false;

                    cellSelected = cellHighlighted;
                    cellSelected.IsSelected = true;
                    clrSelected = (cellSelected.Brush as SolidColorBrush).Color;
                    OnSelectedColorChanged(EventArgs.Empty);
                    break;
                default:
                    return;
            }

            if (x >= xNum || y >= yNum)
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            else if (x < 0 || y < 0)
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            else
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = cells[y, x];
                cellHighlighted.IsHighlighted = true;
            }

            e.Handled = true;
        }

        protected virtual void OnSelectedColorChanged(EventArgs args)
        {
            SelectedColorChanged?.Invoke(this, args);
        }
    }
}