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
            return View();
        }
        [HttpPost]
        public ActionResult AddSubscriberGroup(SubscriberGroupVM subscriberGroup)
        {
            subscriberGroup.UserID = FindUserID();

            if (ModelState.IsValid)
            {
                //TO DO: typo for Database
                SuscriberGroup sg = new SuscriberGroup();
                sg.SuscriberGroupName = subscriberGroup.SubscriberGroupName;
                sg.UserID = subscriberGroup.UserID;
                //sg.SuscriberGroupID = 0;
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
            string publisherID = FindUserID();
            ViewBag.ActionMsg = TempData["ActionMsg"];
            SubscriberRepo subscriberRepo = new SubscriberRepo();
            IEnumerable<SubscriberGroupVM> subscriberGroups = subscriberRepo.GetSubscriberGroups(publisherID);
            return View(subscriberGroups);
        }
        [HttpGet]
        public ActionResult DeleteSubscriberGroup(int id)
        {
            string errMsg = "";
            string publisherID = FindUserID();
            SubscriberRepo subscriberRepo = new SubscriberRepo();
            subscriberRepo.DeleteGroup(publisherID, id, out errMsg);
            TempData["ActionMsg"] = errMsg;
            return RedirectToAction("SubscriberGroupManagement");
        }
        [HttpPost]
        public ActionResult AddSubscriberToGroup(SubscriberGroupVM subGroup) {
            
            string errMsg = "";
            SubscriberRepo subscriberRepo = new SubscriberRepo();
            if (ModelState.IsValid)
            { 
                subscriberRepo.AddUserToGroup(subGroup,out errMsg);
                TempData["EditMsg"] = errMsg;
            }
            else
            {
                TempData["EditMsg"] = "Cannot add user to group.";
            }
            SubscriberGroupVM subscriberGroup = subscriberRepo.GetGroupDetails(subGroup.SubscriberGroupID);
            return RedirectToAction("EditSubscriberGroup", new { id = subGroup.SubscriberGroupID });
        }

        public ActionResult DeleteSubscriber(string userId, int id) {
            string errMsg = "";
            SubscriberRepo subscriberRepo = new SubscriberRepo();
            subscriberRepo.DeleteSubscriber(userId, id, out errMsg);
            ViewBag.ErrorMsg = errMsg;
            return RedirectToAction("EditSubscriberGroup", new { id = id});
        }

        public ActionResult ChangeSubscriberType(string userId, int id)
        {
            string errMsg = "";
            SubscriberRepo subscriberRepo = new SubscriberRepo();
            subscriberRepo.ChangeSubscriberType(userId, id, out errMsg);
            ViewBag.ErrorMsg = errMsg;
            return RedirectToAction("EditSubscriberGroup", new { id = id });
        }
        [HttpGet]
        public ActionResult ChangeSubscriberStatus(string userId, int id)
        {
            string errMsg = "";
            SubscriberRepo subscriberRepo = new SubscriberRepo();
            subscriberRepo.ChangeSubscriberStatus(userId, id, out errMsg);
            ViewBag.ErrorMsg = errMsg;
            return RedirectToAction("EditSubscriberGroup", new { id = id });
        }

        [HttpGet]
        public ActionResult EditSubscriberGroup(int id)
        {
            SubscriberRepo subscriberRepo = new SubscriberRepo();
            SubscriberGroupVM subscriberGroup = subscriberRepo.GetGroupDetails(id);
            return View(subscriberGroup);
        }

        [HttpPost]
        public ActionResult EditSubscriberGroup(SubscriberGroupVM group)
        {
            bool ItemUpdated;
            ViewBag.EditMsg = TempData["EditMsg"];
            if (ModelState.IsValid)
            {
                SubscriberRepo subscriberRepo = new SubscriberRepo();
                ItemUpdated = subscriberRepo.UpdateGroup(group);
                if (ItemUpdated)
                {
                    ViewBag.EditMsg = "Group Name Updated";
                    //return RedirectToAction("ItemDetail", new { id = group.ItemID });
                }
                else
                {
                    ViewBag.EditMsg = "Updated failed";
                }
            }
            return RedirectToAction("EditSubscriberGroup", new { id = group.SubscriberGroupID });
        }

    }
}