using Model.EF;
using System;
using System.Collections.Generic;
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
    }
}