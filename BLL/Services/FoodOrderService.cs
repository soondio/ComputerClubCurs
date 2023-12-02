using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.EntitiesCodeFirst;

namespace BLL.Services
{
    public class FoodOrderService:IFoodOrderService
    {
        private IDbRepos db;
        public FoodOrderService(IDbRepos repos,IDbCrud dbcrud)
        {
            db = repos;
        }


        public bool MakeFoodOrder(FoodOrderModel foodorderModel, int UserId)
        {
            Food food = db.Foods.GetItem(foodorderModel.FoodId);
            Users curUser = db.Users.GetItem(UserId);
            if(foodorderModel.FoodCount>food.Count)
            {
                throw new Exception("Недостаточно еды на складе");
            }
            decimal totalprice = food.Price * foodorderModel.FoodCount;
            if(curUser.Balance+curUser.Bonuses<totalprice)
            {
                throw new Exception("Недостаточно баланса");
            }
            FoodOrders fo = new FoodOrders
            {
                TotalPrice = totalprice,
                UserID = curUser.Id,
                OrderDateTime = foodorderModel.OrderDateTime,
                FoodCount = foodorderModel.FoodCount,
                FoodId = foodorderModel.FoodId,
                OrderStatus = foodorderModel.OrderStatus
            };
            food.Count -= foodorderModel.FoodCount;
            curUser.Balance = curUser.Balance - totalprice;
            db.FoodOrders.Create(fo);
            curUser.Bonuses += totalprice * (decimal)0.05f;

            if (db.Save() > 0)
                return true;
            return false;
        }
        public bool CancelFoodOrder(int id)
        {

            FoodOrders curFoodOrder = db.FoodOrders.GetItem(id);
            Food curFud = db.Foods.GetItem(curFoodOrder.FoodId);
            curFud.Count += curFoodOrder.FoodCount;
            Users curUser = db.Users.GetItem(curFoodOrder.UserID);

            curFoodOrder.OrderStatus = "отменён";
            curUser.Balance += curFoodOrder.TotalPrice;

            if (db.Save() > 0)
                return true;
            return false;
        }
        

        public bool ConfirmFoodOrder(int id)
        {
            FoodOrders curFoodOrder = db.FoodOrders.GetItem(id);
            curFoodOrder.OrderStatus = "выполнен";
            if (db.Save() > 0)
                return true;
            return false;
        }
        public bool UpCountFood(int foodId, int count)
        {
            Food curFood = db.Foods.GetItem(foodId);
            curFood.Count += count;
            if (db.Save() > 0)
                return true;
            return false;
        }
    }
}
