using IO.DataBase;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
public class BuildingViewModel : INotifyPropertyChanged
{
    protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    public string Name { get; set; }                
    public double Width { get; set; }
    public double Height { get; set; }
    public double CanvasLeft { get; set; }
    public double CanvasTop { get; set; }
    public double Rotation { get; set; }
    public Brush Color { get; set; }

    private int _reservationCount;
    public event PropertyChangedEventHandler PropertyChanged;
    public int ReservationCount
    {
        get => _reservationCount;
        private set
        {
            _reservationCount = value;
            OnPropertyChanged(nameof(ReservationCount));
        }
    }


    public ICommand ClickCommand { get; }

    public BuildingViewModel(string name, Brush color, double width, double height, double left, double top, double rotation, Action<string> onClick)
    {
        Name = name;
        Color = color;
        Width = width;
        Height = height;
        CanvasLeft = left;
        CanvasTop = top;
        Rotation = rotation;

        ClickCommand = new RelayCommand(_ => onClick?.Invoke(Name));
        LoadReservationCount();
    }

    private void LoadReservationCount()
    {
        using var context = new DataContext();
        
        ReservationCount = context.Sale.Count(s => s.NrSali.StartsWith(Name.Last().ToString()));
        
    }
    public void Refresh()
    {
        LoadReservationCount(); 
    }
   
}
