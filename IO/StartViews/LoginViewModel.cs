using IO.DataBase;
using IO.MainApp;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace IO
{
    public class LoginViewModel : INotifyPropertyChanged
    {
         protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
            set { _login = value;  }
        }

        public string Password
        {
            get => _password;
            set { _password = value; }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginExecute, CanLoginExecute);
            RegisterCommand = new RelayCommand(RegisterExecute);
        }

       
        private bool CanLoginExecute(object parameter) => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        
        private void LoginExecute(object parameter)
        {
            using var context = new DataContext();
            var user = context.Users.FirstOrDefault(u => u.login == Login);

            if (user != null && PasswordHasher.VerifyPassword(Password, user.password))
            {
                MessageBox.Show("Logowanie zakończone sukcesem");

                if (user.UserType == 1)
                    ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new AdminDashboard());
                else
                    ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new HomePage());
            }
            else
            {
                MessageBox.Show("Błąd logowania: niepoprawne dane.");
            }
        }

        private void RegisterExecute(object parameter)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new RegistrationPage());
        }
    }
}
