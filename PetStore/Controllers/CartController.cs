using Model.DAO;
using Model.EF;
using PetStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace PetStore.Controllers
{
    public class CartController : Controller
    {
        PetStoreDbContext db = null;
        public const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            PetStoreDbContext db = new PetStoreDbContext();
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            ViewBag.total = this.getTotal();
            if(Session["USER"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
                    item.Price = product.Price;
                    list.Add(item);
                }
            }
            else
            {
                //Add New cart Item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                item.Price = product.Price;
                var list = new List<CartItem>();
                list.Add(item);
                //gan vao session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        //Mua Hàng Ngay
        public ActionResult AddItemAttribute(int productId, int quantity, int attribute)
        {
            db = new PetStoreDbContext();
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            var PriceAttributes = db.ProductAttributes.Where(x => x.Id == attribute).FirstOrDefault();
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.Id == productId && x.AttributeName == PriceAttributes.AttributeValue.Name))
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
                    item.Price = PriceAttributes.Price;
                    item.AttributeName = PriceAttributes.AttributeValue.Name;
                    list.Add(item);
                }
            }
            else
            {
                //Add New cart Item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                item.Price = PriceAttributes.Price;
                item.AttributeName = PriceAttributes.AttributeValue.Name;
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
            return sessionCart.Count;
        }
        // tính tổng tiền 
        public decimal getTotal()
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            if (sessionCart == null)
                return 0;
            return sessionCart.Sum(x => x.Total = (decimal) x.Price* x.Quantity);
        }
        public ActionResult ViewCart()
        {
            if (getNumberCart() == 0)
            {
                ViewBag.NumTotal = 0;
                return PartialView();
            }
            ViewBag.NumTotal = getNumberCart();
            return PartialView();
        }
        [HttpPost]
        public ActionResult Update(int id, CartItem model)
        {
            var cart = Session[CartSession];
            
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.Id == id))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.Id == id && item.AttributeName == null)
                        {
                            item.Quantity = model.Quantity;
                        }
                        if (item.Product.Id == id && item.AttributeName != null && item.AttributeName == model.AttributeName)
                        {
                            item.Quantity = model.Quantity;
                        }

                    }
                }
              
            }
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult Checkout()
        {
            PetStoreDbContext db = new PetStoreDbContext();
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            ViewBag.total = this.getTotal();
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int id = ((UserLogin)Session["USER"]).UserID;

            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();

            ViewBag.total = this.getTotal();
            ViewBag.user = user;
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult CheckoutSubmit(string payment)
        {
            PetStoreDbContext db = new PetStoreDbContext();
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            ViewBag.total = this.getTotal();
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int id = ((UserLogin)Session["USER"]).UserID;

            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();

            ViewBag.total = this.getTotal();
            ViewBag.user = user;
            if (cart != null)
            {
               
                if (payment == "tienmat")
                {
                    Order order = new Order();
                    //tạo đơn hàng 
                    order.CustomerId = id;
                    order.OrderDate = DateTime.Now;
                    order.PaymentStatus = false;
                    order.OrderStatus = false;
                    order.Total = (int)this.getTotal();
                    db.Orders.Add(order);                   
                    list = (List<CartItem>)cart;
                    foreach (var item in list)
                    {
                        OrderDetail temp = new OrderDetail();
                        temp.OrderId = order.Id;
                        temp.ProductId = item.Product.Id;
                        temp.Quantity = item.Quantity;
                        if(item.Price == null)
                        {
                            temp.Price = item.Product.Price;
                        }
                        else
                        {
                            temp.Price = item.Price;
                        }
                       
                        db.OrderDetails.Add(temp);
                        db.SaveChanges();
                    }
                }
           
                
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
            Session[CartSession] = null;
            return RedirectToAction("Success", "Cart");
        }
        public ActionResult Success()
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

          
            return View();
        }

    }
}


