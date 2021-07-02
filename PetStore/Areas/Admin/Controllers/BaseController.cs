using PetStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PetStore.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[ConmmonConstants.USER_SESSION];
            if(session==null || (session.Role != 1 && session.Role != 2 && session.Role != 3))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", action = "Index", Area = ""}));
            }

            base.OnActionExecuting(filterContext);
        }
       
    }
}