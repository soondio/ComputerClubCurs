using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using System.ComponentModel;
using BLL;
using System.Collections.ObjectModel;
using System.Windows;

namespace ConstractCurs.ViewModel
{
    public class PersonalAccViewModel : INotifyPropertyChanged
    {

        private IAuthorizationService authServ;
        private MainViewModel mainWindow;
        private List<ReservationModel> ReservationsByClient;
        private List<UserModel> AllUsers;
        private IReservationService resServ;
        private IDbCrud crudServ;
        private IFoodOrderService foodServ;
        public struct ReservationsListForAdmin
        {
            public int Id { get; set; }
            public string ClientName { get; set; }
            public int PlaceID { get; set; }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public decimal TotalPrice { get; set; }
            public string ReservationStatus { get; set; }
        }

        public struct FoodOrdersWithNames
        {
            public int Id { get; set; }
            public string FoodName { get; set; }
            public DateTime? OrderDateTime { get; set; }
            public decimal? TotalPrice
            {
                get; set;
            }
            public string OrderStatus
            {
                get; set;
            }
            public int FoodCount { get; set; }
        };


        
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        //private List<ReservationModel> _Reservations;
        //public List<ReservationModel> Reservations
        //{
        //    get { return _Reservations; }
        //    set
        //    {
        //        _Reservations = value;
        //        NotifyPropertyChanged("Reservations");
        //    }
        //}
        private ObservableCollection<ReservationsListForAdmin> _Reservations;
        public ObservableCollection<ReservationsListForAdmin> Reservations
        {
            get
            {
                return _Reservations;
            }
            set
            {
                _Reservations = value;
                NotifyPropertyChanged("Reservations");
            }
        }

        private ObservableCollection<FoodOrdersWithNames> _FoodOrders;
        public ObservableCollection<FoodOrdersWithNames> FoodOrders
        {
            get
            {
                return _FoodOrders;
            }
            set
            {
                _FoodOrders = value;
                NotifyPropertyChanged("FoodOrders");
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
        /*private List<FoodOrderModel> _FoodOrders;
        public List<FoodOrderModel> FoodOrders
        {
            get { return _FoodOrders; }
            set
            {
                _FoodOrders = value;
                NotifyPropertyChanged("FoodOrders");
            }
        }*/

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                NotifyPropertyChanged("UserName");
            }
        }
        private decimal? _Balance;
        public decimal? Balance
        {
            get { return _Balance; }
            set
            {
                _Balance = value;
                NotifyPropertyChanged("Balance");
            }
        }
        private decimal? _Bonuses;
        public decimal? Bonuses
        {
            get { return _Bonuses; }
            set
            {
                _Bonuses = value;
                NotifyPropertyChanged("Bonuses");
            }
        }

        /*private string _FoodName;
        public string FoodName
        {
            get { return _FoodName; }
            set
            {
                _FoodName = value;
                NotifyPropertyChanged("FoodName");
            }
        }*/

        #endregion
        #region Command

        private RelayCommand _CancelOrder;
        public RelayCommand CancelOrder
        {
            get
            {
                return _CancelOrder ?? (_CancelOrder = new RelayCommand(obj =>
                  {
                      var value = (object)obj;
                      int ResId = (Int32.Parse((string)value));
                      //var mb = new Windows.CustomMessageBox("отменить заказ " + ResId, "ывы");
                      //mb.ShowDialog();
                      CancelReservation(ResId);
                  }));
            }

        }
        private RelayCommand _CancelFoodOrder;
        public RelayCommand CancelFoodOrder
        {
            get
            {
                return _CancelFoodOrder ?? (_CancelFoodOrder = new RelayCommand(obj =>
                    {
                        var value= (object)obj;
                        int FoodOrdId = (Int32.Parse((string)value));
                        CancelFoodOrderConfirm(FoodOrdId);
                    }));
            }
        }

        private RelayCommand _ConfirmReservationByAdmin;
        public RelayCommand ConfirmReservationByAdmin
        {
            get
            {
                return _ConfirmReservationByAdmin ?? (_ConfirmReservationByAdmin = new RelayCommand(obj =>
                  {
                      var value = (object)obj;
                      int ResId = (Int32.Parse((string)value));
                      ConfirmReservationByAdminMethod(ResId);
                  }));
            }
        }
        private RelayCommand _ConfirmFoodOrderByAdmin;
        public RelayCommand ConfirmFoodOrderByAdmin
        {
            get
            {
                return _ConfirmFoodOrderByAdmin ?? (_ConfirmFoodOrderByAdmin = new RelayCommand(obj =>
                  {
                      var value = (object)obj;
                      int FoodOrdId = (Int32.Parse((string)value));
                      ConfirmFoodOrderByAdminMethod(FoodOrdId);
                  }));
            }
        }

        private RelayCommand _LeftFromAcc;
        public RelayCommand LeftFromAcc
        {
            get
            {
                return _LeftFromAcc ?? (_LeftFromAcc = new RelayCommand(obj =>
                  {
                      ToLeftPage();
                  }));
            }
        }

        #endregion


        public PersonalAccViewModel(IFoodOrderService foodserv,IDbCrud crudServ,List<FoodOrderModel> foodOrders,IReservationService resServ,MainViewModel mainWindow, IAuthorizationService authServ,List<ReservationModel> rez, List<UserModel> users)
        {
            this.foodServ = foodserv;
            this.crudServ = crudServ;
            this.resServ = resServ;
            this.authServ = authServ;
            this.mainWindow = mainWindow;

            mainWindow.UpdateAuth();
            UpdateUserInfo(rez, users,foodOrders);

        }
        public void CancelReservation(int ResId)
        {
            
            if(resServ.CancelReservation(ResId))
            {
                var mb = new Windows.CustomMessageBox("Заказ успешно отменён! Средства вернулись на ваш баланс.", "Успех");
                mb.ShowDialog();
                ToAccPage();

            }
            else
            {
                var mb = new Windows.CustomMessageBox("Ошибка", "Ошибка");
                mb.ShowDialog();
                ToAccPage();
            }
        }
        public void CancelFoodOrderConfirm(int FoodOrdId)
        {
            if (foodServ.CancelFoodOrder(FoodOrdId))
            {
                var mb = new Windows.CustomMessageBox("Заказ успешно отменён! Средства вернулись на ваш баланс.", "Успех");
                mb.ShowDialog();
                ToAccPage();
            }
            else 
            {
                var mb = new Windows.CustomMessageBox("Ошибка", "Ошибка");
                mb.ShowDialog();
                ToAccPage();
            }
        }

        public void UpdateUserInfo(List<ReservationModel> rez, List<UserModel> users,List<FoodOrderModel> foods)
        {
            var curUser = users.Where(u => u.Id == authServ.GetCurrentUser().id).FirstOrDefault();

            if (curUser.UserType=="Client")
            {
                    Reservations = new ObservableCollection<ReservationsListForAdmin>();
                    foreach(var res in crudServ.GetReservationsByClient(curUser.Id))
                    {
                    Reservations.Add(new ReservationsListForAdmin()
                        {
                            Id = res.Id,
                            EndDateTime = res.EndDateTime,
                            PlaceID = res.PlaceID,
                            ReservationStatus = res.ReservationStatus,
                            StartDateTime = res.StartDateTime,
                            TotalPrice = res.TotalPrice
                        });
                    }   
                    FoodOrders = new ObservableCollection<FoodOrdersWithNames>();
                    foreach (var order in foods.Where(r => r.UserID == curUser.Id).ToList())
                    {
                        FoodOrders.Add(new FoodOrdersWithNames()
                        {
                            Id = order.Id,
                            FoodName = crudServ.GetAllFood().Where(f => f.Id == order.FoodId).FirstOrDefault().FoodInfo,
                            OrderDateTime = order.OrderDateTime,
                            OrderStatus = order.OrderStatus,
                            TotalPrice = order.TotalPrice,
                            FoodCount = order.FoodCount

                        });
                    }

            }
            else if (curUser.UserType == "Administrator")
            {
                Reservations = new ObservableCollection<ReservationsListForAdmin>();
                foreach(var res in rez)
                {
                    Reservations.Add(new ReservationsListForAdmin()
                    {
                        Id = res.Id,
                        ClientName = users.Where(u => u.Id == res.UserID).FirstOrDefault().FirstName,
                        EndDateTime=res.EndDateTime,
                        PlaceID=res.PlaceID,
                        ReservationStatus=res.ReservationStatus,
                        StartDateTime=res.StartDateTime,
                        TotalPrice=res.TotalPrice
                    }) ;
                }

                FoodOrders = new ObservableCollection<FoodOrdersWithNames>();
                foreach (var order in foods)
                {
                    FoodOrders.Add(new FoodOrdersWithNames()
                    {
                        Id = order.Id,
                        FoodName = crudServ.GetAllFood().Where(f => f.Id == order.FoodId).FirstOrDefault().FoodInfo,
                        OrderDateTime = order.OrderDateTime,
                        OrderStatus = order.OrderStatus,
                        TotalPrice = order.TotalPrice,
                        FoodCount = order.FoodCount

                    });
                }
            }
            FirstName = curUser.FirstName;
            UserName = curUser.Username;
            Bonuses = curUser.Bonuses;
            Balance = curUser.Balance;
        }

        public void ToAccPage()
        {
            mainWindow.ToAccPage();
        }
        public void ConfirmReservationByAdminMethod(int id)
        {
            if(resServ.ConfirmReservation(id))
            {
                var mb = new Windows.CustomMessageBox("Подтверждение успешно", "Успех");
                mb.ShowDialog();
                ToAccPage();
            }
            else
            {
                var mb = new Windows.CustomMessageBox("Произошла ошибка, подтверждение не удалось", "Успех");
                mb.ShowDialog();
                ToAccPage();
            }
        }
        public void ConfirmFoodOrderByAdminMethod(int id)
        {
            if (foodServ.ConfirmFoodOrder(id))
            {
                var mb = new Windows.CustomMessageBox("Подтверждение успешно", "Успех");
                mb.ShowDialog();

                ToAccPage();
            }
            else
            {
                var mb = new Windows.CustomMessageBox("Произошла ошибка, подтверждение не удалось", "Успех");
                mb.ShowDialog();
                ToAccPage();
            }
        }
        public void ToLeftPage()
        {
            mainWindow.ToAuthPage();
        }
    }
}
