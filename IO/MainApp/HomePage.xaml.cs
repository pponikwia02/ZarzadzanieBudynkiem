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
using System.Xml.Linq;
using IO.DataBase;
using IO.MainApp;

namespace IO
{
    /// <summary>
    /// Logika interakcji dla klasy HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        // Słownik przechowujący kolory dla budynków
        private readonly Dictionary<string, Brush> buildingColors = new()
        {
            {"Budynek L (Świętka)", new SolidColorBrush(Color.FromRgb(255, 200, 0))},
            {"Budynek A (Biblioteka)", new SolidColorBrush(Color.FromRgb(0, 150, 255))},
            {"Budynek B (Welcome Point)", new SolidColorBrush(Color.FromRgb(100, 255, 100))},
            {"Budynek H (Dziekanaty)", new SolidColorBrush(Color.FromRgb(255, 100, 255))},
            {"Budynek G (Biuro Rektora)", new SolidColorBrush(Color.FromRgb(255, 100, 100))},
            {"Budynek F (Laboratoria)", new SolidColorBrush(Color.FromRgb(100, 255, 255))},
            {"Budynek E (Aula)", new SolidColorBrush(Color.FromRgb(200, 100, 255))},
            {"Budynek C (Ślademiska)", new SolidColorBrush(Color.FromRgb(255, 255, 100))}
        };

        public HomePage()
        {
            InitializeComponent();
        }

        private void Building_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Rectangle building)
            {
                string buildingName = building.Tag.ToString();

                // Podświetlenie budynku
                building.Stroke = buildingColors[buildingName];
                building.StrokeThickness = 3;
                building.Fill = new SolidColorBrush(Color.FromArgb(60,
                    ((SolidColorBrush)buildingColors[buildingName]).Color.R,
                    ((SolidColorBrush)buildingColors[buildingName]).Color.G,
                    ((SolidColorBrush)buildingColors[buildingName]).Color.B));

                // Dynamiczny tooltip z informacjami
                var toolTip = new ToolTip
                {
                    Style = (Style)FindResource("BuildingToolTipStyle"),
                    Content = CreateToolTipContent(buildingName)
                };
                building.ToolTip = toolTip;
            }
        }

        private StackPanel CreateToolTipContent(string buildingName)
        {
            using var context = new DataContext();
            int roomCount = context.Sale.Count(s => s.NrSali.StartsWith(buildingName.Substring(7, 1)));

            return new StackPanel
            {
                Children =
                {
                    new TextBlock
                    {
                        Text = buildingName,
                        FontWeight = FontWeights.Bold,
                        FontSize = 14,
                        Foreground = buildingColors[buildingName]
                    },
                    new TextBlock
                    {
                        Text = $"Liczba sal: {roomCount}",
                        FontStyle = FontStyles.Italic
                    },
                    new TextBlock
                    {
                        Text = "Kliknij, aby zarezerwować",
                        TextDecorations = TextDecorations.Underline
                    }
                }
            };
        }

        private void Building_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Rectangle building)
            {
                building.Stroke = Brushes.Transparent;
                building.Fill = Brushes.Transparent;
                building.ToolTip = null;
            }
        }

        private void Building_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Rectangle building)
            {
                string buildingName = building.Tag.ToString();
                ClassroomReservationWindow reservationWindow = new ClassroomReservationWindow(buildingName);
                reservationWindow.Owner = Window.GetWindow(this);
                reservationWindow.ShowDialog();
            }
        }
    }
}

