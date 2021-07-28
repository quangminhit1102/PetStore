using Model.DAO;
using Model.EF;
using PetStore.Models;
using PetStore.Models.Common;
using PetStore.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace PetStore.Controllers
{
    public class LoginController : Controller
    {       
        // GET: Login
        public ActionResult Index()
        {
            if (Session["USER"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //lOGIN==============================================================
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var dao = new UserDao();
            if (ModelState.IsValid)
            {

                var result = dao.Login(model.Username, MD5Encryptor.MD5Hash(model.Password), 4);
                if (result == 1)
                {
                    var user = dao.getByUserName(model.Username);
                    var userSession = new UserLogin();
                    userSession.UserID = user.Id;
                    userSession.UserName = user.Username;
                    userSession.Name = user.FullName;
                    //userSession.ProfileImage = user.ProfileImage;
                    Session.Add("USER", userSession);
                    if (user.Role == 4)
                        return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng!");
                }

            }
            return View("Index");
        }
        //LOGOUT==============================================================
        public ActionResult Logout()
        {
            Session["USER"] = null;
            return Redirect("/");
        }
        //REGISTER==============================================================
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {

            var dao = new UserDao();
            if (ModelState.IsValid)
            {
                var temp = dao.getByUserName(model.username);
                if (temp != null)
                {
                    ModelState.AddModelError("username", "Tên đăng nhập đã tồn tại!");
                }
                else
                {
                    User user = new User();
                    user.Username = model.username;
                    user.Password = MD5Encryptor.MD5Hash(model.password);
                    user.FullName = model.name;
                    user.Role = 4;
                    user.Email = model.email;
                    user.Status = true;
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công mời đăng nhập lại!";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng kí không thành công!");
                    }
                }
            }

            return View(model);
        }
        PetStoreDbContext db = null;
        [HttpGet]
        public ActionResult ProfileCustomer()
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int userid = ((UserLogin)Session["USER"]).UserID;
            var dao = new UserDao();
            var user = dao.getUserById(userid);
            return View(user);
        }
        //Update Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileCustomer(User collection)
        {
            db = new PetStoreDbContext();
            int userid = ((UserLogin)Session["USER"]).UserID;
            var dao = new UserDao();
            var user = dao.getUserById(userid);
            try
            {
                var errors = ModelState.Values.SelectMany(b => b.Errors);
                if (ModelState.IsValid)
                {
                    //Update Profile
                    user.FullName = collection.FullName;
                    user.Gender = collection.Gender;
                    user.Phone = collection.Phone;
                    user.Role = 4;
                    user.Birthday = collection.Birthday;
                    user.Email = collection.Email;
                    user.Address = collection.Address;
                    //save change
                    var result = dao.Update(user);
                    if (result)
                    {
                        ViewBag.Success = "Cập nhật thông tin thành công!";
                    }
                    else
                    {
                        ViewBag.Fail = "Cập nhật thông tin thất bại!";
                    }
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }
        //Change Password View
        [HttpGet]
        public ActionResult ChangePassCustomer()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassCustomer(PasswordModel model)
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            db = new PetStoreDbContext();
            int userid = ((UserLogin)Session["USER"]).UserID;
            var dao = new UserDao();
            var user = dao.getUserById(userid);
            try
            {
                var errors = ModelState.Values.SelectMany(b => b.Errors);
                if (ModelState.IsValid)
                {
                    if (user.Password == MD5Encryptor.MD5Hash(model.password))
                    {
                        //Update Password
                        user.Password = MD5Encryptor.MD5Hash(model.newpassword);
                        db.Users.AddOrUpdate(user);
                        try
                        {
                            db.SaveChanges();
                            ViewBag.Success = "Đổi mật khẩu thành công";
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Fail = "Đổi mật khẩu thất bại";
                        }
                    }
                    else
                    {
                        ViewBag.WrongPass = "Mật khẩu cũ không đúng";
                    }

                    //save change
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Order
        [HttpGet]
        public ActionResult Order()
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            db = new PetStoreDbContext();
            int userid = ((UserLogin)Session["USER"]).UserID;
            var dao = new UserDao();
            var user = dao.getUserById(userid);
            try
            {
                var errors = ModelState.Values.SelectMany(b => b.Errors);
                db = new PetStoreDbContext();
                var OD = db.OrderDetails.ToList();
                var Order = db.Orders.Where(x => x.CustomerId == user.Id).OrderByDescending(x => x.OrderDate).ToList();
                foreach (var item in Order)
                {
                    if (item.Total == null){
                        item.Total = OD.Where(t => t.OrderId == item.Id).Sum(i => i.Price * i.Quantity);
                    }
                
                }
                return View(Order);
            }
            catch
            {
                return View();
            }
        }
    }
}
