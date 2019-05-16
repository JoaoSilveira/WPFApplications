using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace CreateDatePicker
{
    public partial class DatePicker : UserControl
    {
        UniformGrid unigridMonth;
        DateTime datetimeSaved = DateTime.Now.Date;

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register(nameof(Date), typeof(DateTime?), typeof(DatePicker), new FrameworkPropertyMetadata(new DateTime(), DateChangedCallback));

        public static readonly RoutedEvent DateChangedEvent = EventManager.RegisterRoutedEvent(nameof(DateChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<DateTime?>), typeof(DatePicker));

        public DateTime? Date
        {
            get { return (DateTime?)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public event RoutedPropertyChangedEventHandler<DateTime?> DateChanged
        {
            add { AddHandler(DateChangedEvent, value); }
            remove { RemoveHandler(DateChangedEvent, value); }
        }

        public DatePicker()
        {
            InitializeComponent();
            Date = datetimeSaved;
            Loaded += DatePickerOnLoaded;
        }

        private void DatePickerOnLoaded(object sender, RoutedEventArgs e)
        {
            unigridMonth = FindUniGrid(lstboxMonth);

            if (Date.HasValue)
                unigridMonth.FirstColumn = (int)new DateTime(Date.Value.Year, Date.Value.Month, 1).DayOfWeek;
        }

        private UniformGrid FindUniGrid(DependencyObject vis)
        {
            if (vis is UniformGrid)
                return vis as UniformGrid;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(vis); i++)
            {
                var visReturn = FindUniGrid(VisualTreeHelper.GetChild(vis, i));

                if (visReturn != null)
                    return visReturn as UniformGrid;
            }
            return null;
        }

        void ButtonBackOnClick(object sender, RoutedEventArgs e) => FlipPage(true);

        void ButtonForwardOnClick(object sender, RoutedEventArgs e) => FlipPage(false);

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            e.Handled = e.Key == Key.PageDown || e.Key == Key.PageUp;

            if (e.Handled)
                FlipPage(e.Key == Key.PageDown);
        }

        private void FlipPage(bool isBack)
        {
            if (!Date.HasValue)
                return;

            int numPages = isBack ? -1 : 1;

            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                numPages *= 12;

            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                numPages = Math.Max(-1200, Math.Min(1200, 120 * numPages));

            var year = Date.Value.Year + numPages / 12;
            var month = Date.Value.Month + numPages % 12;

            while (month < 1)
            {
                month += 12;
                year--;
            }
            while (month > 12)
            {
                month -= 12;
                year++;
            }

            if (year < DateTime.MinValue.Year)
                Date = DateTime.MinValue.Date;
            else if (year > DateTime.MaxValue.Year)
                Date = DateTime.MaxValue.Date;
            else
                Date = new DateTime(year, month, Math.Min(Date.Value.Day, DateTime.DaysInMonth(year, month)));
        }

        void CheckBoxNullOnChecked(object sender, RoutedEventArgs e)
        {
            if (Date.HasValue)
            {
                datetimeSaved = Date.Value;
                Date = null;
            }
        }

        void CheckBoxNullOnUnchecked(object sender, RoutedEventArgs e) => Date = datetimeSaved;

        void ListBoxOnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!Date.HasValue)
                return;

            if (lstboxMonth.SelectedIndex != -1)
                Date = new DateTime(Date.Value.Year, Date.Value.Month, int.Parse(lstboxMonth.SelectedItem as string));
        }

        static void DateChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e) => (obj as DatePicker).OnDateChanged((DateTime?)e.OldValue, (DateTime?)e.NewValue);

        protected virtual void OnDateChanged(DateTime? oldValue, DateTime? newValue)
        {
            chkboxNull.IsChecked = !newValue.HasValue;

            if (newValue.HasValue)
            {
                txtblkMonthYear.Text = newValue.Value.ToString(DateTimeFormatInfo.CurrentInfo.YearMonthPattern);

                if (unigridMonth != null)
                    unigridMonth.FirstColumn = (int)new DateTime(newValue.Value.Year, newValue.Value.Month, 1).DayOfWeek;

                var daysInMonth = DateTime.DaysInMonth(newValue.Value.Year, newValue.Value.Month);

                if (daysInMonth != lstboxMonth.Items.Count)
                {
                    lstboxMonth.BeginInit();
                    lstboxMonth.Items.Clear();

                    for (var i = 1; i <= daysInMonth; i++)
                        lstboxMonth.Items.Add(i.ToString());

                    lstboxMonth.EndInit();
                }

                lstboxMonth.SelectedIndex = newValue.Value.Day - 1;
            }

            var args = new RoutedPropertyChangedEventArgs<DateTime?>(oldValue, newValue, DateChangedEvent);
            args.Source = this;
            RaiseEvent(args);
        }
    }
}
