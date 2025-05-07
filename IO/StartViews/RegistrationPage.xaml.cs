using IO.DataBase;
using IO.MainApp;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Controls;



namespace IO
{
    /// <summary>
    /// Logika interakcji dla klasy RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }

        private void RepeatPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel vm)
            {
                vm.RepeatPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}
