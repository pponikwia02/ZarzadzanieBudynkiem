using IO.DataBase;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using IO.MainApp;
//michal
namespace IO.MainApp
{
    public class AdminDashboardViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AppUser> Users { get; set; }
        public ObservableCollection<Sala> Classrooms { get; set; }

        private AppUser _selectedUser;
        public AppUser SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                OnPropertyChanged(nameof(IsUserSelected));
            }
        }

        public bool IsUserSelected => SelectedUser != null;

        public string UserCount => Users?.Count.ToString() ?? "0";
        public string ReservationCount => Classrooms?.Count.ToString() ?? "0";

        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand UpdateUserCommand { get; }
        public ICommand ExitCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public AdminDashboardViewModel()
        {
            using var context = new DataContext();
            Users = new ObservableCollection<AppUser>(context.Users.ToList());
            Classrooms = new ObservableCollection<Sala>(context.Sale.ToList());

            AddUserCommand = new RelayCommand(AddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser, _ => IsUserSelected);
            UpdateUserCommand = new RelayCommand(UpdateUser, _ => IsUserSelected);
        }

        private void AddUser(object obj)
        {
            var addWindow = new AddUserWindow();
            addWindow.ShowDialog();
            if (addWindow.IsReservationSuccessful)
            {
                RefreshUsers();
            }
        }

        private void DeleteUser(object obj)
        {
            if (SelectedUser == null) return;

            var result = MessageBox.Show($"Czy na pewno chcesz usunąć użytkownika {SelectedUser.login}?", "Potwierdzenie", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using var context = new DataContext();
                var user = context.Users.FirstOrDefault(u => u.login == SelectedUser.login);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    RefreshUsers();
                    MessageBox.Show("Użytkownik został usunięty.");
                }
            }
        }

        private void UpdateUser(object obj)
        {
            // Placeholder - dodaj okno edycji użytkownika
            MessageBox.Show($"Edycja użytkownika {SelectedUser?.login}");
        }

        private void RefreshUsers()
        {
            using var context = new DataContext();
            Users.Clear();
            foreach (var user in context.Users)
                Users.Add(user);

            OnPropertyChanged(nameof(UserCount));
        }

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
