using Model.EF;
using PetStore.Models;
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
            var listProduct =db.Products.Take(32).ToList();         
            var listCategory = db.Categories.ToList();
            HomeModel homemodel = new HomeModel();
            homemodel.listProduct = listProduct;
            homemodel.listCategory = listCategory;
            return View(homemodel);
        }     
    }
}