﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConstractCurs.Components
{
    /// <summary>
    /// Логика взаимодействия для SideMenu.xaml
    /// </summary>
    public partial class SideMenu : UserControl
    {
        public SideMenu()
        {
            InitializeComponent();
        }

        public Button GetButton(string name)
        {
            switch (name)
            {
                case "Reservation":
                    return ReservationButton;
                case "Auth":
                    return AuthButton;
                case "PersonalAcc":
                    return PersonalAccoutButton;
                case "Report":
                    return ReportButton;
                case "Places":
                    return PlacesButton;
                case "FoodOrder":
                    return FoodOrderButton;
            }
            return null;
        }

        public void OpenSide()
        {
            ThicknessAnimation da = new ThicknessAnimation();
            da.Duration = TimeSpan.FromSeconds(0.3);
            da.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };
            da.To = new System.Windows.Thickness(0, 30, 0, 0);
            BeginAnimation(MarginProperty, da);

        }
        public void CloseSide()
        {
            ThicknessAnimation da = new ThicknessAnimation();
            da.Duration = TimeSpan.FromSeconds(0.3);
            da.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };
            da.To = new System.Windows.Thickness(-260, 30, 0, 0);
            BeginAnimation(MarginProperty, da);
        }
    }
}
