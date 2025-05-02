using IO.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IO.MainApp
{
    /// <summary>
    /// Logika interakcji dla klasy AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Page
    {
        private ObservableCollection<AppUser>Users = new ObservableCollection<AppUser>();
        public AdminDashboard()
        {
            InitializeComponent();
            LoadData();
            DataContext context = new DataContext();
            
        }
        private void RefreshDataGrid()
        {

            using (var context = new DataContext())
            {
                Users = new ObservableCollection<AppUser>(context.Users.ToList());
                UsersGrid.ItemsSource = Users;
            }
        }
        private AppUser _selectedReservation;
        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedReservation = UsersGrid.SelectedItem as AppUser;
            DeleteButton.IsEnabled = _selectedReservation != null;
            UpdateButton.IsEnabled = _selectedReservation != null;

            if (_selectedReservation != null)
            {
                DeleteButton.Background = Brushes.Aqua;
                UpdateButton.Background = Brushes.Aqua;

            }
            else
            {
                DeleteButton.Background = Brushes.LightGray;
                UpdateButton.Background= Brushes.LightGray;
            }
        }
        private void LoadData()
        {
            using var context = new DataContext();
            UsersGrid.ItemsSource = context.Users.ToList();
            ClassroomsGrid.ItemsSource = context.Sale.ToList();
            UserCountText.Text = context.Users.Count().ToString();
            ReservationCountText.Text = context.Sale.Count().ToString();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new AddUserWindow();
            createWindow.ShowDialog();
            if (createWindow.IsReservationSuccessful) 
            {
                RefreshDataGrid();
                
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedReservation == null) return;

            var result = MessageBox.Show($"Czy na pewno chcesz usunąć użytkownika {_selectedReservation.login}?", "Potwierdzenie", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using (var context = new DataContext())
                {
                    var reservationToDelete = context.Users.FirstOrDefault(s => s.login == _selectedReservation.login);
                    if (reservationToDelete != null)
                    {
                        context.Users.Remove(reservationToDelete);
                        context.SaveChanges();

                        RefreshDataGrid();
                        _selectedReservation = null;
                        DeleteButton.IsEnabled = false;
                        DeleteButton.Background = Brushes.LightGray;

                        MessageBox.Show("Użytkownik został usunięty");
                    }
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }
    }
}
