using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;
using BLL.Models;
using BLL.Interfaces;
using BLL;

namespace ConstractCurs.ViewModel
{

    public class ReservationViewModel:INotifyPropertyChanged
    {
        private IReservationService resServ;
        private IAuthorizationService authServ;
        private MainViewModel mainWindow;
        private List<UserModel> Users;
        private UserModel curUser;
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private decimal _PricePerHour;
        public decimal PricePerHour
        {
            get { return _PricePerHour; }
            set
            {
                _PricePerHour = value;
                NotifyPropertyChanged("PricePerHour");
            }
        }


        private string _PlacesId;
        public string PlacesId
        {
            get { return _PlacesId; }
            set
            {
                _PlacesId = value;
                NotifyPropertyChanged("PlacesId");
            }
        }

        private List<ComputerPlaceModel> _Computers;
        public List<ComputerPlaceModel> Computers
        {
            get { return _Computers; }
            set
            {
                _Computers = value;
                NotifyPropertyChanged("Computers");
            }
        }


        


        #endregion
        #region Command

        private RelayCommand _ReservAccept;
        public RelayCommand ReservAccept
        {
            get
            {
                return _ReservAccept ?? (_ReservAccept = new RelayCommand(obj =>
                {
                    bool check = false;
                    var values = (object[])obj;
                    foreach(var c in values)
                    {
                        if (c == null) check = true;
                    }
                    if (check)
                    {
                        var mb = new Windows.CustomMessageBox("Выберите все даты", "Ошибка");
                        mb.ShowDialog();
                    }
                    else
                    {
                        DateTime DateStart = (DateTime)values[0];
                        DateTime TimeStart = (DateTime)values[1];
                        DateTime TimeEnd = (DateTime)values[2];
                        DateTime DateEnd = DateStart.Date.AddHours(TimeEnd.Hour);
                        DateStart = DateStart.Date;
                        DateStart = DateStart.AddHours(TimeStart.Hour);
                        if (DateStart > DateEnd || DateStart == DateEnd || DateStart<DateTime.Now)
                        {
                            var mb = new Windows.CustomMessageBox("Неверный выбор даты", "Ошибка");
                            mb.ShowDialog();
                        }
                        else
                        {
                            ShowFreeComps(DateStart, DateEnd);
                        }
                    }

                }));

            }
        }
        private RelayCommand _ConfirmReservation;
        public RelayCommand ConfirmReservation
        {
            get
            {
                return _ConfirmReservation ?? (_ConfirmReservation = new RelayCommand(obj =>
                {
                    var values = (object[])obj;
                    string placeId = (string)values[0];
                    DateTime DateStart = (DateTime)values[1];
                    DateTime TimeStart = (DateTime)values[2];
                    DateTime TimeEnd = (DateTime)values[3];
                    DateTime DateEnd = DateStart.Date.AddHours(TimeEnd.Hour);
                    DateStart = DateStart.Date;
                    DateStart = DateStart.AddHours(TimeStart.Hour);
                    int userId = authServ.GetCurrentUser().id;
                    if(userId==-1)
                    {
                        var mb = new Windows.CustomMessageBox("Заказы могут совершать только авторизированные пользователи, пожалуйста авторизируйтесь.", "Ошибка");
                        mb.ShowDialog();
                    }
                   
                    else
                    {
                        ReservationModel rez = new ReservationModel
                        {
                            PlaceID = (Int32.Parse((string)placeId)),
                            DateTime = DateStart.Date,
                            StartDateTime = DateStart,
                            EndDateTime = DateEnd,
                            ReservationStatus = "забронировано",
                            TotalPrice = PricePerHour * (DateEnd.Hour - DateStart.Hour),


                        };
                        try
                        {
                            if (resServ.MakeReservation(rez, userId))
                            {
                                var mb = new Windows.CustomMessageBox("Бронирование успешно!", "Ждём вас");
                                mb.ShowDialog();
                                ShowFreeComps(DateStart, DateEnd);
                            }

                        }
                        catch
                        {
                            var mb = new Windows.CustomMessageBox("Недостаточно баланса", "Ошибка");
                            mb.ShowDialog();
                        }
                    }

                }));
            }
        }

        private RelayCommand _tocomputerpage;
        public RelayCommand ToComputerPageCommand
        {
            get 
            {
                return _tocomputerpage ?? (_tocomputerpage = new RelayCommand(obj =>
                  {
                      ToComputerPage(Int32.Parse((string)obj));
                  }));
            }
        }


        #endregion
        public ReservationViewModel(MainViewModel mw,BLL.Interfaces.IReservationService resServ, IAuthorizationService authServ,List<UserModel> Users)
        {
            this.Users = Users;
            this.authServ = authServ;
            this.resServ = resServ;
            this.mainWindow = mw;
        }

        public void ShowFreeComps(DateTime start, DateTime end)
        {
            if(start!=null && end!=null)
            {
                
                var comps = resServ.CheckFreeComputers(start, end);
                Computers = comps;

            }
        }

        public void ToComputerPage(int id)
        {
            mainWindow.ToComputerPage(id);
        }


    }
}
