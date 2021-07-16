using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.DAO;

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
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            db = new PetStoreDbContext();
            var product = db.Products.Find(id);
            return View(product);
        }
        
    }
}