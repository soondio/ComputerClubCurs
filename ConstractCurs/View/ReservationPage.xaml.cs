﻿using BLL;
using ConstractCurs.ViewModel;
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

namespace ConstractCurs.View
{
    /// <summary>
    /// Логика взаимодействия для ReservationPage.xaml
    /// </summary>
    public partial class ReservationPage : Page
    {
        private ViewModel.ReservationViewModel VM;
        public ReservationPage(MainViewModel mainWindow,BLL.Interfaces.IReservationService resServ,BLL.Interfaces.IAuthorizationService authServ,List<UserModel> Users)
        {
            VM = new ViewModel.ReservationViewModel(mainWindow,resServ,authServ,Users);
            DataContext = VM;
            InitializeComponent();
        }
    }
}
