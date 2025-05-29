using System.Windows.Controls;

namespace IO.MainApp
{

    public partial class AdminDashboard : Page
    {
        public AdminDashboard()
        {
            InitializeComponent();
            DataContext = new AdminDashboardViewModel();
        }
    }
}
