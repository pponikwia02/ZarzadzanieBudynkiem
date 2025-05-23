using System.Windows.Controls;

namespace IO
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomePageViewModel();
        }
    }
}
