using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PetStore.Controllers
{
    
    public class ProductController : Controller
    {
        public ActionResult ListProductByCate(int ?Id)
        {
            
            return View();
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}