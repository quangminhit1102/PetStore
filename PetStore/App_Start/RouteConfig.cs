﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PetStore
{
    public class RouteConfig
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapRoute(
        //        name: "Default",
        //        url: "{controller}/{action}/{id}",
        //        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        //    );
        //}

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Add Cart Attribute",
                url: "them-gio-hang-theo-loai",
                defaults: new { controller = "Cart", action = "AddItemAttribute", Id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", Id = UrlParameter.Optional }

            );
            routes.MapRoute(
                name: "Product Details",
                url: "san-pham/{sitetile}-{ProId}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }

            );

            routes.MapRoute(
                name: "List Post By Categories",
                url: "bai-viet/danh-muc-bai-viet-{id}",
                defaults: new { controller = "Post", action = "ListPostByCata", id = UrlParameter.Optional }

            );

            routes.MapRoute(
                name: "Post Details",
                url: "bai-viet/{sitetile}-{id}",
                defaults: new { controller = "Post", action = "Detail", id = UrlParameter.Optional }

            );

            routes.MapRoute(
                name: "Posts",
                url: "bai-viet",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }

            );

            routes.MapRoute(
                name: "Brands",
                url: "thuong-hieu",
                defaults: new { controller = "Brand", action = "Index", id = UrlParameter.Optional }

            );

            routes.MapRoute(
                name: "Brand Details",
                url: "thuong-hieu/{chi-tiet}/{BrandId}",
                defaults: new { controller = "Brand", action = "ListProductByBrand", BrandId = UrlParameter.Optional }

            );

            routes.MapRoute(
                name: "Categories",
                url: "danh-muc-san-pham/{sitetile}-{CateId}",
                defaults: new { controller = "Product", action = "ListProductByCate", CateId = UrlParameter.Optional }

            );



            routes.MapRoute(
               name: "Cart",
               url: "gio-hang",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

            );
        }


    }
}
