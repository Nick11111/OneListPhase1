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
        public string FindUserID()
        {
            string name = User.Identity.Name;
            OneListCAEntities context = new OneListCAEntities();
            AspNetUser user = context.AspNetUsers
                    .Where(u => u.UserName == name).FirstOrDefault();
            string userId = user.Id;
            return userId;
        }
        public ActionResult CreateList()
        {
            ListRepo rep = new ListRepo();
            string userID = FindUserID();
            ListVM cleanList = rep.CreateList(userID);
            return View(cleanList);
        }

        [HttpPost]
        public ActionResult CreateList(FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                ListRepo rep = new ListRepo();
                ListVM NewList = new ListVM();
                foreach (string key in formCollection.AllKeys)
                {
                    switch (key)
                    {
                        case "ListName":
                            NewList.ListName = formCollection[key];
                            break;
                        case "ListType":
                            NewList.ListTypeID = int.Parse(formCollection[key]);
                            break;
                        case "ItemCategory":
                            NewList.ItemCategoryID = int.Parse(formCollection[key]);
                            break;
                        case "SuscriberGroup":
                            NewList.SuscribergroupID = int.Parse(formCollection[key]);
                            break;
                    }

                }

                NewList.CreatorID = FindUserID();
                NewList.CreationDate = DateTime.Now;
                bool resp = rep.CreateList(NewList);

                if (resp == false)
                {
                    ViewBag.ErrorMsg = "Cannot add List.";
                }
                else
                {
                    ViewBag.ActionMsg = "List Added Successfully.";
                }

            }
            else
            {
                ViewBag.ErrorMsg = "Cannot add List.";
            }
            ListRepo repw = new ListRepo();
            string userID = FindUserID();
            ListVM cleanList = repw.CreateList(userID);
            return View(cleanList);
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