using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PetStoreDbContext db = new PetStoreDbContext();
            var listBrand = db.Categories.ToList();
            return View(listBrand);
        }
    }
}