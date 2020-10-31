using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MyBalls.Repository;
using System;
using Microsoft.Extensions.Configuration;

namespace MyBalls.Controllers
{
    public abstract class BaseSiteController<T> : Controller where T : BaseSiteController<T>
    {
        private ILogger<T> _logger;
        //protected ILogger<T> Logger => _logger ?? (_logger = HttpContext?.RequestServices.GetService<ILogger<T>>());

        private string _currentUrl;
        protected string CurrentUrl => _currentUrl ?? (_currentUrl = HttpContext?.Request.GetDisplayUrl());

        protected virtual bool IsLoggedIn { get; set; }
        protected virtual int UserId { get; set; }
        protected virtual string Email { get; set; }
        protected virtual bool IsAdmin { get; set; }
        protected virtual bool HasGameStarted { get; set; }
        protected virtual string ErrorMessage { get; set; }
        protected readonly ICookieRepository _cookieRepository;
        protected readonly IConfiguration _config;
        protected virtual int GameId { get; set; }

        protected BaseSiteController()
        {
            
        }

        //protected BaseSiteController(ICookieRepository cookieRepository)
        //{
        //    _cookieRepository = cookieRepository;
        //}

        protected BaseSiteController(ICookieRepository cookieRepository, IConfiguration config)
        {
            _cookieRepository = cookieRepository;
            _config = config;

            this.GameId = Convert.ToInt32(_config["GameId"]);
        }

        //protected BaseSiteController(ICookieRepository cookieRepository, IDashboardHelper dashboardHelper)
        //{
        //    _cookieRepository = cookieRepository;
        //    _dashboardHelper = dashboardHelper;
        //}

        //protected BaseSiteController(IDashboardHelper dashboardHelper)
        //{
        //    _dashboardHelper = dashboardHelper;
        //}

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            SetLogin();
            this.ViewBag.Email = Email;
            //this.ViewBag.IsOwner = _dashboardHelper.IsOwner();
        }

        protected void SetLogin()
        {
            this.ViewBag.IsLoggedIn = false;
            IsLoggedIn = false;

            if (!string.IsNullOrEmpty(Request.Cookies["login"]) && Request.Cookies["login"].ToUpper() == "YES")
            {
                this.ViewBag.IsLoggedIn = true;
                IsLoggedIn = true;

                if (!string.IsNullOrEmpty(Request.Cookies["UserID"]))
                    UserId = Convert.ToInt32(Request.Cookies["UserID"]);

                if (!string.IsNullOrEmpty(Request.Cookies["Email"]))
                    Email = Request.Cookies["Email"].ToString();

                if (!string.IsNullOrEmpty(Request.Cookies["IsAdmin"]))
                    IsAdmin = Convert.ToBoolean(Request.Cookies["IsAdmin"].ToString());

                this.ViewBag.IsAdmin = IsAdmin;
                this.ViewBag.Email = Email;
            }

            HasGameStarted = GameStarted();

            this.ViewBag.GameStarted = HasGameStarted;
            this.ViewBag.ErrorMessage = Request.Cookies["Error"] == null ? null : Request.Cookies["Error"].ToString();
            this.ViewBag.UserId = UserId;
        }

        protected bool GameStarted()
        {
            
            var nowTime = DateTime.Now;
            var gameTime = Convert.ToDateTime(_config["GameTime"]).AddHours(Convert.ToInt32(_config["OffsetHours"]));

            return nowTime > gameTime ? true : false; 
        }
    }
}
