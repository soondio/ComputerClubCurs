using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using System.ComponentModel;
using System.Windows;

namespace ConstractCurs.ViewModel
{
    public class FoodConfirmViewModel:INotifyPropertyChanged
    {
        private IAuthorizationService authServ;
        private IFoodOrderService foodServ;
        private Windows.ConfirmFoodWindow okno;
        private DateTime orderDate;

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        public FoodModel curFood { get; set; }


        private int _CountString = 1;
        public int CountString
        {
            get
            {
                return _CountString;
            }
            set
            {
                _CountString = value;
                NotifyPropertyChanged("CountString");
            }
        }
        private decimal _TotalPrice;
        public decimal TotalPrice
        {
            get
            {
                return _TotalPrice;
            }
            set
            {
                _TotalPrice = value;
                NotifyPropertyChanged("TotalPrice");
            }
        }
        public void UpdateAuth()
        {
            NotifyPropertyChanged("AdminVis");
        }

        #endregion

        #region Command

        private RelayCommand _BackStap;
        public RelayCommand BackStap
        {
            get
            {
                return _BackStap ?? (_BackStap = new RelayCommand(obj =>
                    {
                        CloseWindow();
                    }));
            }
        }

        private RelayCommand _SubCountCommand;
        public RelayCommand SubCountCommand
        {
            get
            {
                return _SubCountCommand ?? (_SubCountCommand = new RelayCommand(obj =>
                  {
                      SubCount();
                  }));
            }
        }
        private RelayCommand _AddCountCommand;
        public RelayCommand AddCountCommand
        {
            get
            {
                return _AddCountCommand ?? (_AddCountCommand = new RelayCommand(obj =>
                {
                    AddCount();
                }));
            }
        }
        private RelayCommand _MakeOrder;
        public RelayCommand MakeOrder
        {
            get
            {
                return _MakeOrder ?? (_MakeOrder = new RelayCommand(obj =>
                    {
                        MakeOrderConfirm();
                    }));
            }
        }

        private RelayCommand _UpCountFood;
        public RelayCommand UpCountFood
        {
            get
            {
                return _UpCountFood ?? (_UpCountFood = new RelayCommand(obj =>
                  {
                      UpCount(curFood.Id,CountString);
                  }));
            }
        }


        #endregion


        public FoodConfirmViewModel(List<FoodModel> allfood,IAuthorizationService authServ,IFoodOrderService foodServ,DateTime orderTime,int id,Windows.ConfirmFoodWindow okno)
        {
            this.okno = okno;
            this.authServ = authServ;
            this.foodServ = foodServ;
            curFood = allfood.Where(f => f.Id == id).FirstOrDefault();
            this.orderDate = orderTime;
            TotalPrice = CountString * curFood.Price;
            this.UpdateAuth();

        }

        public void CloseWindow()
        {
            okno.Close();
        }
        public void UpCount(int Id, int count)
        {
            if(foodServ.UpCountFood(Id, count))
            {
                var mb = new Windows.CustomMessageBox(curFood.FoodInfo+" Успешно пополнен ", "Успешно");
                mb.ShowDialog();
                okno.Close();
            }
        }

        public void SubCount()
        {
            if (CountString != 1)
            {
                CountString--;
                TotalPrice = curFood.Price * CountString;
            }
        }
        public void AddCount()//добавить проверку на конец
        {
            if (CountString < curFood.Count)
            {
                CountString++;
                TotalPrice = curFood.Price * CountString;
            }
        }

        public void MakeOrderConfirm()
        {
            int UserId = authServ.GetCurrentUser().id;

            FoodOrderModel order = new FoodOrderModel
            {
                FoodCount=CountString,
                FoodId=curFood.Id,
                OrderDateTime=orderDate,
                OrderStatus="забронировано"
            };
            try
            {
                if (foodServ.MakeFoodOrder(order, UserId))
                {
                    var mb = new Windows.CustomMessageBox("Заказ выполнен успешно!", "Успешно");
                    mb.ShowDialog();
                    okno.Close();
                }
            }catch
            {
                var mb = new Windows.CustomMessageBox("Недостаточно баланса", "Ошибка");
               mb.ShowDialog();
            }
                
        }

    }
}
