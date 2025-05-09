using System.Windows;
using System.Windows.Controls;

namespace IO
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
          
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }

       
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is LoginViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
                
            }
        }
    }
}