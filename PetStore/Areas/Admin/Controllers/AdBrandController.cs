using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Areas.Admin.Controllers
{
    public class AdBrandController : Controller
    {
        // GET: Admin/Brand
        PetStoreDbContext db = null;
        [Route]
        public ActionResult Index()
        {
            db = new PetStoreDbContext();
            var listBrand = db.Brands.ToList();
            return View(listBrand);
        }
        //CreateBrand =========================================
        [HttpGet]
        public ActionResult CreateBrand()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateBrand([Bind(Include = "Name,Nation,Description")] Brand brand, HttpPostedFileBase fileUpload)
        {
            db = new PetStoreDbContext();
            if (ModelState.IsValid)
            {
                //Image
                string fileName = Path.GetFileNameWithoutExtension(fileUpload.FileName);
                string extension = Path.GetExtension(fileUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                brand.Image = "/Content/Images/Posts/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Content/Images/Posts/"), fileName);
                try
                {
                    fileUpload.SaveAs(fileName);
                }
                catch (Exception ex)
                {

                }
                brand.Status = true;
                db.Brands.Add(brand);
                db.SaveChanges();
                ViewBag.success = "Thêm mới thương hiệu thành công!";
                return View(brand);
            }  
            return View();
        }
    }
}