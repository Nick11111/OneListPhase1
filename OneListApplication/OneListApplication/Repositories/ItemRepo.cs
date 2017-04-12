using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Repositories
{
    public class ItemRepo
    {
        public IEnumerable<ItemVM> GetAll(string userId)
        {

            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<ItemVM> itemList = db.ItemCategories
                    .SelectMany(a => a.Items.Where(b => b.UserID == userId)
                    .Select(it => new ItemVM()
                    {
                        ItemID = it.ItemID,
                        ItemName = it.ItemName,
                        ItemDescription = it.ItemDescription,
                        ItemCategory = (int)it.ItemCategory,
                        ItemCategoryName = a.ItemCategoryName
                    }));
                 
            return itemList;
        }
        public IEnumerable<ItemCategoryVM> GetItemCategories(string userID)
        {

            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<ItemCategoryVM> itemList = db.ItemCategories
                                                    .Where(a=>a.UserID == userID)
                                                    .Select(p =>  new ItemCategoryVM()
                                                    { ItemCategoryID = p.ItemCategoryID,
                                                      ItemCategoryName = p.ItemCategoryName
                                                    });
            return itemList;
        }
        public void CreateItem(ItemVM item, out string errMsg)
        {
            if (String.IsNullOrEmpty(item.ItemName))
            {
                errMsg = "name field empty";
            }else
            {
                OneListEntitiesCore db = new OneListEntitiesCore();
                Item itemAdded = new Item();
                itemAdded.UserID = item.UserID;
                itemAdded.ItemName = item.ItemName;
                itemAdded.ItemDescription = item.ItemDescription;
                itemAdded.ItemCategory = (int)item.ItemCategory;

                db.Items.Add(itemAdded);
                db.SaveChanges();
                errMsg = "item added";
            }
        }

        public IEnumerable<SelectListItem> GetCategories(string userId) {

            OneListEntitiesCore db = new OneListEntitiesCore();
            var categories = db.ItemCategories
                            .Where(a => a.UserID == userId)
                            .Select(x => new SelectListItem
                            {
                                Value = x.ItemCategoryID.ToString(),
                                Text = x.ItemCategoryName
                            });
            return categories;
        }

        public void CreateItemCategory(ItemCategoryVM itemCategory, string userID) {
            ItemCategory c = new ItemCategory();
            c.ItemCategoryName = itemCategory.ItemCategoryName;
            c.ItemCategoryID = 0;
            c.UserID = userID;
            OneListEntitiesCore Core = new OneListEntitiesCore();
            Core.ItemCategories.Add(c);
            Core.SaveChanges();
        }

        public ItemCategoryVM GetCategoryDetails(int itemCategoryID) {
            OneListEntitiesCore db = new OneListEntitiesCore();
            ItemCategory category = db.ItemCategories
                           .Where(ic => ic.ItemCategoryID == itemCategoryID)
                           .FirstOrDefault();
            ItemCategoryVM categoryVM = new ItemCategoryVM();
            categoryVM.ItemCategoryName = category.ItemCategoryName;
            categoryVM.UserID = category.UserID;
            categoryVM.ItemCategoryID = itemCategoryID;
            return categoryVM;
        }

        public bool UpdateItemCategory(ItemCategoryVM itemCategory) {
            OneListEntitiesCore db = new OneListEntitiesCore();
            ItemCategory itemCategoryUpdated = db.ItemCategories
                                                .Where(a => 
                                                    a.ItemCategoryID == itemCategory.ItemCategoryID
                                                 )
                                                .FirstOrDefault();
            itemCategoryUpdated.ItemCategoryName = itemCategory.ItemCategoryName;
            db.SaveChanges();
            return true;
        }
        public ItemVM GetDetails(int itemId)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            Item itemToBeUpdated = db.Items.Where(a => a.ItemID == itemId).FirstOrDefault();
            ItemVM iv = new ItemVM();
            iv.ItemID = itemToBeUpdated.ItemID;
            iv.UserID = itemToBeUpdated.UserID;
            iv.ItemName = itemToBeUpdated.ItemName;
            iv.ItemDescription = itemToBeUpdated.ItemDescription;
            iv.ItemCategory = (int)itemToBeUpdated.ItemCategory;
            ItemCategory categoryDb = db.ItemCategories.Where(a => a.ItemCategoryID == iv.ItemCategory).FirstOrDefault();
            iv.ItemCategoryName = categoryDb.ItemCategoryName;
            return iv;
        }

        public bool UpdateItem(ItemVM item)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            Item itemUpdated = db.Items.Where(a => a.ItemID == item.ItemID).FirstOrDefault();
            itemUpdated.ItemName = item.ItemName;
            itemUpdated.ItemDescription = item.ItemDescription;
            itemUpdated.ItemCategory = item.ItemCategory;

            db.SaveChanges();
            return true;
        }

        public void DeleteItem(int itemId, out string errMsg)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            Item itemToBeDeleted = db.Items.Where(a => a.ItemID == itemId).FirstOrDefault();
            if(itemToBeDeleted != null)
            {
                db.Items.Remove(itemToBeDeleted);
                db.SaveChanges();
                errMsg = "Item Deleted";
            }else
            {
                errMsg = "Item could not be deleted.";
            }
        }

        public void DeleteItemCategory(int categoryID, out string errMsg) {
            OneListEntitiesCore db = new OneListEntitiesCore();
            ItemCategory itemCategoryToBeDeleted = db.ItemCategories
                                    .Where(a => a.ItemCategoryID == categoryID)
                                    .FirstOrDefault();
            Item firstItemInCategory = db.Items
                                       .Where(a => a.ItemCategory == categoryID)
                                       .FirstOrDefault();
        
            if (itemCategoryToBeDeleted != null)
            {
                // check if there are items in this category
                if (firstItemInCategory != null)
                {
                    errMsg = "Item Category has items associated, cannot be deleted";
                }
                else {
                    db.ItemCategories.Remove(itemCategoryToBeDeleted);
                    db.SaveChanges();
                    errMsg = "Item Category Deleted";
                }
            }
            else
            {
                errMsg = "Item Category could not be deleted.";
            }
        }
    }
}