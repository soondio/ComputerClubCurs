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
using BLL.Interfaces;
using ConstractCurs.ViewModel;
using BLL.Models;

namespace ConstractCurs.View
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccPage.xaml
    /// </summary>
    public partial class PersonalAccPage : Page
    {
        private ViewModel.PersonalAccViewModel VM;
        public PersonalAccPage(IFoodOrderService foodserv,IDbCrud crudServ,List<FoodOrderModel> foodOrders,IReservationService rezServ,IAuthorizationService authServ,MainViewModel mainWindow,List<ReservationModel> rez,List<BLL.UserModel>users)
        {
            VM = new PersonalAccViewModel(foodserv,crudServ,foodOrders,rezServ,mainWindow, authServ,rez,users);
            DataContext = VM;
            InitializeComponent();
        }

    }
}
