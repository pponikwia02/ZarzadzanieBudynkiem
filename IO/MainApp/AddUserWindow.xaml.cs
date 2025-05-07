using IO.DataBase;
using IO.MainApp;
using System.Windows.Controls;
using System.Windows;

namespace IO.MainApp
{
    public partial class AddUserWindow : Window
    {
        public bool IsReservationSuccessful { get; set; }

        public AddUserWindow()
        {
            InitializeComponent();

            if (DataContext is AddUserViewModel vm)
                vm.WindowInstance = this;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddUserViewModel vm)
                vm.Password = ((PasswordBox)sender).Password;
        }

        private void RepeatPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddUserViewModel vm)
                vm.RepeatPassword = ((PasswordBox)sender).Password;
        }
    }
}