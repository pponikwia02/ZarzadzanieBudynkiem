using IO.DataBase;
using IO.MainApp;
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
        public class LoginHandler
        {
            public static bool AuthenticateUser(string login, string password)
            {
                DataContext context = new DataContext();
                {
                    var user = context.Users.FirstOrDefault(u => u.login == login && u.password == password);
                    return user != null;
                }
            }
        }
        private void Log_In_Btn_Click(object sender, RoutedEventArgs e)
        {
           
            string login = Login_Box.Text;
            string password = Password_Box.Password;
            using var context = new DataContext();
            var user = context.Users.FirstOrDefault(u => u.login == login && u.password == password);
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
                return;
            }

            bool isAuthenticated = LoginHandler.AuthenticateUser(login, password);

            

            if (user!=null)
            {
                MessageBox.Show("Logowanie udane! Przechodzenie dalej...");
                if (user.UserType==1)
                {

                    this.NavigationService.Navigate(new AdminDashboard());
                    MessageBox.Show($"zalogowaleś się do panelu Administratora");
                }
                else
                {

                    this.NavigationService.Navigate(new HomePage());
                    MessageBox.Show($"zalogowaleś się do panelu Wykładowcy");
                }
            }
            else
            {
                MessageBox.Show("Błąd logowania: niepoprawne dane.");
            }

        }
    }
}
