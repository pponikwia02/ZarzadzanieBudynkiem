using IO.DataBase;
using IO.MainApp;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace IO
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private string _repeatPassword;
        private UserTypeItem _selectedUserType;

        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public string RepeatPassword
        {
            get => _repeatPassword;
            set { _repeatPassword = value; OnPropertyChanged(nameof(RepeatPassword)); }
        }

        public UserTypeItem SelectedUserType
        {
            get => _selectedUserType;
            set { _selectedUserType = value; OnPropertyChanged(nameof(SelectedUserType)); }
        }

        public List<UserTypeItem> UserTypes { get; }

        public ICommand RegisterCommand { get; }
       

        public RegistrationViewModel()
        {
            UserTypes = new List<UserTypeItem>(UserTypeItem.GetUserTypes());
            SelectedUserType = UserTypes.FirstOrDefault();

            RegisterCommand = new RelayCommand(RegisterExecute);
        }

        private void RegisterExecute(object obj)
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
                return;
            }

            if (Password != RepeatPassword)
            {
                MessageBox.Show("Podane hasła nie są identyczne");
                return;
            }

            using var context = new DataContext();

            if (context.Users.Any(u => u.login == Login))
            {
                MessageBox.Show("Użytkownik z takim loginem już istnieje!");
                return;
            }

            var hashedPassword = PasswordHasher.HashPassword(Password);

            var newUser = new AppUser
            {
                login = Login,
                password = hashedPassword,
                UserType = SelectedUserType.Value
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            MessageBox.Show("Rejestracja zakończona sukcesem!");

            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new LoginPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
