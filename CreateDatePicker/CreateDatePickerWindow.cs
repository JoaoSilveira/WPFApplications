using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CreateDatePicker
{
    public partial class CreateDatePickerWindow : Window
    {
        public CreateDatePickerWindow()
        {
            InitializeComponent();
        }

        void DatePickerOnDateChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e) => txtblkDate.Text = e.NewValue?.ToString("d") ?? string.Empty;
    }
}
