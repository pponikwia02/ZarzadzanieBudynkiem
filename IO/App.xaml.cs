using IO.DataBase;
using System.Configuration;
using System.Data;
using System.Windows;

namespace IO
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AppUser CurrentUser { get; set; }
    }

}
