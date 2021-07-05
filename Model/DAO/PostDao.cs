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
        public IEnumerable<Post> listAllPaging(int page, int pageSize)
        {
            return db.Posts.OrderByDescending(x=>x.CreatedAt).ToPagedList(page, pageSize);
        }
    }
}
