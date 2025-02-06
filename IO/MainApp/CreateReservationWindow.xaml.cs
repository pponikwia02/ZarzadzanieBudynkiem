using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace IO.MainApp
{
    /// <summary>
    /// Logika interakcji dla klasy CreateReservationWindow.xaml
    /// </summary>
    public partial class CreateReservationWindow : Window
    {
        // Propercja do przechowywania nowej rezerwacji
        public Classroom NewReservation { get;   set; }

        public CreateReservationWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdzanie poprawności id
            if (!int.TryParse(ClassroomIdTextBox.Text.Trim(), out int classId))
            {
                MessageBox.Show("Podaj poprawne ID sali");
                return;
            }

            // Sprawdzanie poprawności nazwy
            string className = ClassroomNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(className))
            {
                MessageBox.Show("Podaj porawną nazwe sali.");
                return;
            }

            // Sprawdzanie poprawności rezerwującego
            string reservedBy = ReservedByTextBox.Text.Trim();
            if (string.IsNullOrEmpty(reservedBy))
            {
                MessageBox.Show("Podaj Imię i nazwisko osoby rezerwującej");
                return;
            }

            // Sprawdzanie poprawności daty rozpoczęcia rezerwacji
            if (!DateTime.TryParse(ReservationStartTextBox.Text.Trim(), CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime start))
            {
                MessageBox.Show("Podaj poprawną datę i godzinę rozpoczęcia rezerwacji");
                return;
            }

            // Sprawdzanie poprawności daty zakończenia rezerwacji
            if (!DateTime.TryParse(ReservationEndTextBox.Text.Trim(), CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime end))
            {
                MessageBox.Show("Podaj poprawną datę i godzinę zakończenia rezerwacji");
                return;
            }

            if (end <= start)
            {
                MessageBox.Show("Sprawdz daty rezerwacji.");
                return;
            }

           
            NewReservation = new Classroom
            {
                Id = classId,
                Name = className,
                ReservedBy = reservedBy,
                ReservationStart = start,
                ReservationEnd = end,
                IsReserved = true
            };

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
