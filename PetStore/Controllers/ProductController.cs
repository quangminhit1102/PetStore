﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PetStore.Controllers
{
 
    public class ProductController : Controller
    {
        PetStoreDbContext db = null;
        public ActionResult ListProductByCate(int CateId)
        {
            db = new PetStoreDbContext();
            ViewBag.CateName = db.Categories.Where(x => x.Id == CateId).FirstOrDefault().Name;
            var listProductbyCate = db.Products.Where(x => x.CateId == CateId).ToList();
            return View(listProductbyCate);
        }

        public ActionResult Detail(int ProId)
        {
            db = new PetStoreDbContext();
            var Product = db.Products.Where(x => x.Id == ProId).FirstOrDefault();
            var subCate = db.Categories.Where(x => x.Id == Product.CateId).SingleOrDefault();
            var Cate = db.Categories.Where(x => x.Id == subCate.ParentId).SingleOrDefault();
            var supCate = db.Categories.Where(x => x.Id == Cate.ParentId).SingleOrDefault();
            var listRelatedProduct = db.Products.Where(x => x.CateId == Product.CateId).Take(4).ToList();

            ViewBag.subCate = subCate.Name;
            ViewBag.Cate = Cate.Name;
            ViewBag.supCate = supCate.Name;
            ViewBag.listRelatedProduct = listRelatedProduct;
            return View(Product);
        }
        public ActionResult AsideCategory()
        {
            db = new PetStoreDbContext();
            var listCategory = db.Categories.ToList();
            return PartialView(listCategory);
        }
        public ActionResult AsidePost()
        {
            db = new PetStoreDbContext();
            var listPost = db.Posts.Where(x=> x.Status == true).Take(3).ToList();
            return PartialView(listPost);
        }
    }
}