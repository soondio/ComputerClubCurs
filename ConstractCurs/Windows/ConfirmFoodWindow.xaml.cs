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
using System.Windows.Shapes;
using BLL.Interfaces;

namespace ConstractCurs.Windows
{
    /// <summary>
    /// Логика взаимодействия для ConfirmFoodWindow.xaml
    /// </summary>
    public partial class ConfirmFoodWindow : Window
    {
        private ViewModel.FoodConfirmViewModel VM;
        public ConfirmFoodWindow(List<BLL.Models.FoodModel> food,IAuthorizationService authServ,IFoodOrderService foodServ,DateTime orderTime,int id)
        {
            VM = new ViewModel.FoodConfirmViewModel(food,authServ, foodServ, orderTime, id,this);
            DataContext = VM;
            InitializeComponent();
        }
    }
}
