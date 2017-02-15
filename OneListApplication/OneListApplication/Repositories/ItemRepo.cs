﻿using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneListApplication.Repositories
{
    public class ItemRepo
    {
        public IEnumerable<ItemVM> GetAll()
        {
            OneListCAEntities db = new OneListCAEntities();
            IEnumerable<ItemVM> itemList = db.Items
                    .Select(it => new ItemVM()
                    {
                        ItemID = it.ItemID,
                        ItemName = it.ItemName,
                        ItemDescription = it.ItemDescription,
                        ItemCategory = (int)it.ItemCategory
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
                OneListCAEntities db = new OneListCAEntities();
                Item itemAdded = new Item();
                itemAdded.UserID = item.UserID;
                itemAdded.ItemName = item.ItemName;
                itemAdded.ItemDescription = item.ItemDescription;
                itemAdded.ItemCategory = item.ItemCategory;

                db.Items.Add(itemAdded);
                db.SaveChanges();
                errMsg = "item added";
            }

        }

        public ItemVM GetDetails(int itemId)
        {
            OneListCAEntities db = new OneListCAEntities();
            Item itemToBeUpdated = db.Items.Where(a => a.ItemID == itemId).FirstOrDefault();
            ItemVM iv = new ItemVM();
            iv.ItemID = itemToBeUpdated.ItemID;
            iv.UserID = itemToBeUpdated.UserID;
            iv.ItemName = itemToBeUpdated.ItemName;
            iv.ItemDescription = itemToBeUpdated.ItemDescription;
            iv.ItemCategory = (int)itemToBeUpdated.ItemCategory;
            return iv;
        }

        public bool UpdateItem(ItemVM item)
        {
            OneListCAEntities db = new OneListCAEntities();
            Item itemUpdated = db.Items.Where(a => a.ItemID == item.ItemID).FirstOrDefault();
            itemUpdated.ItemName = item.ItemName;
            itemUpdated.ItemDescription = item.ItemDescription;
            itemUpdated.ItemCategory = item.ItemCategory;

            db.SaveChanges();
            return true;
        }

        public void DeleteItem(int itemId, out string errMsg)
        {
            OneListCAEntities db = new OneListCAEntities();
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