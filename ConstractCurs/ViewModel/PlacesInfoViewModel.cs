using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using BLL.Interfaces;
using System.ComponentModel;

namespace ConstractCurs.ViewModel
{
    public class PlacesInfoViewModel:INotifyPropertyChanged
    {

        private MainViewModel mainWindow;

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        private List<ComputerPlaceModel> _Places;
        public List<ComputerPlaceModel> Places
        {
            get { return _Places; }
            set
            {
                _Places = value;
                NotifyPropertyChanged("Places");
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



        public PlacesInfoViewModel(MainViewModel mainWindow, List<ComputerPlaceModel> pl,List<ComputerCompositionModel> comps)
        {
            this.mainWindow = mainWindow;
            Places = pl;
        }


        public void ToComputerPage(int id)
        {
            mainWindow.ToComputerPage(id);
        }
    }
}
