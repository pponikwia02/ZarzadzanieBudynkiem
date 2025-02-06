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

        //TODO Przechowywanie sal w bazie danych. Na razie może być tak
        //Observable Collection Reprezentuje dynamiczną kolekcję danych, która dostarcza powiadomienia po dodaniu lub usunięciu elementów albo odświeżeniu całej listy.
        private ObservableCollection<Classroom> classrooms = new ObservableCollection<Classroom>
        {
         
            new Classroom { Id = 2, Name = "Room 102", Seats = 25, IsReserved = true, ReservedBy = "Alice", ReservationStart = DateTime.Now.AddMinutes(10), ReservationEnd = DateTime.Now.AddHours(1) },
            
        };

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
        }

        // Wyszukaj sale w polu Search
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filtered = classrooms.Where(c => c.Name.ToLower().Contains(searchText)).ToList();
            ClassroomsListView.ItemsSource = filtered;
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
        
        // Rezerwacja sali ( jeśli jeszcze nie została zarezerwowana)
        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            //Nowe okno
            CreateReservationWindow createWindow = new CreateReservationWindow();
            
            createWindow.Owner = Application.Current.MainWindow;
            if (createWindow.ShowDialog() == true)
            {
                Classroom newReservation = createWindow.NewReservation;
                if (newReservation != null)
                {
                    // Zaznacza że sala jest zarezerwowana.
                    newReservation.IsReserved = true;
                  
                    classrooms.Add(newReservation);
                    MessageBox.Show("Rezerwacja dodana poprawnie");
                    // Refresh listy
                    ClassroomsListView.Items.Refresh();
                }
            }
        }

        //Zmiana rezerwacji
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedClassroom != null)
            {
                // Usuwanie rezerwacji
                MessageBox.Show($"Rezerwacja dla  {selectedClassroom.Name} została usunięta.");
                ClassroomsListView.Items.Refresh();
                classrooms.Remove(selectedClassroom);
             
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //zamknij okno
            Close();
        }

    }
}
