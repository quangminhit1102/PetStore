using Model.DAO;
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
        [Route]
        public ActionResult Index()
        {
            db = new PetStoreDbContext();
            //Order and Total Profit count
            int countOrder = 0;
            int? Profit = 0;
            List<OrderDetail> listOD = db.OrderDetails.ToList();//List Order detail
            var listOrder = db.Orders.OrderByDescending(x => x.OrderDate).Take(5).ToList();
           // var listOrder = db.Orders.OrderByDescending(x=>x.OrderDate).Take(5).ToList();//Take 5 newest Order
            countOrder = listOrder.Count();//Count total Order
            foreach (OrderDetail item in listOD)
            {
                Profit += item.Price * item.Quantity;// Count total profit of all order
            }
            ViewBag.countOrder = countOrder;
            ViewBag.Profit = Profit;
            //Profit each Order
            foreach(var item in listOrder)
            {
                item.Total = listOD.Where(t => t.OrderId == item.Id).Sum(i => i.Price*i.Quantity);
            }    
            return View(listOrder);
        }
        public ActionResult Profile()
        {
            UserDao dao = new UserDao();
            var user = dao.getUserById((int)Session["USERID"]);
            return View(user);
        }
        public ActionResult changePass()
        {
            UserDao dao = new UserDao();
            var user = dao.getUserById((int)Session["USERID"]);
            return View(user);
        }
        public ActionResult Calendar()
        {
            return View();
        }
    }
}