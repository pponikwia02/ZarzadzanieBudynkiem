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
    /// Logika interakcji dla klasy ClassroomReservationWindow.xaml
    /// </summary>
  
    public partial  class ClassroomReservationWindow : Window
    {

        //okno po naciśnięciu na budynek
        //Observable Collection Reprezentuje dynamiczną kolekcję danych, która dostarcza powiadomienia po dodaniu lub usunięciu elementów albo odświeżeniu całej listy.
        private ObservableCollection<Classroom> classrooms = new ObservableCollection<Classroom>();
        private ObservableCollection<Sala>Sale =new ObservableCollection<Sala>();

        //trzyma wybrane sale
        private Classroom selectedClassroom;
        //Nazwa Budynku na który klikneliśmy
        private readonly string buildingName;

        public ClassroomReservationWindow(string building)
        {
            InitializeComponent();
            buildingName = building;
            BuildingNameTextBlock.Text = $"{buildingName}";
            ClassroomsListView.ItemsSource = classrooms;
            DataContext context = new DataContext();
            myDataGrid.ItemsSource = context.Sale.ToList();
           
        }
        /*private void RefreshDataGrid()
        {
           
            myDataGrid.ItemsSource = null;
            myDataGrid.ItemsSource = Sale;
        }*/
        private void RefreshDataGrid()
        {

            using (var context = new DataContext()) 
            {
                Sale=new ObservableCollection<Sala>(context.Sale.ToList());
                myDataGrid.ItemsSource = Sale;
            }
        }

        private Sala _selectedReservation;
        private void MyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedReservation = myDataGrid.SelectedItem as Sala;
            ChangeButton.IsEnabled=_selectedReservation!=null;

            if (_selectedReservation != null) 
            {
                ChangeButton.Background=Brushes.Aqua;

            }
            else
            {
                ChangeButton.Background = Brushes.LightGray;
            }
        }
        // Wyszukaj sale w polu Search
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower().Trim();

            if(Sale==null || Sale.Count == 0)
            {
                using var context= new DataContext();
                Sale=new ObservableCollection<Sala>(context.Sale.ToList());
            }

            if (string.IsNullOrEmpty(searchText)) 
            {
                myDataGrid.ItemsSource=Sale;
                return;
            }

            var filtered = Sale.Where(s => s.NrSali.ToLower().Contains(searchText) ||
           
            s.Od.ToLower().Contains(searchText) ||
            s.Do.ToLower().Contains(searchText)).ToList();

            myDataGrid.ItemsSource= filtered;
        }

        //
        private void ClassroomsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedClassroom = ClassroomsListView.SelectedItem as Classroom;
            if (selectedClassroom != null)
            {
                ReserveButton.IsEnabled = true;
                ChangeButton.IsEnabled = selectedClassroom.IsReserved;
            }
            else
            {
                ReserveButton.IsEnabled = true;
                ChangeButton.IsEnabled = false;
            }
        }
        
       //Rezerwacja sali oraz odświeżenie datagridu 
        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            //Nowe okno
            var createWindow = new CreateReservationWindow();
            createWindow.ShowDialog();
            if (createWindow.IsReservationSuccessful)
            {
                using var context = new DataContext();
                myDataGrid.ItemsSource=context.Sale.ToList();
            }
        }

        //Zmiana rezerwacji
        /*private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedClassroom != null)
            {
                // Usuwanie rezerwacji
                MessageBox.Show($"Rezerwacja dla  {selectedClassroom.Name} została usunięta.");
                ClassroomsListView.Items.Refresh();
                classrooms.Remove(selectedClassroom);
             
            }
        }*/
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedReservation == null) return;

            var result = MessageBox.Show($"Czy na pewno chcesz usunąć rezerweacje sali {_selectedReservation.NrSali}?","Potwierdzenie",MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes) 
            {
                using (var context = new DataContext()) 
                {
                    var reservationToDelete=context.Sale.FirstOrDefault(s=>s.IdSali==_selectedReservation.IdSali);
                    if (reservationToDelete != null) 
                    {
                        context.Sale.Remove(reservationToDelete);
                        context.SaveChanges();

                        RefreshDataGrid();
                        _selectedReservation = null;
                        ChangeButton.IsEnabled=false;
                        ChangeButton.Background = Brushes.LightGray;

                        MessageBox.Show("Rezerwacja została usunięta");
                    }
                }   
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //zamknij okno
            Close();
        }

    }
}
