using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using IO.MainApp;

public class HomePageViewModel
{
    public ObservableCollection<BuildingViewModel> Buildings { get; set; }

    public HomePageViewModel()
    {
        Buildings = new ObservableCollection<BuildingViewModel>
        {
            new("Budynek L", new SolidColorBrush(Color.FromRgb(255, 200, 0)), 517, 85, 107, 264, 0, OnBuildingClick),
            new("Budynek A", new SolidColorBrush(Color.FromRgb(0, 150, 255)), 290, 58, 107, 427, 0, OnBuildingClick),
            new("Budynek B", new SolidColorBrush(Color.FromRgb(100, 255, 100)), 275, 55, 107, 598, 0, OnBuildingClick),
            new("Budynek H", new SolidColorBrush(Color.FromRgb(255, 100, 255)), 138, 60, 195, 899, 0, OnBuildingClick),
            new("Budynek G", new SolidColorBrush(Color.FromRgb(255, 100, 100)), 230, 130, 397, 838, 0, OnBuildingClick),
            new("Budynek F", new SolidColorBrush(Color.FromRgb(100, 255, 255)), 78, 83, 759, 744, -3.092, OnBuildingClick),
            new("Budynek E", new SolidColorBrush(Color.FromRgb(200, 100, 255)), 42, 52, 802, 898, -2.304, OnBuildingClick),
            new("Budynek C", new SolidColorBrush(Color.FromRgb(255, 255, 100)), 74, 197, 904, 689, 0, OnBuildingClick)
        };
    }

    private void OnBuildingClick(string buildingName)
    {
        var window = new ClassroomReservationWindow(buildingName);
        bool? result = window.ShowDialog();

        if (result == true)
        {
            var building = Buildings.FirstOrDefault(b => b.Name == buildingName);
            building?.Refresh();
        }
    }
}