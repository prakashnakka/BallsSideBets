using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BL.Models;
using MyBalls.Repository;
using MyBalls.DataAccess;
using Microsoft.Extensions.Configuration;

namespace MyBalls.Controllers
{
    public class LoginController : BaseSiteController<LoginController>
    {
        private IUserRepository _userRepository;
        public LoginController(ICookieRepository cookieRepository, IUserRepository userRepository, IConfiguration config) : base(cookieRepository, config)
        {
            _userRepository = userRepository;
        }

        [Route("users/sign_in")]
        public IActionResult sign_in()
        {
            //this.ViewBag.CurrUrl = CurrentUrl;
            if (IsLoggedIn)
            {
                return RedirectToAction(actionName: "add", controllerName: "picks");
            }

            return View();
        }

        [HttpPost]
        [Route("users/sign_in")]
        public async Task<IActionResult> sign_in(UserAccount myAccount)
        {
            bool isLoginVerified = false;
            if (ModelState.IsValid)
            {
                var user = await _userRepository.ValidateLoginByEmail(myAccount.Email, myAccount.Pwd);


                if (user != null)
                {
                    _cookieRepository.SetLoginCookies(user);

                    isLoginVerified = true;
                }
                else
                {
                    this.TempData["Error"] = "Login information invalid";
                    //var msg = new ToastrMessage("error", "The email/password you entered does not match", "top");
                    //LoginController loginController = this;
                    //loginController.TempData["toasts"] = JsonConvert.SerializeObject(msg);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

            if (isLoginVerified)
            {
                return RedirectToAction(actionName: "add", controllerName: "picks");
                //return Redirect(this.CurrentUrl);
            }
            else
                return RedirectToAction(actionName: "sign_in", controllerName: "users");

            //return RedirectToAction("index", "home");
        }

        [Route("Users/sign_up")]
        public IActionResult sign_up()
        {
            //this.ViewBag.CurrUrl = CurrentUrl;
            if (IsLoggedIn)
            {
                return RedirectToAction(actionName: "add", controllerName: "picks");
            }

            return View();
        }

        [HttpPost]
        [Route("users/sign_up")]
        public async Task<IActionResult> sign_up(UserAccount newUser)
        {
            var isLoginVerified = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isExistingEmail = await _userRepository.ExistingEmail(newUser.Email, null);
            if (!isExistingEmail)
            {
                var user = await _userRepository.Insert(newUser);

                _cookieRepository.SetLoginCookies(user);
                isLoginVerified = true;
            }
            else
            {
                this.TempData["error"] = "Email already exists.";
            }

            if (isLoginVerified)
            {
                return RedirectToAction(actionName: "add", controllerName: "picks");
            }
            else
                return RedirectToAction(actionName: "sign_up", controllerName: "users");
        }

        [Route("Users/Sign_out")]
        public IActionResult Sign_Out()
        {
            if (this.IsLoggedIn)
            {
                _cookieRepository.RemoveLoginCookies();
                base.SetLogin();
            }

            return RedirectToAction(actionName: "index", controllerName: "home");
            //return Redirect(this.CurrentUrl);
        }
    }
}