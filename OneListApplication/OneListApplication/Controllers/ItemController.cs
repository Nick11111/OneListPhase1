using OneListApplication.Repositories;
using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Controllers
{
    public class ItemController : Controller
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
        [HttpGet]
        public ActionResult ItemManagement()
        {
            ViewBag.ItemActionMsg = TempData["ItemActionMsg"];
            string userId = FindUserID();
            ItemRepo itemRepo = new ItemRepo();
            IEnumerable<ItemVM> items = itemRepo.GetAll(userId);
            return View(items);
        }
        [HttpGet]
        public ActionResult CreateItem()
        {
            var model = new ItemVM
            {
                ItemCategoryList = GetCategories()
            };
            return View(model);
        }

        private IEnumerable<SelectListItem> GetCategories()
        {
            string userId = FindUserID();
            ItemRepo itemRepo = new ItemRepo();
            var categories = itemRepo.GetCategories(userId);
            return new SelectList(categories, "Value", "Text");
        }
        [HttpPost]
        public ActionResult CreateItem(ItemVM item)
        {
            item.UserID = FindUserID();
            //string errMsg = "";
            if (ModelState.IsValid)
            {
                ItemRepo itemRepo = new ItemRepo();
                itemRepo.CreateItem(item);
                //TempData["ItemActionMsg"] = errMsg;
                return RedirectToAction("ItemManagement");
            }
            else
            {
                //TempData["ItemCategoryActionMsg"] = "Cannot add item.";
                var model = new ItemVM
                {
                    ItemCategoryList = GetCategories()
                };
                return View(model);
            }

        }
        [HttpGet]
        public ActionResult ItemDetail(int id)
        {
            ItemRepo itemRepo = new ItemRepo();
            return View(itemRepo.GetDetails(id));
        }

        [HttpGet]
        public ActionResult EditItem(int id)
        {
            ItemRepo itemRepo = new ItemRepo();
            ItemVM item = itemRepo.GetDetails(id);
            ViewBag.Categories = new SelectList(GetCategories(), "Value",
                             "Text", id);
            return View(item);
        }
        [HttpPost]
        public ActionResult EditItem(ItemVM item)
        {
            bool ItemUpdated;
            if (ModelState.IsValid)
            {
                ItemRepo itemRepo = new ItemRepo();
                ItemUpdated = itemRepo.UpdateItem(item);
                if (ItemUpdated)
                {
                    return RedirectToAction("ItemDetail", new { id = item.ItemID });
                }else
                {
                    ViewBag.ErrorMsg = "Updated failed";
                }
            }
            ItemRepo itemRepo2 = new ItemRepo();
            ItemVM item2 = itemRepo2.GetDetails(item.ItemID);
            ViewBag.Categories = new SelectList(GetCategories(), "Value",
                             "Text", item.ItemID);
            return View(item2);
        }

        [HttpGet]
        public ActionResult DeleteItem(int id)
        {
            string errMsg = "";
            ItemRepo itemRepo = new ItemRepo();
            itemRepo.DeleteItem(id, out errMsg);
            TempData["ItemActionMsg"] = errMsg;
            return RedirectToAction("ItemManagement");
        }
        //---------------Actions for Item Categories, group == category----------------//
        [HttpGet]
        public ActionResult ItemCategoryManagement()
        {
            ViewBag.ItemCategoryActionMsg = TempData["ItemCategoryActionMsg"];
            string userId = FindUserID();
            ItemRepo itemRepo = new ItemRepo();
            IEnumerable<ItemCategoryVM> items = itemRepo.GetItemCategories(userId);
            return View(items);
        }
        [HttpGet]
        public ActionResult CreateItemCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateItemCategory(ItemCategoryVM itemCategory)
        {
            string userID = FindUserID();
            if (ModelState.IsValid)
            {
                ItemRepo itemRepo = new ItemRepo();
                itemRepo.CreateItemCategory(itemCategory, userID);
                return RedirectToAction("ItemCategoryManagement");
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        public ActionResult ItemCategoryDetail(int id)
        {
            ItemRepo itemRepo = new ItemRepo();
            return View(itemRepo.GetCategoryDetails(id));
        }

        [HttpGet]
        public ActionResult EditItemCategory(int id)
        {
            ItemRepo itemRepo = new ItemRepo();
            ItemCategoryVM itemCategory = itemRepo.GetCategoryDetails(id);
            return View(itemCategory);
        }

        [HttpPost]
        public ActionResult EditItemCategory(ItemCategoryVM itemCategory)
        {
            bool CategoryUpdated;
            if (ModelState.IsValid)
            {
                ItemRepo itemRepo = new ItemRepo();
                CategoryUpdated = itemRepo.UpdateItemCategory(itemCategory);
                if (CategoryUpdated)
                {
                    return RedirectToAction("ItemCategoryDetail", new { id = itemCategory.ItemCategoryID });
                }
                else
                {
                    ViewBag.ErrorMsg = "Updated item category failed";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteItemCategory(int id) {
            string errMsg = "";
            ItemRepo itemRepo = new ItemRepo();
            itemRepo.DeleteItemCategory(id, out errMsg);
            TempData["ItemCategoryActionMsg"] = errMsg;
            return RedirectToAction("ItemCategoryManagement");
        }
    }
}