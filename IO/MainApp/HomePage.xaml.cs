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
        private void Rectangle1_MouseEnter(object sender, MouseEventArgs e)
        {
            
            LabB.Fill = new SolidColorBrush(Colors.Magenta);
        }

        private void Rectangle1_MouseLeave(object sender, MouseEventArgs e)
        {
           
            LabB.Fill = new SolidColorBrush(Colors.DarkMagenta);
        }
        private void Rectangle2_MouseEnter(object sender, MouseEventArgs e)
        {
            LabA.Fill = new SolidColorBrush(Colors.LightBlue);
           
        }

        private void Rectangle2_MouseLeave(object sender, MouseEventArgs e)
        {
        
            LabA.Fill = new SolidColorBrush(Colors.DarkCyan);
        
        }
        private void Rectangle3_MouseEnter(object sender, MouseEventArgs e)
        {
        
            BudynekL.Fill = new SolidColorBrush(Colors.LightPink);
       
        }

        private void Rectangle3_MouseLeave(object sender, MouseEventArgs e)
        {
          
            BudynekL.Fill=new SolidColorBrush(Colors.Magenta);

        }
        private void Rectangle4_MouseEnter(object sender, MouseEventArgs e)
        {
           
            BudynekA.Fill = new SolidColorBrush(Colors.LightYellow);
         
        }

        private void Rectangle4_MouseLeave(object sender, MouseEventArgs e)
        {
           
            BudynekA.Fill = new SolidColorBrush(Colors.Yellow);
           
        }
        private void Rectangle5_MouseEnter(object sender, MouseEventArgs e)
        {
            
            BudynekB.Fill = new SolidColorBrush(Colors.LightGreen);
         
        }

        private void Rectangle5_MouseLeave(object sender, MouseEventArgs e)
        {
        
            BudynekB.Fill = new SolidColorBrush(Colors.Green);
         
        }
        private void Rectangle6_MouseEnter(object sender, MouseEventArgs e)
        {
            Gimnastyczna.Fill = new SolidColorBrush(Colors.LightBlue);
          
        }

        private void Rectangle6_MouseLeave(object sender, MouseEventArgs e)
        {
            Gimnastyczna.Fill = new SolidColorBrush(Colors.Blue);
     
        }
    }
}
