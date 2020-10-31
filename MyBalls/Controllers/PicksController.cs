using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBalls.Security;
using MyBalls.Models;
using MyBalls.Repository;
using MyBalls.DataAccess;
using BL.Models;
using Microsoft.Extensions.Configuration;

namespace MyBalls.Controllers
{
    [LoginAuthorize(MyCustomMode.Enforce)]
    public class PicksController : BaseSiteController<PicksController>
    {
        private IPickRepository _pickRepository;
        public PicksController(ICookieRepository cookieRepository, IPickRepository pickRepository, IConfiguration config) : base(cookieRepository, config)
        {
            _pickRepository = pickRepository;
        }

        public IActionResult Index()
        {
            var myPick = new PlayerPick()
            {
                CombinedScore = 0,
                FirstInningWickets = 6,
                SecondInningWickets = 7,
                HighestScore = 54,
                HighestWickets = 3,
                OversChase = 19.2,
                Total4s = 52,
                Total6s = 21,
                TeamPick = 2,
                UserId = this.UserId,
                AddDt = DateTime.Now
            };

            //var isInserted = await _pickRepository.Insert(myPick);

            //this.TempData["PickInserted"] = isInserted;

            return View();
        }

        public async Task<IActionResult> Add()
        {
            if (this.HasGameStarted)
                return RedirectToAction(actionName: "allpicks", controllerName: "picks");

            var playerPicks = await _pickRepository.GetByUser(this.UserId);
            return View(playerPicks);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PlayerPick pick)
        {
            if (!this.HasGameStarted)
            {
                if (ModelState.IsValid)
                {
                    pick.UserId = this.UserId;

                    var isAddded = await _pickRepository.Insert(pick);


                    if (isAddded)
                    {
                        this.TempData["IsAdded"] = "Saved successfully. You can change these picks until game starts.";
                    }
                    else
                    {
                        this.TempData["IsAdded"] = "Something went wrong. Try again.";
                        //var msg = new ToastrMessage("error", "The email/password you entered does not match", "top");
                        //LoginController loginController = this;
                        //loginController.TempData["toasts"] = JsonConvert.SerializeObject(msg);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                this.TempData["error"] = "Game already started, you can't make any changes.";
                var error = this.TempData["error"].ToString();
                _cookieRepository.SetCookie("Error", error, 1);
            }

            //if (isLoginVerified)
            //    return RedirectToAction(actionName: "schools", controllerName: "user");
            //else
            //    return RedirectToAction(actionName: "sign_in", controllerName: "users");

            return RedirectToAction("add", "picks");
        }

        public async Task<IActionResult> History()
        {
            var myPicks = await _pickRepository.GetAuditByUser(this.UserId);
            this.ViewBag.Now = Convert.ToDateTime(_config["GameTime"]).AddHours(Convert.ToInt32(_config["OffsetHours"]));
            this.ViewBag.gt = DateTime.Now;
            return View(myPicks);
        }

        public async Task<IActionResult> allpicks()
        {
            var leaderboard = await _pickRepository.GetAllPicks();
            this.TempData["ResultPicks"] = await _pickRepository.GetResults(this.GameId);
            return View(leaderboard);
        }

        public async Task<IActionResult> results()
        {
            var pickResults = await _pickRepository.GetAllPoints();

            return View(pickResults);
        }
    }
}