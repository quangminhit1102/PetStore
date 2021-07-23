using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
namespace Model.DAO
{
    
    public class PostDao
    {
        PetStoreDbContext db = null;
        public PostDao()
        {
            db = new PetStoreDbContext();
        }
        public IEnumerable<Post> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Content.Contains(searchString) || x.Catalog.Title.Contains(searchString));
            }
            return model.OrderByDescending(x => x.UpdatedAt).ThenByDescending(x => x.CreatedAt).ToPagedList(page, pageSize);
        }
    }
}
