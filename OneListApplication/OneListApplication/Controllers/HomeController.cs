using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace OneListApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.date = GetBuildDate();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult ListManagement()
        {
            return View();
        }

        public ActionResult SubscriberManagement()
        {
            return View();
        }

        public ActionResult Sidebar()
        {
            return View();
        }

        public static DateTime GetBuildDate()
        {
            UriBuilder uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            return System.IO.File.GetLastWriteTime(
                Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path))
                );
        }
    }
}