using BL.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace MyBalls.Repository
{
    public interface ICookieRepository
    {
        void SetCookie(string key, string value, int? expireTimeInMins);
        void RemoveCookie(string key);
        void SetLoginCookies(UserAccount user);
        void RemoveLoginCookies();
        string GetCookie(string keyName);
    }

    public class CookieRepository : ICookieRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly int ExpireTimeInMins = 10080;
        public CookieRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCookie(string key, string value, int? expireTimeInMins)
        {
            CookieOptions option = new CookieOptions();

            if (expireTimeInMins.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTimeInMins.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }

        public void RemoveCookie(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }

        public void SetLoginCookies(UserAccount user)
        {
            SetCookie("login", "yes", ExpireTimeInMins);
            SetCookie("UserID", user.UserId.ToString(), ExpireTimeInMins);
            //SetCookie("AccountType", user.AccountType.ToString(), ExpireTimeInMins);
            SetCookie("Email", user.Email, ExpireTimeInMins);
            SetCookie("IsAdmin", user.IsAdmin, ExpireTimeInMins);
        }

        public void RemoveLoginCookies()
        {
            RemoveCookie("login");
            RemoveCookie("UserID");
            RemoveCookie("Email");
            RemoveCookie("IsAdmin");
            //RemoveCookie("AccountType");
            //RemoveCookie("IsModerator");
        }

        public string GetCookie(string keyName)
        {
            if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Cookies[keyName]))
                return _httpContextAccessor.HttpContext.Request.Cookies[keyName];
            else
                return "";
        }
    }
}
