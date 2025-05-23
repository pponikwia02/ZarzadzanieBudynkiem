﻿using IO.MainApp;
using IO.DataBase;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace IO.MainApp
{
    /// <summary>
    /// Logika interakcji dla klasy CreateReservationWindow.xaml
    /// </summary>
    public partial class CreateReservationWindow : Window
    {
        // Propercja do przechowywania nowej rezerwacji
        //okno do rezerwacji
        public Classroom NewReservation { get;   set; }
        public bool IsReservationSuccessful {  get; set; }

        public CreateReservationWindow()
        {
            InitializeComponent();
        }
        public class SalaRezerwacjaHandler
        {
            public static bool ZapiszRezerwacje(string NrSali,string Rezerwujacy,string Od,string Do)
            {
                DataContext context = new DataContext();
                {
                    // Sprawdzenie, czy sala jest już zajęta w danym dniu
                    bool istnieje = context.Sale.Any(s => s.NrSali == NrSali && s.Od== Od);
                    if (istnieje)
                    {
                        return false; // Sala jest już zajęta
                    }

                    var nowaRezerwacja = new Sala
                    {
                        NrSali = NrSali,
                        Rezerwujacy=Rezerwujacy,
                        Od = Od,
                        Do = Do
                    };

                    context.Sale.Add(nowaRezerwacja);
                    context.SaveChanges();
                    return true;
                }
               
            }
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

            string NrSali=ClassroomIdTextBox.Text;
            string rezerwujacy=ClassroomNameTextBox.Text;
            string Od=ReservationStartTextBox.Text;
            string Do=ReservationEndTextBox.Text;

            if (string.IsNullOrEmpty(NrSali) || string.IsNullOrEmpty(rezerwujacy) || Od == null)
            {
                MessageBox.Show("Proszę uzupełnić wszystkie pola.");
                return;
            }

            bool sukces = SalaRezerwacjaHandler.ZapiszRezerwacje(NrSali,rezerwujacy,Od,Do);

            if (sukces)
            {
                IsReservationSuccessful = true;
                MessageBox.Show("Rezerwacja zapisana!");
                Close();
            }
            else
            {
                MessageBox.Show("Sala jest już zarezerwowana na ten dzień.");
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
