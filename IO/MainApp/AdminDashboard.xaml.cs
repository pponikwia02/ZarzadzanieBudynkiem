﻿using IO.DataBase;
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

namespace IO.MainApp
{
    /// <summary>
    /// Logika interakcji dla klasy AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Page
    {
        public AdminDashboard()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using var context = new DataContext();
            UsersGrid.ItemsSource = context.Users.ToList();
            ClassroomsGrid.ItemsSource = context.Sale.ToList();
            UserCountText.Text = context.Users.Count().ToString();
            ReservationCountText.Text = context.Sale.Count().ToString();
        }
    }
}
