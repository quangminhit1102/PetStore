using Model.DAO;
using Model.EF;
using PetStore.Areas.Admin.Models;
using PetStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        PetStoreDbContext db = null;
        // GET: Admin/Account
        public ActionResult ListAccount()
        {
            db = new PetStoreDbContext();
            List<User> listUser = db.Users.ToList();
            return View(listUser);
        }
        //View UpdateProfile
        [HttpGet]
        public ActionResult Profile()
        {
            int userid = ((UserLogin)Session["USER"]).UserID;
            var dao = new UserDao();
            var user = dao.getUserById(userid);
            return View(user);
        }
        //Update Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(User collection)
        {
            db = new PetStoreDbContext();
            int userid = ((UserLogin)Session["USER"]).UserID;
            var dao = new UserDao();
            var user = dao.getUserById(userid);
            try
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if (ModelState.IsValid)
                {
                    //Update Profile
                    user.FullName = collection.FullName;
                    user.Gender = collection.Gender;
                    user.Phone = collection.Phone;
                    user.Birthday = collection.Birthday;
                    user.Email = collection.Email;
                    user.Address = collection.Address;
                    //save change
                    var result = dao.Update(user);
                    if(result)
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
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(PasswordModel model)
        {
            db = new PetStoreDbContext();
            int userid = ((UserLogin)Session["USER"]).UserID;
            var dao = new UserDao();
            var user = dao.getUserById(userid);
            try
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
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
                        catch(Exception ex)
                        {
                            ViewBag.Success = "Đổi mật khẩu thất bại";
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
    }
}