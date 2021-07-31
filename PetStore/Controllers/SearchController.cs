using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using PetStore.Models;

namespace PetStore.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string KeyWord)
        {
            PetStoreDbContext db = new PetStoreDbContext();
            ViewBag.SearchName = db.Products.Where(x => x.Name.Contains(KeyWord));
            var list = db.Products.Where(x => x.Name.Contains(KeyWord) || x.Brand.Name.Contains(KeyWord) || x.Category.Name.Contains(KeyWord) || x.Description.Contains(KeyWord));
            return View(list.OrderBy(x => x.Name));
        }
    }
}