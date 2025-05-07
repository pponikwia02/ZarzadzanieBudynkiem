using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.DataBase;
using System.Windows.Input;
using System.Windows;

namespace IO.MainApp
{
    public class AddUserViewModel : INotifyPropertyChanged
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

        public ObservableCollection<UserTypeItem> UserTypes { get; }

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public Window WindowInstance { get; set; }

        public AddUserViewModel()
        {
            UserTypes = new ObservableCollection<UserTypeItem>(UserTypeItem.GetUserTypes());
            SelectedUserType = UserTypes.FirstOrDefault();

            AddCommand = new RelayCommand(AddUser);
            CancelCommand = new RelayCommand(CancelWindow);
        }

        private void AddUser(object obj)
        {
            using var context = new DataContext();

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
            if (WindowInstance != null)
            {
                ((AddUserWindow)WindowInstance).IsReservationSuccessful = true;
                WindowInstance.Close();
            }
        }

        private void CancelWindow(object obj)
        {
            if (WindowInstance != null)
            {
                WindowInstance.DialogResult = false;
                WindowInstance.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
