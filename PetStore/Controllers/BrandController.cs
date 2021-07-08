using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Controllers
{
    [RoutePrefix("thuong-hieu")]
    public class BrandController : Controller
    {
        PetStoreDbContext db = null;


        // GET: Brand
        [Route]
        public ActionResult Index()
        {
            db = new PetStoreDbContext();
            var listBrand = db.Brands.ToList();
            return View(listBrand);
        }
    }
}