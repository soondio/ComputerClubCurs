using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using BLL.Models;
using System.ComponentModel;

namespace ConstractCurs.ViewModel
{
    public class FoodOrderViewModel:INotifyPropertyChanged
    {
        private IAuthorizationService authServ;
        private MainViewModel mainWindow;
        private IFoodOrderService foodServ;

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<FoodModel> _Foods;
        public List<FoodModel> Foods
        {
            get { return _Foods; }
            set
            {
                _Foods = value;
                NotifyPropertyChanged("Foods");
            }
        }
        #endregion


        #region Command

        private RelayCommand _ToConfirmFoodPage;

        public RelayCommand ToConfirmFoodPage
        {
            get
            {
                return _ToConfirmFoodPage ?? (_ToConfirmFoodPage = new RelayCommand(obj =>
                 {
                     bool check = false;
                     var value = (object[])obj;
                     foreach (var c in value)
                     {
                         if (c == null) check = true;
                     }
                     if (check)
                     {
                         var mb = new Windows.CustomMessageBox("Выберите дату", "Ошибка");
                         mb.ShowDialog();
                     }
                     else
                     {
                         int IdFood = Int32.Parse((string)value[0]);
                         if ((DateTime)value[1] < DateTime.Now)
                         {
                             var mb = new Windows.CustomMessageBox("Дата должна быть не меньше текущей", "Ошибка");
                             mb.ShowDialog();
                         }
                         else
                         {
                             DateTime OrderTime = (DateTime)value[1];
                             OrderTime = OrderTime.Date.AddHours(OrderTime.Hour);
                             ToConfirmFood(IdFood, OrderTime);
                         }
                     }
                 }));
            }
        }


        #endregion

        public FoodOrderViewModel(IAuthorizationService authServ, MainViewModel mainWindow, IFoodOrderService foodServ,List<FoodModel> FoodList)
        {
            Foods = FoodList;
            this.authServ = authServ;
            this.mainWindow = mainWindow;
            this.foodServ = foodServ;
            
        }

        public void ToConfirmFood(int id,DateTime ordertime)
        {
            mainWindow.ToConfirmFood(id,ordertime);

        }
    }
}
