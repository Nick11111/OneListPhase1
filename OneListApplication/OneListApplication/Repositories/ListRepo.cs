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
        const int COMPLETED = 3;
        const int PROCESS = 1;
        public ListVM CreateList(string UserID)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            ListVM cleanList = new ListVM(); 
            cleanList.ListType = db.ListTypes.Select(s => s);
            cleanList.ItemCategory = db.ItemCategories.Where(cat => cat.UserID==UserID).Select(s => s);
            cleanList.SuscriberGroup = db.SuscriberGroups.Where( group => group.UserID==UserID).Select(s => s);
            return cleanList;
        }
        public bool CompleteList(int id,string userID)
        {
            try
            {
                //items, users and then list
                OneListEntitiesCore db = new OneListEntitiesCore();
                //1.- complete list.
                List l = db.Lists.Where(p => p.ListID == id).Select(r => r).First();
                //change status to complete
                l.ListStatusID = COMPLETED;//completed
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

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
        public bool updateItemList(string userID,int itemId, int id, bool solved, decimal cost, string notes)
        {
            try
            {
                //items, users and then list
                OneListEntitiesCore db = new OneListEntitiesCore();
                //1.- first delete items.
                ListItem l = db.ListItems.Where(d => d.ItemID == itemId && d.ListID == id).Select(g => g).First();
                l.ListItemSolved = solved;
                l.ListItemCost = cost;
                l.ListItemNotes = notes;
                l.ListItemSolver = userID;
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
            IEnumerable<List> lists = db.Lists.Where(l => l.CreatorID == UserID && l.ListStatusID==PROCESS).Select(list => list);

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
        public IEnumerable<ListViewVM> GetCompletedLists(string UserID)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<List> lists = db.Lists.Where(l => l.CreatorID == UserID && l.ListStatusID == COMPLETED).Select(list => list);

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

                //cmbtypes

                List<SelectListItem> selectedLists = new List<SelectListItem>();
                IEnumerable<ListType> currentTypes = db.ListTypes.Select(p => p);

                foreach (ListType L in currentTypes)
                {
                    SelectListItem lFrom = new SelectListItem();
                    lFrom.Value = L.ListTypeID.ToString();
                    lFrom.Text = L.TypeName;
                    if (L.ListTypeID == resultList.ListTypeID)
                    {
                        lFrom.Selected = true;
                    }
                    selectedLists.Add(lFrom);
   
                }

                resultList.listTypes = (IEnumerable <SelectListItem>) selectedLists;
                //cmb groups

               
                List<SelectListItem> selectedGroups = new List<SelectListItem>();
                IEnumerable<SuscriberGroup> currentGroups = db.SuscriberGroups.Where(r => r.UserID==UserID).Select(p => p);

                foreach (SuscriberGroup M in currentGroups)
                {
                    SelectListItem lFrom = new SelectListItem();
                    lFrom.Value = M.SuscriberGroupID.ToString();
                    lFrom.Text = M.SuscriberGroupName;
                    SuscriberGroup gro = resultList.SuscriberGroup.Where(p => p.SuscriberGroupID == M.SuscriberGroupID).Select(g => g).FirstOrDefault();
                    if (gro!=null)
                    {
                        lFrom.Selected = true;
                    }
                    selectedGroups.Add(lFrom);

                }

                resultList.suscribergroups = (IEnumerable<SelectListItem>)selectedGroups;

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
                    
                    addItem.listItemName = db.Items.Where(i => i.ItemID == l.ItemID).Select(p => p).First().ItemName;
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
            IEnumerable<ListViewVM> ListReturn;
            try
            {
                OneListEntitiesCore db = new OneListEntitiesCore();
                IEnumerable<List> lists = from list in db.Lists
                                          join ListUser luser in db.ListUsers
                                              on list.ListID equals luser.ListID
                                          join SuscriberGroupUser sg in db.SuscriberGroupUsers
                                              on luser.SuscriberGroupID equals sg.SuscriberGroupID
                                          where sg.UserID == UserID && sg.UserTypeID != 1
                                          select list;

                IEnumerable<SuscriberGroupUser> user = from list in db.Lists
                                                       join ListUser luser in db.ListUsers
                                                           on list.ListID equals luser.ListID
                                                       join SuscriberGroupUser sg in db.SuscriberGroupUsers
                                                           on luser.SuscriberGroupID equals sg.SuscriberGroupID
                                                       where sg.UserID == UserID && sg.UserTypeID != 1
                                                       select sg;
                SuscriberGroupUser userFinal = user.First();


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
                    UserType currentUser = db.UserTypes.Where(ut => ut.UserTypeID == userFinal.UserTypeID).Select(o => o).FirstOrDefault();
                    oneList.suscriberRole = currentUser.UserTypeName;
                    oneList.UserType = user.First().UserTypeID;
                    oneList.ListType = db.ListTypes.Where(lt => lt.ListTypeID == single.ListTypeID).Select(p => p).FirstOrDefault().TypeName;
                    r.Add(oneList);
                }
                ListReturn = (IEnumerable<ListViewVM>)r;

            }
            catch
            {
                ListReturn = null;
            }
            return ListReturn;

        }
        public bool deleteItemList(int itemID, int listID)
        {
            try
            {
                OneListEntitiesCore db = new OneListEntitiesCore();
                ListItem l = db.ListItems.Where(p => p.ItemID == itemID && p.ListID == listID).Select(r => r).First();
                db.ListItems.Remove(l);
                db.SaveChanges();
                return true;

            }
            catch 
            {
                return false;
            }
           
        }
        public bool UpdateListData(int id, string name, int listType, int[] groups)
        {
            //update lists 
            try
            {
                OneListEntitiesCore db = new OneListEntitiesCore();
                //first, select List

                List newList = db.Lists.Where(p => p.ListID == id).Select(r => r).FirstOrDefault();
                //change name
                newList.ListName = name;
                //change type
                newList.ListTypeID = listType;

                //last part,delete groups first, add them again

                IEnumerable<ListUser> currentGroups = db.ListUsers.Where(p => p.ListID==id).Select(r => r);
                db.ListUsers.RemoveRange(currentGroups);
                db.SaveChanges();
                foreach (int group in groups)
                {
                    ListUser Luser = new ListUser();
                    Luser.ListID = id;
                    Luser.SuscriberGroupID = group;
                    Luser.SuscriptionDate = DateTime.Today.ToShortDateString();

                    db.ListUsers.Add(Luser);
                    db.SaveChanges();

                }




                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }


            return true;
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
                string[] groups = list.SuscribergroupID.Split(',');
                foreach (string group in groups)
                {
                    ListUser Luser = new ListUser();
                    Luser.ListID = createdList.ListID;
                    Luser.SuscriberGroupID = int.Parse(group);
                    Luser.SuscriptionDate = list.CreationDate.ToShortDateString();

                    ListUser createdLuser = db.ListUsers.Add(Luser);
                    db.SaveChanges();

                }


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