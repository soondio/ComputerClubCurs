using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntitiesCodeFirst
{
    public partial class Food
    {

        public Food()
        {
            FoodOrders = new HashSet<FoodOrders>();
        }
        public int Id { get; set; }
        public string FoodInfo { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<FoodOrders> FoodOrders { get; set; }
    }
}
