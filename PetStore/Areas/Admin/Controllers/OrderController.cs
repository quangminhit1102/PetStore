using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        PetStoreDbContext db = null;
        // GET: Admin/Order
        public ActionResult ListOrder()
        {
            db = new PetStoreDbContext();
            var listOrder = db.Orders.OrderByDescending(x => x.OrderDate).ToList();//list Order
            var listOD = db.OrderDetails.ToList();//List order Detail
            foreach (var item in listOrder)
            {
                item.Total = listOD.Where(t => t.OrderId == item.Id).Sum(i => i.Price * i.Quantity);
            }
            return View(listOrder);
        }
    }
}