using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Repositories
{
    public class ListRepo
    {
        public ListVM CreateList()
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            ListVM cleanList = new ListVM(); 
            cleanList.ListType = db.ListTypes.Select(s => s);
            cleanList.ItemCategory = db.ItemCategories.Select(s => s);
            cleanList.SuscriberGroup = db.SuscriberGroups.Select(s => s);
            return cleanList;
        }

        public bool CreateList(ListVM list)
        {
            try
            {
                OneListEntitiesCore db = new OneListEntitiesCore();
                //first, create List

                List newList = new List();

                newList.CreationDate = list.CreationDate;
                newList.CreatorID = list.CreatorID;
                newList.ListName = list.ListName;
                newList.ListTypeID = list.ListTypeID;
                newList.ListStatusID = 1;

                List createdList = db.Lists.Add(newList);
                //then create list items and users 
                db.SaveChanges();
                //list users
                ListUser Luser = new ListUser();
                Luser.ListID = createdList.ListID;
                Luser.SuscriberGroupID = list.SuscribergroupID;
                Luser.SuscriptionDate = list.CreationDate.ToShortDateString();

                ListUser createdLuser = db.ListUsers.Add(Luser);
                db.SaveChanges();

                //listitems
                IEnumerable<Item> selectedItems = db.Items
                                               .Where(a => a.ItemCategory == list.ItemCategoryID && a.UserID == list.CreatorID)
                                               .Select(s => s);

                foreach (Item sel in selectedItems)
                {
                    ListItem lItem = new ListItem();
                    lItem.ItemID = sel.ItemID;
                    lItem.ListID = createdList.ListID;
                    db.ListItems.Add(lItem);
                }
                db.SaveChanges();
                return true;

            }
            catch(Exception ex) {
                return false;
            }
            


        }
    }
}