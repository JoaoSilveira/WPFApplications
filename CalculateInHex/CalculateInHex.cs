using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CalculateInHex
{
    class CalculateInHex : Window
    {
        RoundedButton btnDisplay;
        ulong numDisplay;
        ulong numFirst;
        bool newMember = true;
        char chOperation = '=';

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CalculateInHex());
        }

        public CalculateInHex()
        {
            Title = "Calculate in Hex";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;

            var grid = new Grid();
            grid.Margin = new Thickness(4);
            Content = grid;

            for (int i = 0; i < 5; i++)
            {
                var col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < 7; i++)
            {
                var row = new RowDefinition();
                row.Height = GridLength.Auto;
                grid.RowDefinitions.Add(row);
            }

            string[] arrButtons = { "0", "D", "E", "F", "+", "&", "A", "B", "C", "-", "|", "7", "8", "9", "*", "^", "4", "5", "6", "/", "<<", "1", "2", "3", "%", ">>", "0", "Back", "Equals" };

            int irow = 0;
            int icol = 0;
            foreach (var str in arrButtons)
            {
                var btn = new RoundedButton();
                btn.Focusable = false;
                btn.Height = 32;
                btn.Margin = new Thickness(4);
                btn.Click += ButtonOnClick;

                var txt = new TextBlock();
                txt.Text = str;
                btn.Child = txt;

                grid.Children.Add(btn);
                Grid.SetRow(btn, irow);
                Grid.SetColumn(btn, icol);

                if (irow == 0 && icol == 0)
                {
                    btnDisplay = btn;
                    btn.Margin = new Thickness(4, 4, 4, 6);
                    Grid.SetColumnSpan(btn, 5);
                    irow = 1;
                }
                else if (irow == 6 && icol > 0)
                {
                    Grid.SetColumnSpan(btn, 2);
                    icol += 2;
                }
                else
                {
                    btn.Width = 32;
                    if ((icol = (icol + 1) % 5) == 0)
                        irow++;
                }
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as RoundedButton;

            if (btn == null)
                return;

            var strButton = (btn.Child as TextBlock).Text;
            char chButton = strButton[0];

            if (strButton == "Equals")
                chButton = '=';

            if (btn == btnDisplay)
                numDisplay = 0;
            else if (strButton == "Back")
                numDisplay >>= 4;
            else if (char.IsLetterOrDigit(chButton))
            {
                if (newMember)
                {
                    numFirst = numDisplay;
                    numDisplay = 0;
                    newMember = false;
                }
                if (numDisplay <= ulong.MaxValue >> 4)
                    numDisplay = (numDisplay << 4) + (ulong)(chButton - (char.IsDigit(chButton) ? '0' : 'A' - 10));
            }
            else
            {
                if (!newMember)
                {
                    switch (chOperation)
                    {
                        case '=':
                            break;
                        case '+':
                            numDisplay = numFirst + numDisplay;
                            break;
                        case '-':
                            numDisplay = numFirst - numDisplay;
                            break;
                        case '*':
                            numDisplay = numFirst * numDisplay;
                            break;
                        case '&':
                            numDisplay = numFirst & numDisplay;
                            break;
                        case '|':
                            numDisplay = numFirst | numDisplay;
                            break;
                        case '^':
                            numDisplay = numFirst ^ numDisplay;
                            break;
                        case '<':
                            numDisplay = numFirst << (int)numDisplay;
                            break;
                        case '>':
                            numDisplay = numFirst >> (int)numDisplay;
                            break;
                        case '/':
                            numDisplay = numDisplay == 0 ? ulong.MaxValue : numFirst / numDisplay;
                            break;
                        case '%':
                            numDisplay = numDisplay == 0 ? ulong.MaxValue : numFirst % numDisplay;
                            break;
                        default:
                            numDisplay = 0;
                            break;
                    }
                }
                newMember = true;
                chOperation = chButton;
            }

            var text = new TextBlock();
            text.Text = $"{numDisplay:X}";
            btnDisplay.Child = text;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            if (e.Text.Length == 0)
                return;

            char key = char.ToUpper(e.Text[0]);

            foreach(UIElement child in (Content as Grid).Children)
            {
                var btn = child as RoundedButton;
                string strButton = (btn.Child as TextBlock).Text;

                if (
                    (key == strButton[0] && btn != btnDisplay && strButton != "Equals" && strButton != "Back")
                    ||
                    (key == '=' && strButton == "Equals")
                    ||
                    (key == '\r' && strButton == "Equals")
                    ||
                    (key == '\b' && strButton == "Back")
                    ||
                    (key == '\x1B' && btn == btnDisplay))
                {
                    var args = new RoutedEventArgs(RoundedButton.ClickEvent, btn);
                    btn.RaiseEvent(args);
                    btn.IsPressed = true;

                    var tmr = new DispatcherTimer();
                    tmr.Interval = TimeSpan.FromMilliseconds(100);
                    tmr.Tag = btn;
                    tmr.Tick += TimerOnTick;
                    tmr.Start();

                    e.Handled = true;
                }
            }
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            var tmr = sender as DispatcherTimer;
            var btn = tmr.Tag as RoundedButton;
            btn.IsPressed = false;

            tmr.Stop();
            tmr.Tick -= TimerOnTick;
        }
    }
}
