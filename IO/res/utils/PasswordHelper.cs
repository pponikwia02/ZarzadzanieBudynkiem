using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace IO.res.utils
{
    public static class PasswordHelper
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordHelper),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static string GetPassword(DependencyObject d) => (string)d.GetValue(PasswordProperty);
        public static void SetPassword(DependencyObject d, string value) => d.SetValue(PasswordProperty, value);

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordHelper), new PropertyMetadata(false, Attach));

        public static bool GetAttach(DependencyObject d) => (bool)d.GetValue(AttachProperty);
        public static void SetAttach(DependencyObject d, bool value) => d.SetValue(AttachProperty, value);

        private static void Attach(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is PasswordBox passwordBox && (bool)e.NewValue)
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;
            SetPassword(box, box.Password);
        }

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox box && !string.Equals(box.Password, (string)e.NewValue))
                box.Password = (string)e.NewValue;
        }
    }
}
