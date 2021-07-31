using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Areas.Admin.Controllers
{
    public class AdminHomeController : BaseController
    {
        // GET: Admin/Home
        PetStoreDbContext db = null;
        public ActionResult Index()
        {
            db = new PetStoreDbContext();
            int countOrder = 0;
            int? Profit = 0;

       
           List <OrderDetail> listOD = db.OrderDetails.ToList();
            var listOrder = db.Orders.OrderByDescending(x=>x.OrderDate).ToList();
            
            countOrder = listOrder.Count();
            foreach (OrderDetail item in listOD)
            {
                Profit += item.Price * item.Quantity;
            }
            //Profit each Order
            foreach (var item in listOrder)
            {
                item.Total = listOD.Where(t => t.OrderId == item.Id).Sum(i => i.Price * i.Quantity);
            }
            ViewBag.countOrder = countOrder;
            ViewBag.Profit = Profit;
            return View(listOrder);
        }
        public ActionResult Calendar()
        {
            return View();
        }
    }
}