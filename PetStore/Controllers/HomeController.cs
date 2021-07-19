using Model.EF;
using PetStore.Models;
using PetStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PetStore.Controllers
{
  
    public class HomeController : Controller
    {
        PetStoreDbContext db = null;
        public ActionResult Index()
        {
            db = new PetStoreDbContext();
            var listProduct = db.Products.Take(32).ToList();
            HomeModel homemodel = new HomeModel();
            homemodel.listProduct = listProduct;
            return View(homemodel);
        }

    

        public ActionResult LoadMenu()
        {
            db = new PetStoreDbContext();
            var listCategory = db.Categories.ToList();
            
            return PartialView(listCategory);
        }
    }
}