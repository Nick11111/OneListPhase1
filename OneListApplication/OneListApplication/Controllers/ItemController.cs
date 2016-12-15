using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult ItemManagement()
        {
            return View();
        }
        public ActionResult CreateItem()
        {
            return View();
        }
        public ActionResult CreateItemGroup()
        {
            return View();
        }
        public ActionResult ItemGroupManagement()
        {
            return View();
        }

        public ActionResult ItemDetail()
        {
            return View();
        }
        public ActionResult ItemGroupDetail()
        {
            return View();
        }
    }
}