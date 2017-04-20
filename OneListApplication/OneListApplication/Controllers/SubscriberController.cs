using OneListApplication.Repositories;
using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Controllers
{
    public class SubscriberController : Controller
    {
        public string FindUserID()
        {
            string name = User.Identity.Name;
            OneListCAEntities context = new OneListCAEntities();
            AspNetUser user = context.AspNetUsers
                    .Where(u => u.UserName == name).FirstOrDefault();
            string userId = user.Id;
            return userId;
        }
        /* *******************************************************
        * Add Subscriber Group
        * Get & Post
        ********************************************************/
        [HttpGet]
        public ActionResult AddSubscriberGroup()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddSubscriberGroup(SubscriberGroupVM subscriberGroup)
        {
            subscriberGroup.UserID = FindUserID();

            if (ModelState.IsValid)
            {
                SuscriberGroup sg = new SuscriberGroup();
                sg.SuscriberGroupName = subscriberGroup.SubscriberGroupName;
                sg.UserID = subscriberGroup.UserID;
                OneListEntitiesCore Core = new OneListEntitiesCore();
                Core.SuscriberGroups.Add(sg);
                Core.SaveChanges();
            }
            else
            {
                ViewBag.ErrorMsg = "Cannot add Subscriber Group.";
            }
            return View();
        }
        /* *******************************************************
        * Subscriber Group Management
        ********************************************************/
        [HttpGet]
        public ActionResult SubscriberGroupManagement()
        {
            if (Request.IsAuthenticated)
            {
                string publisherID = FindUserID();
                ViewBag.ActionMsg = TempData["ActionMsg"];
                SubscriberRepo subscriberRepo = new SubscriberRepo();
                IEnumerable<SubscriberGroupVM> subscriberGroups = subscriberRepo.GetSubscriberGroups(publisherID);
                return View(subscriberGroups);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public ActionResult DeleteSubscriberGroup(int id)
        {
            if (Request.IsAuthenticated)
            {
                string errMsg = "";
                string publisherID = FindUserID();
                SubscriberRepo subscriberRepo = new SubscriberRepo();
                subscriberRepo.DeleteGroup(publisherID, id, out errMsg);
                TempData["ActionMsg"] = errMsg;
                return RedirectToAction("SubscriberGroupManagement");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddSubscriberToGroup(SubscriberGroupVM subGroup) {

            if (Request.IsAuthenticated)
            {
                string errMsg = "";
                SubscriberRepo subscriberRepo = new SubscriberRepo();
                if (ModelState.IsValid)
                {
                    subscriberRepo.AddUserToGroup(subGroup, out errMsg);
                    TempData["EditMsg"] = errMsg;
                }
                else
                {
                    TempData["EditMsg"] = "Cannot add user to group.";
                }
                SubscriberGroupVM subscriberGroup = subscriberRepo.GetGroupDetails(subGroup.SubscriberGroupID);
                return RedirectToAction("EditSubscriberGroup", new { id = subGroup.SubscriberGroupID });
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult DeleteSubscriber(string userId, int id) {
            if (Request.IsAuthenticated)
            {
                string errMsg = "";
                SubscriberRepo subscriberRepo = new SubscriberRepo();
                subscriberRepo.DeleteSubscriber(userId, id, out errMsg);
                ViewBag.ErrorMsg = errMsg;
                return RedirectToAction("EditSubscriberGroup", new { id = id });
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult ChangeSubscriberType(string userId, int id)
        {
            if (Request.IsAuthenticated)
            {
                string errMsg = "";
                SubscriberRepo subscriberRepo = new SubscriberRepo();
                subscriberRepo.ChangeSubscriberType(userId, id, out errMsg);
                ViewBag.ErrorMsg = errMsg;
                return RedirectToAction("EditSubscriberGroup", new { id = id });
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public ActionResult ChangeSubscriberStatus(string userId, int id)
        {
            if (Request.IsAuthenticated)
            {
                string errMsg = "";
                SubscriberRepo subscriberRepo = new SubscriberRepo();
                subscriberRepo.ChangeSubscriberStatus(userId, id, out errMsg);
                ViewBag.ErrorMsg = errMsg;
                return RedirectToAction("EditSubscriberGroup", new { id = id });
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult EditSubscriberGroup(int id)
        {
            if (Request.IsAuthenticated)
            {
                SubscriberRepo subscriberRepo = new SubscriberRepo();
                SubscriberGroupVM subscriberGroup = subscriberRepo.GetGroupDetails(id);
                return View(subscriberGroup);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditSubscriberGroup(SubscriberGroupVM group)
        {
            SubscriberRepo subscriberRepo = new SubscriberRepo();
            SubscriberGroupVM subscriberGroup = subscriberRepo.GetGroupDetails(group.SubscriberGroupID);
            if (ModelState.IsValid)
            {
                if (subscriberRepo.UpdateGroup(group))
                {
                    ViewBag.success = "Updated successfully!";
                }
                else {
                    ViewBag.fail = "Cannot update!";
                }
                return View(subscriberGroup);

            }
            else {
                return View(subscriberGroup);
            }

        }

    }
}