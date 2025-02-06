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

namespace IO
{
    /// <summary>
    /// Logika interakcji dla klasy LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Rejestracja_Btn_MouseDown(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegistrationPage());
        }

        private void Log_In_Btn_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            string username = Login_Box.Text;
            string password = Password_Box.Password;
            if (password == string.Empty || username == string.Empty)
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
            }

            //TODO:
            // Sprawdź czy taki użytkownik istnieje/wprowadzone dane sa poprawne

            if (password == "admin" && username == "admin") flag = true;
            if(flag)
            {
                this.NavigationService.Navigate(new HomePage());
            }
            
        }
    }
}
