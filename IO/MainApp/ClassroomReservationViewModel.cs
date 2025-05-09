using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.DataBase;
using System.Windows.Input;
using System.Windows;
using System.Collections.Specialized;
using System.ComponentModel;
//michal
namespace IO.MainApp
{
    public class ClassroomReservationViewModel : INotifyPropertyChanged
    {
        private readonly string _buildingName;

        public ObservableCollection<Classroom> Classrooms { get; set; } = new();
        public ObservableCollection<Sala> Sale { get; set; } = new();

        private Sala _selectedReservation;
        public Sala SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
                OnPropertyChanged(nameof(CanChangeReservation));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                
                FilterSale();
            }
        }

        public ICommand ReserveCommand { get; }
        public ICommand ChangeCommand { get; }
        public ICommand CloseCommand { get; }

        public bool CanChangeReservation => SelectedReservation != null;

        public ClassroomReservationViewModel(string buildingName)
        {
            _buildingName = buildingName;
            LoadSale();

            ReserveCommand = new RelayCommand(_ => OpenReservationWindow());
            ChangeCommand = new RelayCommand(_ => DeleteReservation(), _ => CanChangeReservation);
            CloseCommand = new RelayCommand(_ => Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close());
        }

        private void LoadSale()
        {
            using var context = new DataContext();
            Sale = new ObservableCollection<Sala>(context.Sale.ToList());
            OnPropertyChanged(nameof(Sale));
        }

        private void FilterSale()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadSale();
                return;
            }

            var lower = SearchText.ToLower().Trim();
            var filtered = Sale.Where(s =>
                s.NrSali.ToLower().Contains(lower) ||
                s.Od.ToLower().Contains(lower) ||
                s.Do.ToLower().Contains(lower)
            ).ToList();

            Sale = new ObservableCollection<Sala>(filtered);
            OnPropertyChanged(nameof(Sale));
        }

        private void OpenReservationWindow()
        {
            var window = new CreateReservationWindow();
            window.ShowDialog();

            if (window.IsReservationSuccessful)
            {
                LoadSale();
            }
        }

        private void DeleteReservation()
        {
            if (SelectedReservation == null) return;

            var result = MessageBox.Show($"Czy na pewno chcesz usunąć rezerwację sali {SelectedReservation.NrSali}?", "Potwierdzenie", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes) return;

            using var context = new DataContext();
            var toDelete = context.Sale.FirstOrDefault(s => s.IdSali == SelectedReservation.IdSali);
            if (toDelete != null)
            {
                context.Sale.Remove(toDelete);
                context.SaveChanges();
                LoadSale();
                SelectedReservation = null;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string BuildingDisplayName => $"Budynek: {_buildingName}";
        protected void OnPropertyChanged(string name) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

