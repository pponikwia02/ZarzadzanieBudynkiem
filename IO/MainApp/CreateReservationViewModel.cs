﻿using System.ComponentModel;
using IO.DataBase;
using System.Windows.Input;
using System.Windows;

namespace IO.MainApp
{
    public class CreateReservationViewModel : INotifyPropertyChanged
    {
        private string _nrSali;
        public string NrSali
        {
            get => _nrSali;
            set
            {
                _nrSali = value;
                OnPropertyChanged(nameof(NrSali));
            }
        }

        private string _rezerwujacy;
        public string Rezerwujacy
        {
            get => _rezerwujacy;
            set
            {
                _rezerwujacy = value;
                OnPropertyChanged(nameof(Rezerwujacy));
            }
        }

        private string _od;
        public string Od
        {
            get => _od;
            set
            {
                _od = value;
                OnPropertyChanged(nameof(Od));
            }
        }

        private string _do;
        public string Do
        {
            get => _do;
            set
            {
                _do = value;
                OnPropertyChanged(nameof(Do));
            }
        }

        public ICommand CreateCommand { get; }
        public ICommand CancelCommand { get; }

        public event Action<bool> RequestClose;

        public CreateReservationViewModel(string currentUserLogin)
        {
            Rezerwujacy=currentUserLogin;
            CreateCommand = new RelayCommand(CreateReservation);
            CancelCommand = new RelayCommand(_ => RequestClose?.Invoke(false));
        }

        private void CreateReservation(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NrSali) || string.IsNullOrWhiteSpace(Rezerwujacy) || string.IsNullOrWhiteSpace(Od))
            {
                MessageBox.Show("Proszę uzupełnić wszystkie pola.");
                return;
            }

            using var context = new DataContext();

            bool exists = context.Sale.Any(s => s.NrSali == NrSali && s.Od == Od);
            if (exists)
            {
                MessageBox.Show("Sala jest już zarezerwowana na ten dzień.");
                return;
            }

            var newReservation = new Sala
            {
                NrSali = NrSali,
                Rezerwujacy = Rezerwujacy,
                Od = Od,
                Do = Do
            };

            context.Sale.Add(newReservation);
            context.SaveChanges();

            MessageBox.Show("Rezerwacja zapisana!");
            RequestClose?.Invoke(true);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
