using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Controllers
{
    public class PostController : Controller
    {
        PetStoreDbContext db = null;
        // GET: Post
        public ActionResult Index()
        {
            db = new PetStoreDbContext();
            var listPost = db.Posts.ToList();
            return View(listPost);
         
        }
        public ActionResult Detail(int id)
        {
            db = new PetStoreDbContext();
            var Post = db.Posts.Find(id);
            var catelog = db.Catalogs.Where(x => x.Id == Post.CataId).FirstOrDefault();
            ViewBag.catelog = catelog.Title;
            return View(Post);
        }
        public ActionResult ListPostByCata(int id)
        {
            db = new PetStoreDbContext();
            var listPost = db.Posts.Where(x => x.CataId == id).ToList();
            var catelog = db.Catalogs.Find(id);
            ViewBag.catelog = catelog.Title;
            return View(listPost);
        }
        public ActionResult ListCatalog()
        {
            db = new PetStoreDbContext();

            var listPost = db.Posts.ToList();
            var listCatalog = db.Catalogs.Where(x=>x.Status == true).ToList();
            foreach (var item in listCatalog)
            {
                item.Total = listPost.Where(x => x.CataId == item.Id).Count();
            }
            return View(listCatalog);
        }
    }
}