using Model.EF;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetStore.Models.Common;
using System.IO;
using System.Data.Entity.Migrations;

namespace PetStore.Areas.Admin.Controllers
{
    public class AdPostController : Controller
    {
        // GET: Admin/Post
        PetStoreDbContext db = null;
        public ActionResult ListPost(string searchString, int page = 1, int pageSize = 5)
        {
            PostDao dao = new PostDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }
        //Edit Post=================================================================
        [HttpGet]
        public ActionResult EditPost(int? id)
        {
            db = new PetStoreDbContext();
            var listCata = db.Catalogs.ToList();
            ViewBag.listCata = listCata;
            Post post = db.Posts.SingleOrDefault(x => x.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditPost([Bind(Include = "Id,CataId,Title,Content")] Post post, HttpPostedFileBase fileUpload)
        {
            db = new PetStoreDbContext();
            var listCata = db.Catalogs.ToList();
            ViewBag.listCata = listCata;
            if (ModelState.IsValid)
            {
                var editpost = db.Posts.Where(x => x.Id == post.Id).SingleOrDefault();
                if (editpost == null)
                    return HttpNotFound();
                if (fileUpload!=null)
                {
                    //upload Image
                    string fileName = Path.GetFileNameWithoutExtension(fileUpload.FileName);
                    string extension = Path.GetExtension(fileUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    editpost.Image = "/Content/Images/Posts/" + fileName;
                    fileName = Path.Combine(Server.MapPath("/Content/Images/Posts/"), fileName);
                    try
                    {
                        fileUpload.SaveAs(fileName);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                editpost.Title = post.Title;
                editpost.CataId = post.CataId;
                editpost.Content = post.Content;
                editpost.UpdatedAt = DateTime.Now;
                editpost.UpdatedBy = ((UserLogin)Session["USER"]).UserID;
                editpost.SiteTitle = FriendlyURL.URLFriendly(post.Title);
                editpost.Status = true;
                db.Posts.AddOrUpdate(editpost);
                db.SaveChanges();
                ViewBag.success = "Sửa bài viết thành công";
                return View(editpost);
            }
            //Goi lại viewbag tránh trường hợp bị null
            //db = new PetStoreDbContext();
            //var listCata = db.Catalogs.ToList();
            //ViewBag.listCata = listCata;
            return View(post);

        }
        //Create Post=======================================================
        [HttpGet]
        public ActionResult CreatePost()
        {
            db = new PetStoreDbContext();
            var listCata = db.Catalogs.ToList();
            ViewBag.listCata = listCata;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreatePost([Bind(Include = "CataId,Title,Content")] Post post, HttpPostedFileBase fileUpload)
        {
            db = new PetStoreDbContext();
            var listCata = db.Catalogs.ToList();
            ViewBag.listCata = listCata;
            if (ModelState.IsValid)
            {
                //Image
                string fileName = Path.GetFileNameWithoutExtension(fileUpload.FileName);
                string extension = Path.GetExtension(fileUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                post.Image = "/Content/Images/Posts/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Content/Images/Posts/"), fileName);
                try
                {
                    fileUpload.SaveAs(fileName);
                }
                catch (Exception ex)
                {

                }
                post.CreatedAt = DateTime.Now;
                post.CreatedBy = ((UserLogin)Session["USER"]).UserID;
                post.SiteTitle = FriendlyURL.URLFriendly(post.Title);
                post.Status = true;
                db.Posts.Add(post);
                db.SaveChanges();
                ViewBag.success = "Tạo bài viết thành công";
                return View();
            }
            else
            {
                return View(post);
            }     
        }
        //Delete Post=======================================================
        public ActionResult Delete(int id)
        {
            db = new PetStoreDbContext();
            var delpost = db.Posts.Where(x => x.Id == id).SingleOrDefault();
            if (delpost == null) return HttpNotFound();
            else
            {
                db.Posts.Remove(delpost);
                db.SaveChanges();
            }
            return RedirectToAction("Listpost");
        }
    }
}