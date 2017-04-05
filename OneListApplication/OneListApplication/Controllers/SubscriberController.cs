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
            if (ModelState.IsValid)
            {
                //TO DO: currently allowing duplicate subscriber group name.
                //TO DO: typo for the database model 
                SuscriberGroup sg = new SuscriberGroup();
                sg.SuscriberGroupName = subscriberGroup.SubscriberGroupName;
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
            SubscriberRepo subscriberRepo = new SubscriberRepo();
            IEnumerable<SubscriberGroupVM> subscriberGroups = subscriberRepo.GetSubscriberGroups();
            return View(subscriberGroups);
        }

        public ActionResult DeleteSubscriberGroup(int id)
        {
            string errMsg = "";
            SubscriberRepo subscriberRepo = new SubscriberRepo();
            subscriberRepo.DeleteGroup(id, out errMsg);
            ViewBag.ErrorMsg = errMsg;
            return RedirectToAction("SubscriberGroupManagement");
        }

        public ActionResult AddSubscriberToGroup(SubscriberGroupVM subGroup) {
            string errMsg = "";
            if (ModelState.IsValid)
            {
                SubscriberRepo subscriberRepo = new SubscriberRepo();
                subscriberRepo.AddUserToGroup(subGroup);
                ViewBag.ErrorMsg = errMsg;
            }
            else
            {
                ViewBag.ErrorMsg = "Cannot add user to group.";
            }
            return RedirectToAction("SubscriberGroupManagement");
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
            if (ModelState.IsValid)
            {
                SubscriberRepo subscriberRepo = new SubscriberRepo();
                ItemUpdated = subscriberRepo.UpdateGroup(group);
                if (ItemUpdated)
                {
                    //return RedirectToAction("ItemDetail", new { id = group.ItemID });
                }
                else
                {
                    ViewBag.ErrorMsg = "Updated failed";
                }
            }
            return View();
        }


    }
}