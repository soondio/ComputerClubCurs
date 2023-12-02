using BLL.Models;
using ConstractCurs.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace ConstractCurs.Windows
{
    /// <summary>
    /// Логика взаимодействия для ComputerInfoWindow.xaml
    /// </summary>
    public partial class ComputerInfoWindow : Window 
    {
        private ViewModel.ComputerViewModel VM;
        public ComputerInfoWindow(List<VideoCardModel> vid, List<MonitorModel> mon, List<HeadphonesModel> head, List<KeyboardModel> key, List<MouseModel> mos, List<ProcessorModel> proc, List<RAMModel> ram, List<ComputerCompositionModel> comps, List<ComputerPlaceModel> pl, int id)
        {
            VM = new ViewModel.ComputerViewModel(vid, mon, head, key, mos, proc, ram, comps, pl, id,this);
            DataContext = VM;
            InitializeComponent();
           
        }
    }
}
