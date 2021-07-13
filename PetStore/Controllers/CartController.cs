
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
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new Product().Id;
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
                    item.Product.Id = productId;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else
            {
                //Add New cart Item
                var item = new CartItem();
                item.Product.Id = productId;
                item.Quantity = quantity;
                var list = new List<CartItem>();

                //gan vao session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

    }
}