using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public IEnumerable<ItemCategoryVM> GetItemCategories()
        {

            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<ItemCategoryVM> itemList = db.ItemCategories.Select(p =>  new ItemCategoryVM() { ItemCategoryID = p.ItemCategoryID, ItemCategoryName= p.ItemCategoryName });
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
            Item itemToBeUpdated = db.Items.Where(a => a.ItemID == itemId).FirstOrDefault();
            if(itemToBeUpdated != null)
            {
                db.Items.Remove(itemToBeUpdated);
                db.SaveChanges();
                errMsg = "Item Deleted";
            }else
            {
                errMsg = "Item could not be deleted.";
            }
        }
    }
}