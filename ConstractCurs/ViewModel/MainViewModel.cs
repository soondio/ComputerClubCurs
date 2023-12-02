using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BLL.Interfaces;
using System.Windows.Controls;
using Ninject;
using ConstractCurs.Unit;
using BLL;
using System.Windows;
using ConstractCurs.Components;
using BLL.Models;


namespace ConstractCurs.ViewModel
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private IDbCrud crudServ;
        private IAuthorizationService authServ;
        private IReportService reportServ;
        private IReservationService resServ;
        private IFoodOrderService foodServ;

        private Frame MainFrame;
        private MainWindow MainWindow;
        private SideMenu SideMenuBar;
        private List<ComputerPlaceModel> Places;
        private List<ComputerCompositionModel> Comps;
        private List<VideoCardModel> vid;
        private List<MonitorModel> mon;
        private List<HeadphonesModel> head;
        private List<KeyboardModel> key;
        private List<MouseModel> mos;
        private List<ProcessorModel> proc;
        private List<RAMModel> ram;
        private List<UserModel> Users;
        private List<ReservationModel> Reservations;
        private List<FoodModel> Foods;
        private List<FoodOrderModel> FoodOrders;

        private RelayCommand navigate;
        public RelayCommand NavigateCommand
        {
            get
            {
                return navigate ?? (navigate = new RelayCommand(obj =>
                {
                    var real = (string)obj;
                    switch (real)
                    {
                        case "Auth":
                            NavigatetoToAuthPage();
                            break;
                        case "Report":
                            NavigatetoToReportPage();
                            break;
                        case "Reservation":
                            NavigatetoToReservationPage();
                            break;
                        case "PersonalAcc":
                            NavigatetoToPersonalAcc();
                            break;
                        case "Places":
                            NavigatetoToPlacesPage();
                            break;
                        case "FoodOrder":
                            NavigatetoToFoodOrderPage();
                            break;
                    }
                }));
            }
        }

        private RelayCommand sidemenu;
        public RelayCommand SideMenuCommand
        {
            get
            {
                return sidemenu ?? (sidemenu = new RelayCommand(obj =>
                {
                    SideMenu();
                }));
            }
        }
        private RelayCommand sidemenuclose;
        public RelayCommand SideMenuCloseCommand
        {
            get
            {
                return sidemenuclose ?? (sidemenuclose = new RelayCommand(obj =>
                {
                    SideMenuClose();
                }));
            }
        }
        private RelayCommand close;
        public RelayCommand CloseCommand
        {
            get
            {
                return close ?? (close = new RelayCommand(obj =>
                {
                    CloseWindow();
                }));
            }
        }

        private RelayCommand maxmin;
        public RelayCommand MaxMinCommand
        {
            get
            {
                return maxmin ?? (maxmin = new RelayCommand(obj =>
                {
                    MaxMinWindow();
                }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private Visibility _AuthVis;
        public Visibility AuthVis
        {
            get { return _AuthVis = authServ.GetCurrentUser().type != UserType.Unauthorized ? Visibility.Visible : Visibility.Collapsed; }
            set
            {
                _AuthVis = value;
                NotifyPropertyChanged("AuthVis");
            }
        }


        private Visibility _clientVIs;
        public Visibility ClientVis
        {
            get { return _clientVIs = authServ.GetCurrentUser().type == UserType.Client ? Visibility.Visible : Visibility.Collapsed; }
            set
            {
                _clientVIs = value;
                NotifyPropertyChanged("ClientVis");
            }
        }

        private Visibility _adminVis;
        public Visibility AdminVis
        {
            get { return _adminVis = authServ.GetCurrentUser().type == UserType.Admin ? Visibility.Visible : Visibility.Collapsed; }
            set
            {
                _adminVis = value;
                NotifyPropertyChanged("AdminVis");
            }
        }

        private Visibility _NotAuth;
        public Visibility NotAuth
        {
            get { return _NotAuth = authServ.GetCurrentUser().type == UserType.Unauthorized ? Visibility.Visible : Visibility.Collapsed; }
            set
            {
                _NotAuth = value;
                NotifyPropertyChanged("NotAuth");
            }
        }

        public void UpdateAuth()
        {
            NotifyPropertyChanged("AuthVis");
            NotifyPropertyChanged("ClientVis");
            NotifyPropertyChanged("AdminVis");
            NotifyPropertyChanged("NotAuth");
        }



        public MainViewModel(SideMenu sideMenu, Frame mainFraim, MainWindow mainWindow)
        {
            MainFrame = mainFraim;
            SideMenuBar = sideMenu;

            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule("CompClubContext"));

            crudServ = kernel.Get<IDbCrud>();
            resServ = kernel.Get<IReservationService>();
            reportServ = kernel.Get<IReportService>();
            authServ = kernel.Get<IAuthorizationService>();
            foodServ = kernel.Get<IFoodOrderService>();
            
            Places = crudServ.GetAllPlaces();
            Comps = crudServ.GetAllComps();
            vid = crudServ.GetAllVideocards();
            mon = crudServ.GetAllMonitors();
            head = crudServ.GetAllHeadphones();
            key = crudServ.GetAllKeyboards();
            mos = crudServ.GetAllMice();
            proc = crudServ.GetAllProcessors();
            ram = crudServ.GetAllRAMs();
            Users = crudServ.GetAllUsers();
            FoodOrders = crudServ.GetAllFoodOrders();
            Foods = crudServ.GetAllFood();
            Reservations = crudServ.GetAllReservations();

            

            MainWindow = mainWindow;
            MainFrame.Navigate(new View.AuthPage(authServ, this));
            SideMenuBar.DataContext = this;
        }

        private void NavigatetoToPersonalAcc()
        {
            Reservations = crudServ.GetAllReservations();
            Users = crudServ.GetAllUsers();
            FoodOrders = crudServ.GetAllFoodOrders();
            MainFrame.Navigate(new View.PersonalAccPage(foodServ,crudServ,FoodOrders,resServ,authServ,this,Reservations,Users));
            SideMenuBar.CloseSide();
        }
        private void NavigatetoToAuthPage()
        {
            MainFrame.Navigate(new View.AuthPage(authServ, this));
            SideMenuBar.CloseSide();
        }
        private void NavigatetoToFoodOrderPage()
        {
            Foods = crudServ.GetAllFood();
            MainFrame.Navigate(new View.FoodOrderPage(authServ, this, foodServ,Foods));
            SideMenuBar.CloseSide();
        }
        private void NavigatetoToPlacesPage()
        {
            MainFrame.Navigate(new View.PlacesInfoPage(this, Places, Comps));
            SideMenuBar.CloseSide();
        }
        private void NavigatetoToReportPage()
        {
            MainFrame.Navigate(new View.ReportPage(reportServ));
            SideMenuBar.CloseSide();
        }
        private void NavigatetoToReservationPage()
        {
            MainFrame.Navigate(new View.ReservationPage(this,resServ,authServ,Users));
            SideMenuBar.CloseSide();
        }

        private void CloseWindow()
        {
            MainWindow.Close();
        }
        private void SideMenu()
        {
            SideMenuBar.OpenSide();
        }

        private void SideMenuClose()
        {
            SideMenuBar.CloseSide();
        }

        public void ToComputerPage(int id)
        {
            var cw = new Windows.ComputerInfoWindow(vid,mon,head,key,mos,proc,ram,Comps,Places,id);
            cw.Show();
        }

        public void ToReservationPage()
        {
            NavigatetoToReservationPage();
        }
        public void ToAccPage()
        {
            NavigatetoToPersonalAcc();
        }
        public void ToAuthPage()
        {
            NavigatetoToAuthPage();
        }

        public void ToConfirmFood(int id, DateTime ordertime)
        {
            var fc = new Windows.ConfirmFoodWindow(Foods,authServ, foodServ, ordertime, id);
            fc.Show();
        }


        private void MaxMinWindow()
        {
            if (MainWindow.WindowState == WindowState.Maximized)
                MainWindow.WindowState = WindowState.Normal;
            else
                MainWindow.WindowState = WindowState.Maximized;
        }
    }
}
