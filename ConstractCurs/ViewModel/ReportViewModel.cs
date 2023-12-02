using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using System.ComponentModel;
namespace ConstractCurs.ViewModel
{
    public class ReportViewModel:INotifyPropertyChanged
    {
        private IReportService reportService;


        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _RealCount = 0;
        private int _PotencionalCount = 0;
        private decimal? _PotencionalMoney = 0;
        private decimal? _RealMoney = 0;

        private int _RealСountFoodOrders = 0;
        private int _PotencionalСountFoodOrders = 0;
        private decimal? _PotencionalMoneyFoodOrders = 0;
        private decimal? _RealMoneyFoodOrders = 0;

        private decimal? _TotalPotencionalMoney = 0;
        private decimal? _TotalRealMoney = 0;

        #region ResirvationData
        public int RealCount
        {
            get { return _RealCount; }
            set
            {
                _RealCount = value;
                NotifyPropertyChanged("RealCount");
            }
        }
        public int PotencionalCount
        {
            get { return _PotencionalCount; }
            set
            {
                _PotencionalCount = value;
                NotifyPropertyChanged("PotencionalCount");
            }
        }

        public decimal? RealMoney
        {
            get { return _RealMoney; }
            set
            {
                _RealMoney = value;
                NotifyPropertyChanged("RealMoney");
            }
        }
        public decimal? PotencionalMoney
        {
            get { return _PotencionalMoney; }
            set 
            { 
                _PotencionalMoney = value;
                NotifyPropertyChanged("PotencionalMoney");
            }
        }
        #endregion

        #region FoodData
        public int RealСountFoodOrders
        {
            get { return _RealСountFoodOrders; }
            set
            {
                _RealСountFoodOrders = value;
                NotifyPropertyChanged("RealСountFoodOrders");
            }
        }
        public int PotencionalСountFoodOrders
        {
            get { return _PotencionalСountFoodOrders; }
            set
            {
                _PotencionalСountFoodOrders = value;
                NotifyPropertyChanged("PotencionalСountFoodOrders");
            }
        }

        public decimal? RealMoneyFoodOrders
        {
            get { return _RealMoneyFoodOrders; }
            set
            {
                _RealMoneyFoodOrders = value;
                NotifyPropertyChanged("RealMoneyFoodOrders");
            }
        }
        public decimal? PotencionalMoneyFoodOrders
        {
            get { return _PotencionalMoneyFoodOrders; }
            set
            {
                _PotencionalMoneyFoodOrders = value;
                NotifyPropertyChanged("PotencionalMoneyFoodOrders");
            }
        }
        #endregion
        #region TotalData
        public decimal? TotalPotencionalMoney
        {
            get { return _TotalPotencionalMoney; }
            set
            {
                _TotalPotencionalMoney = value;
                NotifyPropertyChanged("TotalPotencionalMoney");
            }
        }

        public decimal? TotalRealMoney
        {
            get { return _TotalRealMoney; }
            set
            {
                _TotalRealMoney = value;
                NotifyPropertyChanged("TotalRealMoney");
            }
        }

        #endregion

        #endregion
        #region Command
        private RelayCommand _GetReportByDate;
        public RelayCommand GetReportByDate
        {
            get
            {
                return _GetReportByDate ?? (_GetReportByDate = new RelayCommand(obj =>
                  {
                      var values = (object[])obj;
                      DateTime from = (DateTime)values[0];
                      DateTime to = (DateTime)values[1];

                      if (from > to || from == to)
                      {
                          var mb = new Windows.CustomMessageBox("Неверный выбор даты", "Ошибка");
                          mb.ShowDialog();
                      }
                      else
                      {
                          CalculateReportByDate(from, to);
                      }
                  }));
            }
        }

        #endregion
        public ReportViewModel(IReportService reportServ)
        {
            this.reportService = reportServ;
        }


        public void CalculateReportByDate(DateTime from, DateTime to)
        {
            var report = reportService.GetReport(from, to);
            var reportFood = reportService.GetReportByFood(from, to);
            if (report.PotencionalCount == 0 && reportFood.PotencionalCount==0)
            {
                var mb = new Windows.CustomMessageBox("Не найдено ни одного заказа", "Ошибка");
                mb.ShowDialog();
            }
            else
            {
                RealCount = report.RealCount;
                PotencionalCount = report.PotencionalCount;
                PotencionalMoney = report.PotencionalMoney ?? 0;
                RealMoney = report.RealMoney ?? 0;

                RealСountFoodOrders = reportFood.RealCount;
                PotencionalСountFoodOrders = reportFood.PotencionalCount;
                PotencionalMoneyFoodOrders = reportFood.PotencionalMoney ?? 0;
                RealMoneyFoodOrders = reportFood.RealMoney ?? 0;

                TotalPotencionalMoney = report.PotencionalMoney + reportFood.PotencionalMoney ?? 0;
                TotalRealMoney = report.RealMoney + reportFood.RealMoney ?? 0;

            }
        }
    }
}
