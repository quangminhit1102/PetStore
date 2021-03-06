using Model.DAO;
using Model.EF;
using Newtonsoft.Json.Linq;
using PetStore.Models.Common;
using PetStore.Models.Others;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int id = ((UserLogin)Session["USER"]).UserID;

            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            if(user.Address == null || user.Phone == null)
            {
                return RedirectToAction("ProfileCustomer", "Login");
            }
            ViewBag.total = this.getTotal();
            ViewBag.user = user;
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        //Mua Hàng Ngay
        public ActionResult AddItem(int productId, int quantity, string url)
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
            return Redirect(url);
        }
        //Mua Hàng Ngay
        public ActionResult AddItemAttribute(int productId, int quantity, int attribute, string url)
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
                        if (item.Product.Id == productId && item.AttributeName == PriceAttributes.AttributeValue.Name)
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
            return Redirect(url);
        }
        public ActionResult AddItemNow(int productId, int quantity)
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
        public ActionResult AddItemAttributeNow(int productId, int quantity, int attribute)
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
                        if (item.Product.Id == productId && item.AttributeName == PriceAttributes.AttributeValue.Name)
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
            return sessionCart.Sum(x => x.Total = (decimal)x.Price * x.Quantity);
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
            if (user.Address == null || user.Phone == null)
            {
                return RedirectToAction("ProfileCustomer", "Login");
            }
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
                        if (item.Price == null)
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

                if (payment == "momo")
                {
                    return RedirectToAction("Payment", "Cart");
                }



            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
            Session[CartSession] = null;
            return RedirectToAction("Success", "Cart");
        }
        [HttpPost]
        public ActionResult Success()
        {
            db = new PetStoreDbContext();
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int id = ((UserLogin)Session["USER"]).UserID;
            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.address = user.Address;
            return View();
        }

        public ActionResult Payment()
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOJX6D20210724";
            string accessKey = "nq30cmUMfYdmhtyF";
            string serectkey = "kJ0ueEBqeMc8p64wXBjbxlcSvnV5Lntb";
            string orderInfo = "test";
            string returnUrl = "https://localhost:44305/Cart/ConfirmPaymentClient";
            string notifyurl = "https://abc006ff6988.ngrok.io/Cart/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = this.getTotal().ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MomoSecurity crypto = new MomoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }


        public ActionResult ConfirmPaymentClient()
        {
            /* if (Session["USER"] == null)
             {
                 return RedirectToAction("Index", "Login");
             }
             string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signture") - 1);
             param = Server.UrlDecode(param);
             string serectkey = "aRqDgit3JmAciVJ3wXkcWhdIREHyXdEM";
             MomoSecurity crypto = new MomoSecurity();
             string signture = crypto.signSHA256(param, serectkey);
             if (signture != Request["signture"].ToString())
             {
                 ViewBag.message = "Thông tin không hợp lệ";
                 return View();
             }*/
            if (!Request.QueryString["errorCode"].Equals("0"))
            {
                ViewBag.message = "Thanh toán thất bại";

            }
            else
            {
                db = new PetStoreDbContext();
                ViewBag.message = "Đặt hàng thành công";
               
               
                int id = ((UserLogin)Session["USER"]).UserID;
                var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.address = user.Address;

                var cart = Session[CartSession];
                var list = new List<CartItem>();
                if (Session["USER"] == null && Session["CartSession"] ==null)
                {
                    return RedirectToAction("Index", "Login");
                }
           
                Order order = new Order();
                //tạo đơn hàng 
                order.CustomerId = id;
                order.OrderDate = DateTime.Now;
                order.PaymentStatus = true;
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
                    if (item.Price == null)
                    {
                        temp.Price = item.Product.Price;
                    }
                    else
                    {
                        temp.Price = item.Price;
                    }

                    db.OrderDetails.Add(temp);
                    db.SaveChanges();
                    Session[CartSession] = new List<CartItem>();
                }
            }

            return View();
            //hiển thị thông báo cho người dùng
        }
        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db

            /*string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signture") - 1);
            param = Server.UrlDecode(param);
            string serectkey = "aRqDgit3JmAciVJ3wXkcWhdIREHyXdEM";
            MomoSecurity crypto = new MomoSecurity();
            string signture = crypto.signSHA256(param, serectkey);*/

            if (!Request.QueryString["status"].Equals("0"))
            {


            }
            else
            {
               /* var cart = Session[CartSession];
                var list = new List<CartItem>();
                int id = ((UserLogin)Session["USER"]).UserID;
                Order order = new Order();
                //tạo đơn hàng 
                order.CustomerId = id;
                order.OrderDate = DateTime.Now;
                order.PaymentStatus = true;
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
                    if (item.Price == null)
                    {
                        temp.Price = item.Product.Price;
                    }
                    else
                    {
                        temp.Price = item.Price;
                    }

                    db.OrderDetails.Add(temp);
                    db.SaveChanges();
                }*/
            }




        }



    }
}


