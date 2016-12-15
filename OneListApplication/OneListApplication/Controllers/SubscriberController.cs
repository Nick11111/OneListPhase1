using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Controllers
{
    public class SubscriberController : Controller
    {
        // GET: Subscriber
        public ActionResult AddSubscriber()
        {
            return View();
        }

        public ActionResult SubGroupDetails()
        {
            return View();
        }

        public ActionResult EditSubGroup()
        {
            return View();
        }

    }
}