using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace ExamineRoutedEvents
{
    class ExamineRoutedEvents : Application
    {
        static readonly FontFamily fontFam = new FontFamily("Lucida Console");
        const string format = "{0,-30} {1,-15} {2,-15} {3,-15}";
        StackPanel stackOutput;
        DateTime lastTime;

        [STAThread]
        public static void Main()
        {
            var app = new ExamineRoutedEvents();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var win = new Window();
            win.Title = "Examine Routed Events";

            var grid = new Grid();
            win.Content = grid;

            var rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(100, GridUnitType.Star);
            grid.RowDefinitions.Add(rowdef);

            var btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Padding = new Thickness(24);
            grid.Children.Add(btn);

            var text = new TextBlock();
            text.FontSize = 24;
            text.Text = win.Title;
            btn.Content = text;

            var txtHeadings = new TextBlock();
            txtHeadings.FontFamily = fontFam;
            txtHeadings.Inlines.Add(new Underline(new Run(string.Format(format, "Routed Event", "sender", "Source", "OriginalSource"))));
            grid.Children.Add(txtHeadings);
            Grid.SetRow(txtHeadings, 1);

            var scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 2);

            stackOutput = new StackPanel();
            scroll.Content = stackOutput;

            UIElement[] els = { win, grid, btn, text };

            foreach (var el in els)
            {
                el.PreviewKeyDown += AllPurposeEventHandler;
                el.PreviewKeyUp += AllPurposeEventHandler;
                el.PreviewTextInput += AllPurposeEventHandler;
                el.KeyDown += AllPurposeEventHandler;
                el.KeyUp += AllPurposeEventHandler;
                el.TextInput += AllPurposeEventHandler;

                el.PreviewMouseDown += AllPurposeEventHandler;
                el.PreviewMouseUp += AllPurposeEventHandler;
                el.MouseDown += AllPurposeEventHandler;
                el.MouseUp += AllPurposeEventHandler;

                el.StylusDown += AllPurposeEventHandler;
                el.StylusUp += AllPurposeEventHandler;
                el.PreviewStylusDown += AllPurposeEventHandler;
                el.PreviewStylusUp += AllPurposeEventHandler;

                el.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(AllPurposeEventHandler));
            }

            win.Show();
        }

        private void AllPurposeEventHandler(object sender, RoutedEventArgs e)
        {
            var now = DateTime.Now;

            if (now - lastTime > TimeSpan.FromMilliseconds(100))
                stackOutput.Children.Add(new TextBlock(new Run(" ")));

            lastTime = now;

            var text = new TextBlock();
            text.FontFamily = fontFam;
            text.Text = string.Format(format, e.RoutedEvent.Name, TypeWithoutNamespace(sender), TypeWithoutNamespace(e.Source), TypeWithoutNamespace(e.OriginalSource));

            stackOutput.Children.Add(text);
            (stackOutput.Parent as ScrollViewer).ScrollToBottom();
        }

        private string TypeWithoutNamespace(object sender)
        {
            var fullpath = sender.GetType().Name.Split('.');
            return fullpath[fullpath.Length - 1];
        }
    }
}
