using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PetStore.Controllers
{
 
    public class ProductController : Controller
    {
        PetStoreDbContext db = null;
        public ActionResult ListProductByCate(int CateId)
        {
            db = new PetStoreDbContext();
            ViewBag.CateName = db.Categories.Where(x => x.Id == CateId).FirstOrDefault().Name;
            var listProductbyCate = db.Products.Where(x => x.CateId == CateId).ToList();
            return View(listProductbyCate);
        }

        public ActionResult Detail(int ProId)
        {
            db = new PetStoreDbContext();
            var Product = db.Products.Where(x => x.Id == ProId).FirstOrDefault();
            var subCate = db.Categories.Where(x => x.Id == Product.CateId).SingleOrDefault();
            var Cate = db.Categories.Where(x => x.Id == subCate.ParentId).SingleOrDefault();
            var supCate = db.Categories.Where(x => x.Id == Cate.ParentId).SingleOrDefault();
            
            ViewBag.subCate = subCate.Name;
            ViewBag.Cate = Cate.Name;
            ViewBag.supCate = supCate.Name;
            return View(Product);
        }

        public ActionResult Index(string searchName)
        {
            var links = from l in db.Products // lấy toàn bộ liên kết
                        select l;

            if (!String.IsNullOrEmpty(searchName)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.Name.Contains(searchName)); //lọc theo chuỗi tìm kiếm
            }

            return View(links); //trả về kết quả
        }
    }
}