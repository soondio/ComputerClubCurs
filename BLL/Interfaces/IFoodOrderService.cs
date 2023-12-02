using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IFoodOrderService
    {
        bool MakeFoodOrder(FoodOrderModel FoodOrderModel, int UserId);
        bool CancelFoodOrder(int id);
        bool ConfirmFoodOrder(int id);
        bool UpCountFood(int idfood, int count);

    }
}
