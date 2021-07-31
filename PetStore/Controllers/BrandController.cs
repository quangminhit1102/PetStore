using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Controllers
{

    public class BrandController : Controller
    {
        PetStoreDbContext db = null;


        // GET: Brand
        public ActionResult Index()
        {
            db = new PetStoreDbContext();
            var listBrand = db.Brands.ToList();
            return View(listBrand);
        }

        public ActionResult ListProductByBrand(int BrandId, int page = 1, int pageSize = 10)
        {
            db = new PetStoreDbContext();
            var listProduct = db.Products.Where(x => x.BrandId == BrandId).ToList();
            var brand = db.Brands.Where(x => x.Id == BrandId).FirstOrDefault();
            ViewBag.brand = brand.Name;
            return View(listProduct.OrderBy(x => x.ViewCount).ToPagedList(page, pageSize));
         
        }

    }
}