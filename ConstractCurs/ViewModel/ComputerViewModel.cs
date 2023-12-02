using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BLL.Models;

namespace ConstractCurs.ViewModel
{
    public class ComputerViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private RelayCommand toreservationpage;
        public RelayCommand ToReservationPageCommand
        {
            get
            {
                return toreservationpage ?? (toreservationpage = new RelayCommand(obj =>
                {
                    ToReservationPage();
                }));
            }
        }



        public ComputerPlaceModel placeModel { get; set; }
        private int placeId;
        private List<ComputerPlaceModel> Places;
        public ComputerCompositionModel CompModel { get; set; }
        private Windows.ComputerInfoWindow okno;
        public VideoCardModel videoCardModel { get; set; }
        public MonitorModel monitorModel { get; set; }
        public HeadphonesModel headphonesModel { get; set; }
        public KeyboardModel keyboardModel { get; set; }
        public MouseModel mouseModel { get; set; }
        public ProcessorModel processorModel { get; set; }
        public RAMModel ramModel { get; set; }
        public ComputerViewModel(List<VideoCardModel> vid, List<MonitorModel>mon,List<HeadphonesModel> head,List<KeyboardModel> key,List<MouseModel> mos,List<ProcessorModel>proc,List<RAMModel> ram,List<ComputerCompositionModel> comps,List<ComputerPlaceModel> pl, int id,Windows.ComputerInfoWindow okno)
        {
            this.Places = pl;
            placeId = id;
            placeModel = pl.Where(i => i.Id == placeId).FirstOrDefault();
            this.okno = okno;
            CompModel = comps.Where(p => p.PlaceId == placeId).FirstOrDefault();
            videoCardModel = vid.Where(i => i.Id == CompModel.VideoCardID).FirstOrDefault();
            monitorModel = mon.Where(i => i.Id == CompModel.MonitorID).FirstOrDefault();
            headphonesModel = head.Where(i => i.Id == CompModel.HeadphonesID).FirstOrDefault();
            mouseModel = mos.Where(i => i.Id == CompModel.MouseID).FirstOrDefault();
            processorModel = proc.Where(i => i.Id == CompModel.ProcessorID).FirstOrDefault();
            ramModel = ram.Where(i => i.Id == CompModel.RAMID).FirstOrDefault();

        }


        public void ToReservationPage()
        {
            okno.Close();
        }

    }
}
