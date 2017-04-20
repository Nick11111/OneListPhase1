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
            if (Request.IsAuthenticated)
            {
                ListRepo rep = new ListRepo();
                string userID = FindUserID();
                ListVM cleanList = rep.CreateList(userID);
                return View(cleanList);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CompleteList(int id)
        {
            ListRepo rep = new ListRepo();
            string userID = FindUserID();
            bool cleanList = rep.CompleteList(id, userID);

            if (cleanList == true)
            {
                ViewBag.ActionMsg = "List Added Successfully.";
            }
            else
            {
                ViewBag.ErrorMsg = "Cannot complete List.";
            }
            return RedirectToAction("ListManagement", "Home");
        }

        [HttpPost]
        public ActionResult CreateList(FormCollection formCollection)
        {
            string userID = FindUserID();
            ListRepo rep = new ListRepo();
            ListVM NewList = new ListVM();
            if (ModelState.IsValid)
            {
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
                            NewList.SuscribergroupID = formCollection[key];
                            break;
                    }

                }

                NewList.CreatorID = userID;
                NewList.CreationDate = DateTime.Now;
                if (NewList.ListName.Trim().Length < 1)
                {
                    ViewBag.InputErrorMsg = "List name is required.";
                    return View(rep.CreateList(userID));
                }
                else {
                    rep.CreateList(NewList);

                    //if (resp == false)
                    //{
                    //    TempData["ErrorMsg"] = "Cannot add List.";
                    //    ViewBag.ErrorMsg = "Cannot add List.";
                    //    return View(rep.CreateList(userID));
                    //}
                    //else
                    //{
                    //    TempData["ActionMsg"] = "List Added Successfully";
                    //    ViewBag.ActionMsg = "List Added Successfully.";
                    return RedirectToAction("ListManagement", "Home");
                    //}
                }
            }
            else
            {
                TempData["ErrorMsg"] = "Cannot add List.";
                ViewBag.ErrorMsg = "Cannot add List.";
                return View(rep.CreateList(userID));
            }
        }
        [HttpPost]
        public ActionResult EditList(ListViewVM formCollection)
        {
            return View();
        }

        public ActionResult UpdateItemList(int itemId, int id, string solved, string notes, string cost)
        {
            string userID = FindUserID();
            ListRepo repw = new ListRepo();
            bool solveoption = false;
            decimal valueCost = 0;
            if (solved == "true")
            {
                solveoption = true;
            }
            try
            {
               valueCost  = decimal.Parse(cost);
            }
            catch
            {
                valueCost = 0;
            }

            if (repw.updateItemList(userID,itemId, id, solveoption, valueCost, notes))
            {
                ViewBag.ActionMsg = "Task  Deleted from List Successfully.";
            }
            else
            {
                ViewBag.DeleteMsg = "Error deleting Task from List";
            }

            return RedirectToAction("EditList", new { id = id });
        }
        public ActionResult DeleteItemList(int itemId, int id)
        {
            ListRepo repw = new ListRepo();
            if (repw.deleteItemList(itemId, id))
            {
                ViewBag.ActionMsg = "Task  Deleted from List Successfully.";
            }

            return RedirectToAction("EditList", new { id = id });
        }
        public ActionResult EditList(int id)
        {
            //send the information to get the list and the user type

            if (!Request.IsAuthenticated)
            {

                //that list doesnt exist or he has no rights for that list, return to list management
                return RedirectToAction("Login", "Home");

            }
            else
            {
                string userID = FindUserID();
                ListRepo repw = new ListRepo();
                ListViewVM list = repw.getList(id, userID);
                //show list 
                return View(list);
            }

        }

        public ActionResult DeleteList(int id)
        {
            //delete the list
            try
            {
                ListRepo repw = new ListRepo();
                string userID = FindUserID();
                bool cleanList = repw.DeleteList(id);
                if (cleanList == true)
                {
                    ViewBag.ActionMsg = "List Deleted Successfully.";
                }
                else
                {
                    ViewBag.ErrorMsg = "Cannot Delete List.";
                }
            }
            catch
            {
                ViewBag.ErrorMsg = "Cannot Delete List.";
            }
            return RedirectToAction("ListManagement", "Home");
        }

        public ActionResult ShowListDetails()
        {
            return View();
        }

        public ActionResult ShowSubscribedList()
        {
            if (Request.IsAuthenticated)
            {
                string UserID = FindUserID();
                ListRepo r = new ListRepo();
                IEnumerable<ListViewVM> listsummary = r.GetSuscribedLists(UserID);

                return View(listsummary);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult ShowCompleteList()
        {
            if (Request.IsAuthenticated)
            {

                string UserID = FindUserID();
                ListRepo r = new ListRepo();
                IEnumerable<ListViewVM> listsummary = r.GetCompletedLists(UserID);
                return View(listsummary);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult ShowAllList()
        {
            string UserID = FindUserID();
            ListRepo r = new ListRepo();
            IEnumerable<ListViewVM> listsummary = r.GetLists(UserID);

            return View(listsummary);
        }

    }
}