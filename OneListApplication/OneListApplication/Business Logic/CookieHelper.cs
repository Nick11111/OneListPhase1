using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneListApplication.Business_Logic
{
    public class CookieHelper
    {
        public const string USER_NAME = "UserName";
        public const string COLOR = "color";

        public void ClearCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[key];

                // Can't delete cookie so set expiry to past to clear it.
                cookie.Expires = DateTime.Now.AddDays(-1);

                // Send updated cookie back to client.
                HttpContext.Current.Response.SetCookie(cookie);
            }
        }

        public string GetCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                // Get cookie value if it exists.
                HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
                return cookie.Value;
            }
            return null;
        }

        public void SetCookie(string key, string cookieValue)
        {
            // Create a cookie.
            HttpCookie cookie = new HttpCookie(key);

            // Store a value in the cookie and set it.
            cookie.Value = cookieValue;
            cookie.Expires = DateTime.Now.AddYears(50);

            // Send cookie back to client.
            HttpContext.Current.Response.SetCookie(cookie);
        }

    }
}