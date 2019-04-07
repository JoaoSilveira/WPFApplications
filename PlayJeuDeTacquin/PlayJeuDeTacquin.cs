using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PlayJeuDeTacquin
{
    class PlayJeuDeTacquin : Window
    {
        const int NumberRows = 4;
        const int NumberCols = 4;

        UniformGrid unigrid;
        int xEmpty;
        int yEmpty;
        int counter;
        Key[] keys = { Key.Left, Key.Right, Key.Up, Key.Down };
        Random rand;
        UIElement emptySpare = new Empty();

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new PlayJeuDeTacquin());
        }

        public PlayJeuDeTacquin()
        {
            Title = "Jeu De Tacquin";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            Background = SystemColors.ControlBrush;

            var stack = new StackPanel();
            Content = stack;

            var btn = new Button();
            btn.Content = "_Scramble";
            btn.Margin = new Thickness(10);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Click += ScrambleOnClick;
            stack.Children.Add(btn);

            var bord = new Border();
            bord.BorderBrush = SystemColors.ControlDarkBrush;
            bord.BorderThickness = new Thickness(1);
            stack.Children.Add(bord);

            unigrid = new UniformGrid();
            unigrid.Rows = NumberRows;
            unigrid.Columns = NumberCols;
            bord.Child = unigrid;

            for (int i = 0; i < NumberRows * NumberCols - 1; i++)
            {
                var tile = new Tile();
                tile.Text = (i + 1).ToString();
                tile.MouseLeftButtonDown += TileOnMouseLeftButtonDown;
                unigrid.Children.Add(tile);
            }

            unigrid.Children.Add(new Empty());
            xEmpty = NumberRows - 1;
            yEmpty = NumberCols - 1;
        }

        private void TileOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tile = sender as Tile;

            var move = unigrid.Children.IndexOf(tile);
            var xMove = move % NumberCols;
            var yMove = move / NumberCols;

            if (xMove == xEmpty)
            {
                while (yMove != yEmpty)
                {
                    MoveTile(xMove, yEmpty + (yMove - yEmpty) / Math.Abs(yMove - yEmpty));
                }
            }

            if (yMove == yEmpty)
            {
                while (xMove != xEmpty)
                {
                    MoveTile(xEmpty + (xMove - xEmpty) / Math.Abs(xMove - xEmpty), yMove);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.Right:
                    MoveTile(xEmpty - 1, yEmpty);
                    break;
                case Key.Left:
                    MoveTile(xEmpty + 1, yEmpty);
                    break;
                case Key.Down:
                    MoveTile(xEmpty, yEmpty - 1);
                    break;
                case Key.Up:
                    MoveTile(xEmpty, yEmpty + 1);
                    break;
            }
        }

        private void ScrambleOnClick(object sender, RoutedEventArgs e)
        {
            rand = new Random();
            counter = 16 * NumberCols * NumberRows;

            var tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromMilliseconds(10);
            tmr.Tick += TimerOnTick;
            tmr.Start();
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                MoveTile(xEmpty, yEmpty + rand.Next(3) - 1);
                MoveTile(xEmpty + rand.Next(3) - 1, yEmpty);
            }

            if (0 == counter--)
            {
                (sender as DispatcherTimer).Stop();
            }
        }

        private void MoveTile(int xTile, int yTile)
        {
            if ((xTile == xEmpty && yTile == yEmpty) || xTile < 0 || yTile < 0 || xTile >= NumberCols || yTile >= NumberRows)
            {
                return;
            }

            int tile = NumberCols * yTile + xTile;
            int empty = NumberCols * yEmpty + xEmpty;

            var elTile = unigrid.Children[tile];
            var elEmpty = unigrid.Children[empty];

            unigrid.Children.RemoveAt(tile);
            unigrid.Children.Insert(tile, emptySpare);

            unigrid.Children.RemoveAt(empty);
            unigrid.Children.Insert(empty, elTile);

            xEmpty = xTile;
            yEmpty = yTile;
            emptySpare = elEmpty;
        }
    }
}
