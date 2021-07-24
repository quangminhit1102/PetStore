using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.DAO;
using System.IO;
using PetStore.Models.Common;

namespace PetStore.Areas.Admin.Controllers
{
    public class AdProductController : Controller
    {
        // GET: Admin/Product
        PetStoreDbContext db = null;
        public ActionResult ListProduct(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        //CREATE PRODUCT ======================================================================
        [HttpGet]
        public ActionResult CreateProduct()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateProduct([Bind(Include = "Name,CateId,BrandId,Price,Description")] Product product, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            SetViewBag();
            if(ModelState.IsValid)
            {
                db = new PetStoreDbContext();
                product.SiteTile = FriendlyURL.URLFriendly(product.Name);
                product.CreatedAt = DateTime.Now;
                product.CreatedBy = ((UserLogin)Session["AD"]).UserID;
                product.Status = true;
                product.ViewCount = 0;
                product.Evaluate = 0f;
                product.TotalEvaluate = 0;
                db.Products.Add(product);
                db.SaveChanges();
                //Lưu từng ảnh nêu có nhập ảnh
                int ProId = db.Products.Max(x=>x.Id);//Lấy Id của sản phẩm mới vừa thêm vào
                if (file1 != null)
                {
                    string fileName1 = SaveImage(file1);
                    var img1 = new ProductImage();
                    img1.ProductId = ProId;
                    img1.Image = fileName1;
                    db.ProductImages.Add(img1);
                    db.SaveChanges();
                }
                if (file2 != null)
                {
                    string fileName2 = SaveImage(file2);
                    var img2 = new ProductImage();
                    img2.ProductId = ProId;
                    img2.Image = fileName2;
                    db.ProductImages.Add(img2);
                    db.SaveChanges();
                }
                if (file3 != null)
                {
                    string fileName3 = SaveImage(file3);
                    var img3 = new ProductImage();
                    img3.ProductId = ProId;
                    img3.Image = fileName3;
                    db.ProductImages.Add(img3);
                    db.SaveChanges();
                }

                ViewBag.success = "Tạo mới sản phẩm thành công!";
                return View(product);
            }
            else
            {
                return View(product);
            }
        }
        public void SetViewBag()
        {
            db = new PetStoreDbContext();
            var cate0 = db.Categories.Where(x => x.ParentId == null).ToList();
            ViewBag.cate0 = new SelectList(cate0, "Id", "Name");
            var brand = db.Brands.OrderBy(x => x.Name).ToList();
            ViewBag.brand = new SelectList(brand, "Id", "Name");
        }
        public string SaveImage(HttpPostedFileBase fileUpload)
        {
            //Image
            string fileName = Path.GetFileNameWithoutExtension(fileUpload.FileName);
            string extension = Path.GetExtension(fileUpload.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string ImagePath= "/Content/Images/Products/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Content/Images/Products/"), fileName);
            try
            {
                fileUpload.SaveAs(fileName);
            }
            catch (Exception ex)
            {

            }
            return ImagePath;
        }
        public JsonResult GetCate1(int Id)
        {
            db = new PetStoreDbContext();
            db.Configuration.ProxyCreationEnabled = false;
            var cate1 = db.Categories.Where(x => x.ParentId == Id).ToList();
            ViewBag.cate1 = new SelectList(cate1, "Id", "Name");
            return Json(cate1, JsonRequestBehavior.AllowGet);
        } 
        //EDIT PRODUCT============================================================
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            db = new PetStoreDbContext();
            var product = db.Products.Find(id);
            SetViewBag();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditProduct([Bind(Include = "Id,Name,CateId,BrandId,Price,Description")] Product product, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            SetViewBag();
            db = new PetStoreDbContext();
            if(ModelState.IsValid)
            {
                var editPro = db.Products.Where(x => x.Id == product.Id).SingleOrDefault();
                if (editPro == null)
                    return HttpNotFound();
                //Xóa hình ảnh cũ trong bảng ProductImage
                var delImg = db.ProductImages.Where(x => x.ProductId == product.Id);
                foreach (var item in delImg)
                {
                    db.ProductImages.Remove(item);
                }
                db.SaveChanges();
                //Sửa thông tin sản phẩm 
                editPro.Name = product.Name;
                editPro.CateId = product.CateId;
                editPro.BrandId = product.BrandId;
                editPro.Price = product.Price;
                editPro.Description = product.Description;
                editPro.SiteTile = FriendlyURL.URLFriendly(product.Name);
                editPro.UpdatedAt = DateTime.Now;
                editPro.UpdatedBy = ((UserLogin)Session["AD"]).UserID;
                db.SaveChanges();
                //Lưu từng ảnh nêu có nhập ảnh
                if (file1 != null)
                {
                    string fileName1 = SaveImage(file1);
                    var img1 = new ProductImage();
                    img1.ProductId = product.Id;
                    img1.Image = fileName1;
                    db.ProductImages.Add(img1);
                    db.SaveChanges();
                }
                if (file2 != null)
                {
                    string fileName2 = SaveImage(file2);
                    var img2 = new ProductImage();
                    img2.ProductId = product.Id;
                    img2.Image = fileName2;
                    db.ProductImages.Add(img2);
                    db.SaveChanges();
                }
                if (file3 != null)
                {
                    string fileName3 = SaveImage(file3);
                    var img3 = new ProductImage();
                    img3.ProductId = product.Id;
                    img3.Image = fileName3;
                    db.ProductImages.Add(img3);
                    db.SaveChanges();
                }
                ViewBag.success = "Sửa Sản Phẩm thành công!";
                return View(product);
            }
            return View();
        }
            //DELETE PRODUCT==========================================================
            public ActionResult DeleteProduct(int id)
        {
            db = new PetStoreDbContext();
            //Xóa ảnh SP
            var allimg = db.ProductImages.Where(x => x.ProductId == id).ToList();
            foreach(var item in allimg)
            {
                db.ProductImages.Remove(item);
            }
            db.SaveChanges();
            //Xóa SP
            var delPro = db.Products.Where(x => x.Id == id).SingleOrDefault();
            db.Products.Remove(delPro);
            db.SaveChanges();
            return RedirectToAction("ListProduct");
        }
        
    }
}