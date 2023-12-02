using BLL.Interfaces;
using ConstractCurs.ViewModel;
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

namespace ConstractCurs.View
{
    /// <summary>
    /// Логика взаимодействия для FoodOrderPage.xaml
    /// </summary>
    public partial class FoodOrderPage : Page
    {
        private FoodOrderViewModel VM;
        public FoodOrderPage(IAuthorizationService authServ, MainViewModel mainViewModel, IFoodOrderService foodServ,List<FoodModel>f)
        {
            VM = new ViewModel.FoodOrderViewModel(authServ,mainViewModel,foodServ,f);
            DataContext = VM;
            InitializeComponent();
        }
    }
}
