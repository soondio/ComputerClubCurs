using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class FoodModel
    {
        public int Id { get; set; }
        public string FoodInfo { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public FoodModel() { }

        public FoodModel(DAL.EntitiesCodeFirst.Food f)
        {
            Id = f.Id;
            FoodInfo = f.FoodInfo;
            Count = f.Count;
            Price = f.Price;
        }
    }
}
