using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBalls.Models;
using MyBalls.Security;
using MyBalls.Repository;
using Microsoft.Extensions.Configuration;

namespace MyBalls.Controllers
{
    //[LoginAuthorize(MyCustomMode.Enforce)]
    public class HomeController : BaseSiteController<HomeController>
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, ICookieRepository cookieRepository, IConfiguration config) : base(cookieRepository, config)
        {
            _logger = logger;
            //_config = config;
        }

        public IActionResult Index()
        {
            if (this.HasGameStarted)
                return RedirectToAction(actionName: "allpicks", controllerName: "picks");
            else
                return RedirectToAction(actionName: "add", controllerName: "picks");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Rules()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
