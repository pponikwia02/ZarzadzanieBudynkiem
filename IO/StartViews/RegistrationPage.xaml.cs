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
using IO.DataBase;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Configuration;


namespace IO
{
    /// <summary>
    /// Logika interakcji dla klasy RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            UserTypeComboBox.ItemsSource=UserTypeItem.GetUserTypes();
            UserTypeComboBox.SelectedIndex=0;//domyślnie jest wykładowca
            UserTypeComboBox.SelectedValuePath = "Value";
            UserTypeComboBox.DisplayMemberPath = "DisplayName";

        }
        public class UserService
        {
            public bool RegisterUser(string login, string password, string passwordRepeat, UserTypeItem selectedType)
            {
               
                DataContext context = new DataContext();
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Uzupełnij wszystkie pola!");
                    return false;
                }

                if (password != passwordRepeat)
                {
                    MessageBox.Show("Podane hasła nie są identyczne");
                    return false;
                }
                if (selectedType == null)
                {
                    MessageBox.Show("Wybierz typ użytkownika");
                    return false;
                }

                {
                    // Sprawdzenie, czy login już istnieje w bazie
                    if (context.Users.Any(u => u.login == login))
                    {
                        MessageBox.Show("Użytkownik z takim loginem już istnieje!");
                        return false;
                    }

                    // Tworzenie użytkownika
                    var newUser = new AppUser
                    {
                        login = login,
                        password = password, // Hashujemy hasło przed zapisem!
                        UserType = selectedType.Value
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges(); // Zapis do bazy

                    MessageBox.Show("Rejestracja zakończona sukcesem!");
                    return true;
                }
            }
        }

        public void Reg_Btn_Click(object sender, RoutedEventArgs e)
        {
            var userService= new UserService();
            var selectedUserType = (UserTypeItem)UserTypeComboBox.SelectedItem;
            
            string login = Login_Box.Text;
            string password = Password_Box.Password;
            string password_repeat = Password_Box_Repeat.Password;
            //var userType = ((ComboBoxItem)UserTypeComboBox.SelectedItem).Content.ToString();

            
            if (userService.RegisterUser(login, password, password_repeat, selectedUserType))
            {
                this.NavigationService.Navigate(new LoginPage()); // Przejście do logowania
            }
        }
    }
}
