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
        public ActionResult ListProductByCate(int ?Id)
        {
            
            return View();
        }

        public ActionResult Detail(int ProId)
        {
            db = new PetStoreDbContext();
            var Product = db.Products.Where(x => x.Id == ProId).FirstOrDefault();
            return View(Product);
        }

        
    }
}