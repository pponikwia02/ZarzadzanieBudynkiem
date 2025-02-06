using System;
using System.Collections.Generic;
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
using System.Xml.Linq;
using IO.MainApp;

namespace IO
{
    /// <summary>
    /// Logika interakcji dla klasy HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void Building_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Otwarcie okna rezerwacji
            if (sender is Rectangle clickedRectangle)
            {
                string selectedBuilding = clickedRectangle.Name.ToString(); // Pobranie nazwy budynku
                ClassroomReservationWindow reservationWindow = new ClassroomReservationWindow(selectedBuilding);
                reservationWindow.Show();
            }

        }
    }
}
