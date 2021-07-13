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
            var listOD = db.OrderDetails.ToList();
            var listOrder = db.Orders.OrderByDescending(x => x.OrderDate).ToList();
            foreach (var item in listOrder)
            {
                item.Total = listOD.Where(t => t.OrderId == item.Id).Sum(i => i.Price * i.Quantity);
            }
            return View(listOrder);
        }
    }
}