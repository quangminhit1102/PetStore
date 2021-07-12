using System;
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
                name: "Product Details",
                url: "{san-pham}/{sitetile}-{ProId}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }

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
                url: "danh-muc-san-pham/{sitetile}/{CateId}",
                defaults: new { controller = "Product", action = "ListProductByCate", CateId = UrlParameter.Optional }

            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                
            );
        }


    }
}
