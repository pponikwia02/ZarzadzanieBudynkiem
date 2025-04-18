using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IO.DataBase;
using IO.MainApp;
using System.Windows.Input;
using System.Windows;
using IO.res.utils;

namespace IO.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public event Action<object> Navigate;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(_ => LoginExecute());
            RegisterCommand = new RelayCommand(_ => Navigate?.Invoke(new RegistrationPage()));
        }

        private void LoginExecute()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
                return;
            }

            using var context = new DataContext();
            var user = context.Users.FirstOrDefault(u => u.login == Login && u.password == Password);

            if (user != null)
            {
                MessageBox.Show("Logowanie udane! Przechodzenie dalej...");

                if (user.UserType == "Administrator")
                    Navigate?.Invoke(new AdminDashboard());
                else
                    Navigate?.Invoke(new HomePage());
            }
            else
            {
                MessageBox.Show("Błąd logowania: niepoprawne dane.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
