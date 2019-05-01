using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace NotepadClone
{
    public class AboutDialog : Window
    {
        public AboutDialog(Window owner)
        {
            var asmbly = Assembly.GetExecutingAssembly();
            var title = (AssemblyTitleAttribute)asmbly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
            var strTitle = title.Title;

            var version = (AssemblyFileVersionAttribute)asmbly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];
            var strVersion = version.Version.Substring(0, 3);

            var copyright = (AssemblyCopyrightAttribute)asmbly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];
            var strCopyright = copyright.Copyright;

            Title = $"About {strTitle}";
            ShowInTaskbar = false;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;
            Left = owner.Left + 96;
            Top = owner.Top + 96;

            var stackMain = new StackPanel();
            Content = stackMain;

            var txtblk = new TextBlock();
            txtblk.Text = $"{strTitle} Version {strVersion}";
            txtblk.FontFamily = new FontFamily("Times New Roman");
            txtblk.FontSize = 32;
            txtblk.FontStyle = FontStyles.Italic;
            txtblk.Margin = new Thickness(24);
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            stackMain.Children.Add(txtblk);

            txtblk = new TextBlock();
            txtblk.Text = strCopyright;
            txtblk.FontSize = 20;
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            stackMain.Children.Add(txtblk);

            var run = new Run("www.charlespetzel.com");
            var link = new Hyperlink(run);
            link.Click += LinkOnClick;
            txtblk = new TextBlock(link);
            txtblk.FontSize = 20;
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            stackMain.Children.Add(txtblk);

            var btn = new Button();
            btn.Content = "OK";
            btn.IsDefault = true;
            btn.IsCancel = true;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += OkOnClick;
            stackMain.Children.Add(btn);

            btn.Focus();
        }

        private void LinkOnClick(object sender, RoutedEventArgs e) => Process.Start("http://www.charlespetzold.com");

        private void OkOnClick(object sender, RoutedEventArgs e) => DialogResult = true;
    }
}
