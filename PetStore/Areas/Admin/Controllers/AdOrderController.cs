using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Areas.Admin.Controllers
{
    public class AdOrderController : Controller
    {
        PetStoreDbContext db = null;
        // GET: Admin/Order
        public ActionResult ListOrder()
        {
            db = new PetStoreDbContext();
            var listOrder = db.Orders.OrderByDescending(x => x.OrderDate).ToList();
            return View(listOrder);
        }
    }
}