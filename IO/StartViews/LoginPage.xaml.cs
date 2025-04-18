using IO.ViewModels;
using System.Windows.Controls;

namespace IO
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            if (DataContext is LoginViewModel vm)
                vm.Navigate += page => this.NavigationService.Navigate(page);
        }
    }
}