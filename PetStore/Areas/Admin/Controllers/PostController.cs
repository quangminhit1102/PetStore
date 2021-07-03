using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        // GET: Admin/Post
        PetStoreDbContext db = null;
        public ActionResult ListPost()
        {
            db = new PetStoreDbContext();
            List<Post> listPost = db.Posts.OrderByDescending(x => x.CreatedAt).ToList();
            return View(listPost);
        }
        //Edit Post
        public ActionResult EditPost(int? Post_ID)
        {
            db = new PetStoreDbContext();
            Post post = db.Posts.SingleOrDefault(x => x.Id == Post_ID);
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
            return View("ListPost");
        }
        //Create Post
        public ActionResult CreatePost(int? Post_ID)
        {
            var post = db.Posts.SingleOrDefault(x => x.Id == Post_ID);
            return View(post);
        }
    }
}