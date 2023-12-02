using DAL.EntitiesCodeFirst;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class FoodOrdersRepositorySQL:IRepository<FoodOrders>
    {
        private ComputerClubContext db;

        public FoodOrdersRepositorySQL(ComputerClubContext dbcontext)
        {
            this.db = dbcontext;
        }
        public List<FoodOrders> GetList()
        {
            return db.FoodOrders.ToList();
        }
        public FoodOrders GetItem(int id)
        {
            return db.FoodOrders.Find(id);
        }

        public void Create(FoodOrders FoodOrders)
        {
            db.FoodOrders.Add(FoodOrders);
        }
        public void Update(FoodOrders FoodOrders)
        {
            db.Entry(FoodOrders).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(int id)
        {
            FoodOrders FoodOrders = db.FoodOrders.Find(id);
            if (FoodOrders != null)
            {
                db.FoodOrders.Remove(FoodOrders);
            }
        }
    }
}
