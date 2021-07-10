using Model.EF;
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

        public ActionResult ListProductByBrand(int Id)
        {
            db = new PetStoreDbContext();
            var listProduct = db.Products.Where(x => x.BrandId == Id).ToList();
            
            return View(listProduct);
        }

    }
}