using Model.EF;
using PagedList;
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
        PetStoreDbContext db = new PetStoreDbContext();
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Brand.Name.Contains(searchString) || x.Category.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedAt).ToPagedList(page, pageSize);
        }
    }
}
