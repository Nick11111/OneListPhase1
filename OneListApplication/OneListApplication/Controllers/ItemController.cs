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
        [HttpGet]
        public ActionResult ItemManagement()
        {
            ItemRepo itemRepo = new ItemRepo();
            IEnumerable<ItemVM> items = itemRepo.GetAll();
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
            OneListEntitiesCore db = new OneListEntitiesCore();
            var categories = db.ItemCategories
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ItemCategoryID.ToString(),
                                    Text = x.ItemCategoryName
                                });

            return new SelectList(categories, "Value", "Text");
        }
        [HttpPost]
        public ActionResult CreateItem(ItemVM item)
        {
            string errMsg = "";
            if (ModelState.IsValid)
            {
                //currently allowing duplicate item name.
                ItemRepo itemRepo = new ItemRepo();
                itemRepo.CreateItem(item, out errMsg);
                ViewBag.ErrorMsg = errMsg;
                return RedirectToAction("ItemManagement");
            }
            else
            {
                ViewBag.ErrorMsg = "Cannot add item.";
            }
            return View();
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
            return View();
        }

        [HttpGet]
        public ActionResult DeleteItem(int id)
        {
            string errMsg = "";
            ItemRepo itemRepo = new ItemRepo();
            itemRepo.DeleteItem(id, out errMsg);
            ViewBag.ErrorMsg = errMsg;
            return RedirectToAction("ItemManagement");
        }
        //---------------Actions for Item Categories, group == category----------------//
        [HttpGet]
        public ActionResult ItemCategoryManagement()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateItemCategory()
        {
            return View();
        }
        //TO DO: create post action for CreateItemGroup()
        [HttpGet]
        public ActionResult ItemCategoryDetail()
        {
            return View();
        }
        //TO DO: create post action for updateItemGroup()

        //TO DO: delete category --- how to delete if some items belong to this category
    }
}