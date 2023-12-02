using DAL.EntitiesCodeFirst;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class FoodRepositorySQL:IRepository<Food>
    {
        private ComputerClubContext db;

        public FoodRepositorySQL(ComputerClubContext dbcontext)
        {
            this.db = dbcontext;
        }
        public List<Food> GetList()
        {
            return db.Food.ToList();
        }
        public Food GetItem(int id)
        {
            return db.Food.Find(id);
        }

        public void Create(Food Food)
        {
            db.Food.Add(Food);
        }
        public void Update(Food Food)
        {
            db.Entry(Food).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(int id)
        {
            Food Food = db.Food.Find(id);
            if (Food != null)
            {
                db.Food.Remove(Food);
            }
        }
    }
}
