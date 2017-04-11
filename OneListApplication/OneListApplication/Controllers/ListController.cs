using OneListApplication.Repositories;
using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Controllers
{
    public class ListController : Controller
    {
        // GET: List

        public ActionResult CreateList()
        {
            ListRepo rep = new ListRepo();
            ListVM cleanList = rep.CreateList();
            return View(cleanList);
        }

        [HttpPost]
        public ActionResult CreateList(FormCollection formCollection)
        {
            foreach (string _formData in formCollection)
            {
                ViewData[_formData] = formCollection[_formData];
            }

            return View();
        }

        public ActionResult EditList()
        {
            return View();
        }

        public ActionResult ShowListDetails()
        {
            return View();
        }

        public ActionResult ShowSubscribedList()
        {
            return View();
        }

        public ActionResult ShowCompleteList()
        {
            return View();
        }

    }
}