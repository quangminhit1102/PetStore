using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        PetStoreDbContext db = null;
        public ActionResult ListProduct()
        {
            return View();
        }
    }
}