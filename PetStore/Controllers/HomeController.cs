﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStore.Controllers
{
    
    public class HomeController : Controller
    {
        PetStoreDbContext db = null;
        public ActionResult Index()
        {
            db = new PetStoreDbContext();
            var listProduct = db.Products.ToList().Take(16);
            return View(listProduct);
        }

    }
}