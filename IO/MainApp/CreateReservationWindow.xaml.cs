using System.Windows;

namespace IO.MainApp
{
    /// <summary>
    /// Logika interakcji dla klasy CreateReservationWindow.xaml
    /// </summary>
    public partial class CreateReservationWindow : Window
    {
        public bool IsReservationSuccessful { get; private set; }

        public CreateReservationWindow()
        {
            InitializeComponent();

            var vm = new CreateReservationViewModel();
            vm.RequestClose += OnRequestClose;
            DataContext = vm;
        }

        private void OnRequestClose(bool success)
        {
            IsReservationSuccessful = success;
            DialogResult = success;
            Close();
        }
    }
}
