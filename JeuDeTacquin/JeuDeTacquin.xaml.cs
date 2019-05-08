using PlayJeuDeTacquin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace JeuDeTacquin
{
    public partial class JeuDeTacquin : Page
    {
        public int NumberRows = 4;
        public int NumberColumns = 4;
        bool isLoaded = false;

        int xEmpty;
        int yEmpty;
        int iCounter;
        Key[] keys = { Key.Left, Key.Right, Key.Up, Key.Down };
        Random rand;
        UIElement elEmptySpare = new Empty();

        public JeuDeTacquin()
        {
            Loaded += PageOnLoaded;
            InitializeComponent();
        }

        private void PageOnLoaded(object sender, RoutedEventArgs e)
        {
            Focus();
            if (isLoaded)
                return;

            Title = $"Jeu de Tacquin ({NumberColumns}\x00D7{NumberRows})";
            unigrid.Rows = NumberRows;
            unigrid.Columns = NumberColumns;

            for (var i = 0; i < NumberRows * NumberColumns - 1; i++)
            {
                var tile = new Tile();
                tile.Text = $"{i + 1}";
                tile.MouseLeftButtonDown += TileOnMouseLeftButtonDown;
                unigrid.Children.Add(tile);
            }

            unigrid.Children.Add(new Empty());
            xEmpty = NumberColumns - 1;
            yEmpty = NumberRows - 1;

            isLoaded = true;
        }

        private void TileOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Focus();

            var tile = sender as Tile;
            var iMove = unigrid.Children.IndexOf(tile);
            var xMove = iMove % NumberColumns;
            var yMove = iMove / NumberColumns;

            if (xMove == xEmpty)
                while (yMove != yEmpty)
                    MoveTile(xMove, yEmpty + (yMove - yEmpty) / Math.Abs(yMove - yEmpty));
            if (yMove == yEmpty)
                while (xMove != xEmpty)
                    MoveTile(xEmpty + (xMove - xEmpty) / Math.Abs(xMove - xEmpty), yMove);

            e.Handled = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.Right: MoveTile(xEmpty - 1, yEmpty); break;
                case Key.Left: MoveTile(xEmpty + 1, yEmpty); break;
                case Key.Up: MoveTile(xEmpty, yEmpty + 1); break;
                case Key.Down: MoveTile(xEmpty, yEmpty - 1); break;
                default: return;
            }
            e.Handled = true;
        }

        void ScrambleOnClick(object sender, RoutedEventArgs e)
        {
            rand = new Random();
            iCounter = 16 * NumberColumns * NumberRows;

            var tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromMilliseconds(10);
            tmr.Tick += TimerOnTick;
            tmr.Start();
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            for (var i = 0; i < 5; i++)
            {
                MoveTile(xEmpty, yEmpty + rand.Next(3) - 1);
                MoveTile(xEmpty + rand.Next(3) - 1, yEmpty);
            }

            if (iCounter-- == 0)
                (sender as DispatcherTimer).Stop();
        }

        void MoveTile(int xTile, int yTile)
        {
            if ((xTile == xEmpty && yTile == yEmpty) ||
                xTile < 0 || xTile >= NumberColumns ||
                yTile < 0 || yTile >= NumberRows)
                return;

            var iTile = yTile * NumberColumns + xTile;
            var iEmpty = yEmpty * NumberColumns + xEmpty;

            var elTile = unigrid.Children[iTile];
            var elEmpty = unigrid.Children[iEmpty];

            unigrid.Children.RemoveAt(iTile);
            unigrid.Children.Insert(iTile, elEmptySpare);

            unigrid.Children.RemoveAt(iEmpty);
            unigrid.Children.Insert(iEmpty, elTile);

            xEmpty = xTile;
            yEmpty = yTile;
            elEmptySpare = elEmpty;
        }

        void NextOnClick(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoForward)
                NavigationService.GoForward();
            else
            {
                var page = new JeuDeTacquin();
                page.NumberRows = NumberRows + 1;
                page.NumberColumns = NumberColumns + 1;

                NavigationService.Navigate(page);
            }
        }
    }
}
