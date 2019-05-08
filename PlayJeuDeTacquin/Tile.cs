using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PlayJeuDeTacquin
{
    public class Tile : Canvas
    {
        const int Size = 64;
        const int Bord = 6;

        TextBlock txtblk;

        public string Text
        {
            get { return txtblk.Text; }
            set { txtblk.Text = value; }
        }

        public Tile()
        {
            Width = Size;
            Height = Size;

            var poly = new Polygon();
            poly.Points = new PointCollection(new[]{
                new Point(0, 0),
                new Point(Size, 0),
                new Point(Size - Bord, Bord),
                new Point(Bord,Bord),
                new Point(Bord, Size - Bord),
                new Point(0, Size)
            });
            poly.Fill = SystemColors.ControlLightLightBrush;
            Children.Add(poly);

            poly = new Polygon();
            poly.Points = new PointCollection(new[]
            {
                new Point(Size, Size),
                new Point(Size, 0),
                new Point(Size-Bord, Bord),
                new Point(Size-Bord, Size-Bord),
                new Point(Bord, Size-Bord),
                new Point(0, Size)
            });
            poly.Fill = SystemColors.ControlDarkBrush;
            Children.Add(poly);

            var border = new Border();
            border.Width = Size - 2 * Bord;
            border.Height = Size - 2 * Bord;
            border.Background = SystemColors.ControlBrush;
            Children.Add(border);
            SetLeft(border, Bord);
            SetTop(border, Bord);

            txtblk = new TextBlock();
            txtblk.FontSize = 32;
            txtblk.Foreground = SystemColors.ControlTextBrush;
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            txtblk.VerticalAlignment = VerticalAlignment.Center;
            border.Child = txtblk;
        }
    }
}
