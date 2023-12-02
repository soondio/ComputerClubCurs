using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Models;
using ConstractCurs.ViewModel;

namespace ConstractCurs.View
{
    /// <summary>
    /// Логика взаимодействия для PlacesInfoPage.xaml
    /// </summary>
    public partial class PlacesInfoPage : Page
    {
        private PlacesInfoViewModel VM;
        public PlacesInfoPage(MainViewModel mainWindow,List<ComputerPlaceModel> Places,List<ComputerCompositionModel> Comps)
        {
            VM = new PlacesInfoViewModel(mainWindow,Places,Comps);
            DataContext = VM;
            InitializeComponent();
        }
    }
}
