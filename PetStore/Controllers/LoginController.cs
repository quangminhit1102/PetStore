using Model.DAO;
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
                    userSession.UserID = user.Id;
                    userSession.UserName = user.Username;
                    userSession.Role = user.Role;
                    Session.Add(ConmmonConstants.USER_SESSION, userSession);
                    if (user.Role == 4)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
                    }
                }
                else if(result==0) 
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
        //LOGOUT
        public ActionResult Logout()
        {
            Session[ConmmonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

    }
}