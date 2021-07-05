using Model.DAO;
using Model.EF;
using PetStore.Models;
using PetStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        //lOGIN==============================================================
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var dao = new UserDao();
            if (ModelState.IsValid)
            {

                var result = dao.Login(model.Username, MD5Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.getByUserName(model.Username);
                    var userSession = new UserLogin();
                    userSession.UserID = user.Id;//
                    userSession.UserName = user.Username;//
                    userSession.Role = user.Role;//
                    Session.Add("USERID", user.Id);
                    if (user.Role == 4)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect("/admin/adminhome/index");
                    }
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
            Session["USERID"] = null;
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
                if(temp!=null)
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
                    if(result>0)
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
    }
}