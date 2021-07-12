using Model.EF;
using System;
using System.Collections.Generic;
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
    }
}