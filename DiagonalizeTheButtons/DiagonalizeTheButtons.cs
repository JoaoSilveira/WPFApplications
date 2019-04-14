using System;
using System.Windows;
using System.Windows.Controls;

namespace DiagonalizeTheButtons
{
    class DiagonalizeTheButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new DiagonalizeTheButtons());
        }

        public DiagonalizeTheButtons()
        {
            Title = "Diagonalize the Buttons";

            var pnl = new DiagonalPanel();
            Content = pnl;

            var rand = new Random();

            for (var i = 0; i < 5; i++)
            {
                var btn = new Button();
                btn.Content = $"Button Number {i + 1}";
                btn.FontSize += rand.Next(20);
                pnl.Add(btn);
            }
        }
    }
}
