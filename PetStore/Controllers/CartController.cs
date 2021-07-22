using Model.DAO;
using Model.EF;
using PetStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

            ViewBag.total = this.getTotal();
            ViewBag.user = user;
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        //Mua Hàng Ngay
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

        //Xóa sản phẩm trong giỏ hàng
        public ActionResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.Id == id);
            Session[CartSession] = sessionCart;
            return RedirectToAction("Index", "Cart");
        }

        //Tính số lượng
        public double getNumberCart()
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            if (sessionCart == null)
                return 0;
            return sessionCart.Sum(x => x.Quantity);
        }
        // tính tổng tiền 
        public decimal getTotal()
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            if (sessionCart == null)
                return 0;
            return sessionCart.Sum(x => x.Total);
        }


    }
}


