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
    /// Logika interakcji dla klasy RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Reg_Btn_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            string email = Login_Box.Text;
            string password = Password_Box.Password;
            string password_repeat = Password_Box_Repeat.Password;
            if(password==password_repeat && password != string.Empty)
            {
                flag = true;
            }
             if(password != password_repeat)
            {
                MessageBox.Show("Podane hasła nie są identyczne");
                flag = false;
                return;
            }
             if(password ==  string.Empty || email== string.Empty)
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
                flag = false;
            }
            if(flag)
            {
                this.NavigationService.Navigate(new LoginPage());

            }
        }
    }
}
