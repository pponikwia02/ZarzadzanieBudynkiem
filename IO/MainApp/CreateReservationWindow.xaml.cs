using IO.MainApp;
using IO.DataBase;
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
