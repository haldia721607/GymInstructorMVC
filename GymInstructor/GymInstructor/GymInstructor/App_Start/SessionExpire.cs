using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace GymInstructor.App_Start
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (filterContext != null)
            {
                HttpSessionStateBase objHttpSessionStateBase = filterContext.HttpContext.Session;
                var userSession = objHttpSessionStateBase[SessionObjects.CNUserId];
                var adminSession = objHttpSessionStateBase[SessionObjects.CNAdminId];
                if (adminSession == null && userSession == null)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.StatusCode = 403;
                        filterContext.Result = new JsonResult { Data = "LogOut" };
                    }
                    else
                    {
                        string redirectUrl = string.Format("?returnUrl={0}", filterContext.HttpContext.Request.Url.PathAndQuery);
                        filterContext.HttpContext.Response.Redirect(FormsAuthentication.LoginUrl + redirectUrl, true);
                    }
                }
            }
        }
    }
}