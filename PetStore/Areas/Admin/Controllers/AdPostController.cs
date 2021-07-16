using Model.EF;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Areas.Admin.Controllers
{
    public class AdPostController : Controller
    {
        // GET: Admin/Post
        PetStoreDbContext db = null;
        public ActionResult ListPost(string searchString,int page = 1, int pageSize = 5)
        {
            PostDao dao = new PostDao();
            var model = dao.ListAllPaging(searchString,page, pageSize);
            return View(model);
        }
        //Edit Post
        [HttpGet]
        public ActionResult EditPost(int? id)
        {
            db = new PetStoreDbContext();
            Post post = db.Posts.SingleOrDefault(x => x.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost()
        {
            return View();
        }
        //Create Post
        public ActionResult CreatePost()
        {
            db = new PetStoreDbContext();
            var listCata = db.Catalogs.ToList();
            return View(listCata);
        }
    }
}