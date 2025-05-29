using System.Windows;

namespace IO.MainApp
{

    public partial  class ClassroomReservationWindow : Window
    {
       public ClassroomReservationWindow(string building)
        {
            InitializeComponent();
            DataContext = new ClassroomReservationViewModel(building);
        }
    }
}
