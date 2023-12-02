using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class FoodOrderModel
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public DateTime? OrderDateTime { get; set; }

        public decimal? TotalPrice { get; set; }
        public int FoodCount { get; set; }
        public int FoodId { get; set; }
        public string OrderStatus { get; set; }
        public bool InBasket { get; set; }

        public FoodOrderModel() { }
        public FoodOrderModel(DAL.EntitiesCodeFirst.FoodOrders FOrd)
        {
            Id = FOrd.Id;
            UserID = FOrd.UserID;
            OrderDateTime = FOrd.OrderDateTime;
            FoodId = FOrd.FoodId;
            FoodCount = FOrd.FoodCount;
            TotalPrice = FOrd.TotalPrice;
            OrderStatus = FOrd.OrderStatus;
            InBasket = false;
        }

    }
}
