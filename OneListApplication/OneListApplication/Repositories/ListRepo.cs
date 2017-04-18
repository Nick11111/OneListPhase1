﻿using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Repositories
{
    public class ListRepo
    {
        public ListVM CreateList(string UserID)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            ListVM cleanList = new ListVM(); 
            cleanList.ListType = db.ListTypes.Select(s => s);
            cleanList.ItemCategory = db.ItemCategories.Where(cat => cat.UserID==UserID).Select(s => s);
            cleanList.SuscriberGroup = db.SuscriberGroups.Where( group => group.UserID==UserID).Select(s => s);
            return cleanList;
        }
        public bool DeleteList(int List)
        {
            try
            {
                //items, users and then list
                OneListEntitiesCore db = new OneListEntitiesCore();
                //1.- first delete items.
                IEnumerable<ListItem> lItems = db.ListItems.Where(li => li.ListID == List).Select(p => p);
                db.ListItems.RemoveRange(lItems);
                //2.- remove users
                IEnumerable<ListUser> lUsers = db.ListUsers.Where(lu => lu.ListID == List).Select(p => p);
                db.ListUsers.RemoveRange(lUsers);
                //3.- finally Remove list
                List L = db.Lists.Where(l => l.ListID == List).Select(p => p).First();
                db.Lists.Remove(L);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
         
            return true;
        }
        public IEnumerable<ListViewVM> GetLists(string UserID)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<List> lists = db.Lists.Where(l => l.CreatorID == UserID).Select(list => list);

            IEnumerable<ListViewVM> ListReturn;
            var r = new List<ListViewVM>();
            foreach (List  single in lists)
            {
                ListViewVM oneList=new ListViewVM();
                oneList.CreatorID = UserID;
                oneList.ListStatusID = single.ListStatusID;
                oneList.ListTypeID = single.ListTypeID;
                oneList.ListID = single.ListID;
                oneList.CreationDate = single.CreationDate.ToShortDateString();
                oneList.ListName = single.ListName;
                oneList.SuscriberGroup = from groups in db.SuscriberGroups
                                         join ListUser l in db.ListUsers
                                             on groups.SuscriberGroupID equals l.SuscriberGroupID 
                                         join List li in db.Lists
                                             on l.ListID equals li.ListID
                                         where li.CreatorID == UserID
                                         where li.ListID == oneList.ListID
                                         select groups;

                oneList.ListType = db.ListTypes.Where(lt => lt.ListTypeID == single.ListTypeID).Select(p => p).FirstOrDefault().TypeName;
                r.Add(oneList);
            }
            ListReturn = (IEnumerable<ListViewVM>)r;

            return ListReturn;
        }
        public ListViewVM getList(int id, string UserID)
        {
            ListViewVM resultList = new ListViewVM();

            //first, check if the user is the creator
            OneListEntitiesCore db = new OneListEntitiesCore();

            IEnumerable<List> lists = db.Lists.Where(l => l.CreatorID == UserID && l.ListID == id).Select(list => list);

            if (lists.Count() > 0)
            {
                List single = lists.First();
                //the user is the creator
                resultList.CreatorID = UserID;
                resultList.suscriberRole = "List Publisher";
                resultList.UserType = 0;
                resultList.ListStatusID = single.ListStatusID;
                resultList.ListTypeID = single.ListTypeID;
                resultList.ListID = single.ListID;
                resultList.CreationDate = single.CreationDate.ToShortDateString();
                resultList.ListName = single.ListName;
                resultList.SuscriberGroup = from groups in db.SuscriberGroups
                                         join ListUser l in db.ListUsers
                                             on groups.SuscriberGroupID equals l.SuscriberGroupID
                                         join List li in db.Lists
                                             on l.ListID equals li.ListID
                                         where li.CreatorID == UserID
                                         where li.ListID == resultList.ListID
                                         select groups;
                resultList.ListType = db.ListTypes.Where(lt => lt.ListTypeID == single.ListTypeID).Select(p => p).FirstOrDefault().TypeName;
                //TO-DO: provide the list items for that list
                IEnumerable<ListItem> itemsList = db.ListItems.Where(l => l.ListID == id).Select(p => p);
                var itemListFinal= new List<ListItemVM>();

                foreach (ListItem l in itemsList)
                {
                    ListItemVM addItem = new ListItemVM();
                    addItem.ListID = id;
                    addItem.ItemID = l.ItemID;
                    addItem.ListItemSolved = l.ListItemSolved;
                    if (addItem.ListItemSolved)
                    {
                        User currentUser = db.Users.Where(u => u.UserID == l.ListItemSolver).Select(p => p).First();
                        addItem.ListItemSolverName = currentUser.FirstName + " " + currentUser.LastName;
                    }
                    
                    addItem.ListItemNotes = l.ListItemNotes;
                    try
                    {
                        addItem.ListItemCost = (decimal)l.ListItemCost;
                    }
                    catch
                    {
                        addItem.ListItemCost = 0;
                    }
                    
                    addItem.listItemName = db.Items.Where(i => i.ItemID == l.ItemID).Select(p => p).First().ItemDescription;
                    //finally adding the itemlist to the  vm
                    itemListFinal.Add(addItem);
                }

                resultList.items = (IEnumerable<ListItemVM>)itemListFinal;
            }
            else
            {
                //check if he is a suscriber or a colaborator
                IEnumerable<List> suscriptions = from list in db.Lists
                                          join ListUser luser in db.ListUsers
                                              on list.ListID equals luser.ListID
                                          join SuscriberGroupUser sg in db.SuscriberGroupUsers
                                              on luser.SuscriberGroupID equals sg.SuscriberGroupID
                                          where sg.UserID == UserID
                                          where list.ListID == id
                                          select list;
                IEnumerable<SuscriberGroupUser> user = from list in db.Lists
                                                       join ListUser luser in db.ListUsers
                                                           on list.ListID equals luser.ListID
                                                       join SuscriberGroupUser sg in db.SuscriberGroupUsers
                                                           on luser.SuscriberGroupID equals sg.SuscriberGroupID
                                                       where sg.UserID == UserID
                                                       where list.ListID == id
                                                       select sg;
                SuscriberGroupUser userFinal = user.First();
                if (suscriptions.Count() > 0)
                {
                    //this user is a suscriber
                    List single = suscriptions.First();
                    //the user is the creator
                    resultList.CreatorID = UserID;
                    UserType currentUser = db.UserTypes.Where(ut => ut.UserTypeID == userFinal.UserTypeID).Select(o => o).FirstOrDefault();
                    resultList.suscriberRole = currentUser.UserTypeName; ;
                    resultList.UserType = user.First().UserTypeID;
                    resultList.ListStatusID = single.ListStatusID;
                    resultList.ListTypeID = single.ListTypeID;
                    resultList.ListID = single.ListID;
                    resultList.CreationDate = single.CreationDate.ToShortDateString();
                    resultList.ListName = single.ListName;
                    resultList.SuscriberGroup = from groups in db.SuscriberGroups
                                                join ListUser l in db.ListUsers
                                                    on groups.SuscriberGroupID equals l.SuscriberGroupID
                                                join List li in db.Lists
                                                    on l.ListID equals li.ListID
                                                where li.CreatorID == UserID
                                                where li.ListID == resultList.ListID
                                                select groups;
                    resultList.ListType = db.ListTypes.Where(lt => lt.ListTypeID == single.ListTypeID).Select(p => p).FirstOrDefault().TypeName;
                    //TO-DO: provide the list items for that list
                    IEnumerable<ListItem> itemsList = db.ListItems.Where(l => l.ListID == id).Select(p => p);
                    var itemListFinal = new List<ListItemVM>();

                    foreach (ListItem l in itemsList)
                    {

                        ListItemVM addItem = new ListItemVM();
                        addItem.ListID = id;
                        addItem.ItemID = l.ItemID;
                        addItem.ListItemSolved = l.ListItemSolved;
                        if (addItem.ListItemSolved)
                        {
                            User cUser = db.Users.Where(u => u.UserID == l.ListItemSolver).Select(p => p).First();
                            addItem.ListItemSolverName = cUser.FirstName + " " + cUser.LastName;
                        }

                        addItem.ListItemNotes = l.ListItemNotes;
                        try
                        {
                            addItem.ListItemCost = (decimal)l.ListItemCost;
                        }
                        catch
                        {
                            addItem.ListItemCost = 0;
                        }

                        addItem.listItemName = db.Items.Where(i => i.ItemID == l.ItemID).Select(p => p).First().ItemDescription;
                        //finally adding the itemlist to the  vm
                        itemListFinal.Add(addItem);
                    }

                    resultList.items = (IEnumerable<ListItemVM>)itemListFinal;
                }
                else
                {
                    // he is not a suscriber either, we wont show anything
                }

            }


            return resultList;

        }
        public IEnumerable<ListViewVM> GetSuscribedLists(string UserID)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<List> lists = from list in db.Lists
                                      join ListUser luser in db.ListUsers
                                          on list.ListID equals luser.ListID
                                      join SuscriberGroupUser sg in db.SuscriberGroupUsers
                                          on luser.SuscriberGroupID equals sg.SuscriberGroupID
                                      where sg.UserID == UserID
                                      select list;

            IEnumerable<SuscriberGroupUser> user = from list in db.Lists
                                      join ListUser luser in db.ListUsers
                                          on list.ListID equals luser.ListID
                                      join SuscriberGroupUser sg in db.SuscriberGroupUsers
                                          on luser.SuscriberGroupID equals sg.SuscriberGroupID
                                      where sg.UserID == UserID
                                      select sg;
            SuscriberGroupUser userFinal = user.First();
            

            IEnumerable<ListViewVM> ListReturn;
            var r = new List<ListViewVM>();
            foreach (List single in lists)
            {
                ListViewVM oneList = new ListViewVM();
                oneList.CreatorID = UserID;
                oneList.ListStatusID = single.ListStatusID;
                oneList.ListTypeID = single.ListTypeID;
                oneList.ListID = single.ListID;
                oneList.CreationDate = single.CreationDate.ToShortDateString();
                oneList.ListName = single.ListName;
                oneList.SuscriberGroup = from groups in db.SuscriberGroups
                                         join ListUser l in db.ListUsers
                                             on groups.SuscriberGroupID equals l.SuscriberGroupID
                                         join List li in db.Lists
                                             on l.ListID equals li.ListID
                                         where li.CreatorID == UserID
                                         where li.ListID == oneList.ListID
                                         select groups;
                UserType currentUser = db.UserTypes.Where(ut => ut.UserTypeID ==userFinal.UserTypeID).Select(o => o).FirstOrDefault();
                oneList.suscriberRole = currentUser.UserTypeName;
                oneList.UserType = user.First().UserTypeID;
                oneList.ListType = db.ListTypes.Where(lt => lt.ListTypeID == single.ListTypeID).Select(p => p).FirstOrDefault().TypeName;
                r.Add(oneList);
            }
            ListReturn = (IEnumerable<ListViewVM>)r;

            return ListReturn;
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
                    lItem.ListItemSolved = false;
                    lItem.ListItemSolvingDate = DateTime.Today;
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