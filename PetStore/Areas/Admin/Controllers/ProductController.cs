using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace PetStore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        PetStoreDbContext db = null;
        public ActionResult ListProduct(int page = 1, int pageSize = 10)
        {
            db = new PetStoreDbContext();
            var listProduct = db.Products.OrderByDescending(x=>x.CreatedAt).ToPagedList(page,pageSize);
            return View(listProduct);
        }
    }
}