﻿using System;
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

        [Route("users/login")]
        public IActionResult login()
        {
            //this.ViewBag.CurrUrl = CurrentUrl;
            if (IsLoggedIn)
            {
                return RedirectToAction(actionName: "add", controllerName: "picks");
            }

            return View();
        }

        [HttpPost]
        [Route("users/login")]
        public async Task<IActionResult> login(UserAccount myAccount)
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
                return RedirectToAction(actionName: "login", controllerName: "users");

            //return RedirectToAction("index", "home");
        }

        [Route("Users/register")]
        public IActionResult register()
        {
            //this.ViewBag.CurrUrl = CurrentUrl;
            if (IsLoggedIn)
            {
                return RedirectToAction(actionName: "add", controllerName: "picks");
            }

            return View();
        }

        [HttpPost]
        [Route("users/register")]
        public async Task<IActionResult> register(UserAccount newUser)
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
                this.TempData["error"] = "Email already registered. Check again or try logging in now?";
            }

            if (isLoginVerified)
            {
                return RedirectToAction(actionName: "add", controllerName: "picks");
            }
            else
                return RedirectToAction(actionName: "register", controllerName: "users");
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

        [Route("Users/reset")]
        public IActionResult Reset()
        {
            if (!this.IsLoggedIn)
            {
                return View();
            }
            else
            {
                return RedirectToAction(actionName: "index", controllerName: "home");
            }
        }

        [HttpPost]
        [Route("users/reset")]
        public async Task<IActionResult> reset(ChangePassword updateUser)
        {
            var isUpdated = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isExistingEmail = await _userRepository.ExistingEmail(updateUser.Email, null);
            if (isExistingEmail)
            {
                var user = await _userRepository.UpdatePassword(updateUser);

                //_cookieRepository.SetLoginCookies(user);
                isUpdated = true;
            }
            else
            {
                this.TempData["error"] = "Email not found. Please enter email you registered with.";
            }

            if (isUpdated)
            {
                this.TempData["Error"] = "Password updated. Please login with new password";
                return RedirectToAction(actionName: "login", controllerName: "users");
            }
            else
                return RedirectToAction(actionName: "reset", controllerName: "users");
        }
    }
}