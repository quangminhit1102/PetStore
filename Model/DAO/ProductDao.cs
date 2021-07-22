using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductDao
    {
        PetStoreDbContext db = null;
        public ProductDao()
        {
            db = new PetStoreDbContext();
        }


        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }
    }
}