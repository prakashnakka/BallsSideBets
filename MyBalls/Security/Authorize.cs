using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace MyBalls.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LoginAuthorize : Attribute, IAuthorizationFilter
    {
        private bool _isLoggedIn = false;

        private MyCustomMode _mode;
        public LoginAuthorize(MyCustomMode mode)
        {
            _mode = mode;
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {

            var controllerInfo = filterContext.ActionDescriptor as ControllerActionDescriptor;

            if (filterContext != null)
            {
                if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["login"]) &&
                    filterContext.HttpContext.Request.Cookies["login"].ToUpper() == "YES")
                {
                    _isLoggedIn = true;
                }
                else
                {
                    _isLoggedIn = false;
                }

                if (!_isLoggedIn && _mode == MyCustomMode.Enforce)
                {
                    filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary {
                           {
                            "Controller",
                            "Users"
                           }, {
                            "Action",
                            "login"
                           }
                     });
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAuthorize : Attribute, IAuthorizationFilter
    {
        private bool _isAdmin = false;

        private MyCustomMode _mode;
        public AdminAuthorize(MyCustomMode mode)
        {
            _mode = mode;
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {

            var controllerInfo = filterContext.ActionDescriptor as ControllerActionDescriptor;

            if (filterContext != null)
            {
                if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["IsAdmin"]) &&
                    filterContext.HttpContext.Request.Cookies["IsAdmin"].ToUpper() == "TRUE")
                {
                    _isAdmin = true;
                }
                else
                {
                    _isAdmin = false;
                }

                if (!_isAdmin && _mode == MyCustomMode.Enforce)
                {
                    filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary {
                           {
                            "Controller",
                            "Home"
                           }, {
                            "Action",
                            "error"
                           }
                     });
                }
            }
        }
    }

    public enum MyCustomMode
    {
        Enforce,
        Ignore
    }
}
