using Model.DAO;
using Model.EF;
using PetStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Controllers
{
    public class CartController : Controller
    {

        public const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            PetStoreDbContext db = new PetStoreDbContext();
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            int id = ((UserLogin)Session["USER"]).UserID;
            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();

            ViewBag.user = user;
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.Id == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.Id == productId)
                        {
                            item.Quantity += quantity;
                        }

                    }
                }
                else
                {
                    //Add New cart Item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else
            {
                //Add New cart Item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gan vao session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

    }
}


