using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Repositories
{
    public class SubscriberRepo
    {
        const int USER_TYPE_COLLABORATOR_INDEX  = 2;
        const int USER_TYPE_SUBSCRIBER_INDEX    = 3;
        const int DEFAULT_USER_TYPE             = 3;
        const string DEFAULT_STATUS             = "Active";
        const string DISABLED_STATUS            = "Disabled";

        /* *******************************************************
        * Get all subscriber Group
        * Return: void
        ********************************************************/
        public IEnumerable<SubscriberGroupVM> GetSubscriberGroups(string publisherID)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<SubscriberGroupVM> subscriberGroups = db.SuscriberGroups
                                                .Where(a=>a.UserID == publisherID)
                                                .Select(s => new SubscriberGroupVM()
                                                    { SubscriberGroupID = s.SuscriberGroupID,
                                                        SubscriberGroupName = s.SuscriberGroupName
                                                });
            return subscriberGroups;
        }
        /* *******************************************************
        * Delete subscriber Group
        * Parameter: Int subscriberGroupId
        * Return: out errMsg (string)
        ********************************************************/
        public void DeleteGroup(string publisherID, int subscriberGroupId, out string errMsg)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroup groupToBeUpdated = db.SuscriberGroups
                                            .Where(s => 
                                                s.SuscriberGroupID == subscriberGroupId
                                            ).FirstOrDefault();
            int numOfSubscribers = db.SuscriberGroupUsers
                                            .Where(s =>
                                                s.SuscriberGroupID == subscriberGroupId
                                            ).Count();
            int numOfLists = db.ListUsers
                             .Where(l => l.SuscriberGroupID == subscriberGroupId)
                             .Count();

            if (numOfSubscribers > 0)
            {
                errMsg = "Group has subscribers and cannot be deleted";
            }
            else {
                if (numOfLists > 0)
                {
                    errMsg = "Group could not be deleted because it's used in lists.";
                }
                else {
                    if (groupToBeUpdated != null)
                    {
                        db.SuscriberGroups.Remove(groupToBeUpdated);
                        db.SaveChanges();
                        errMsg = "Group Deleted";
                    }
                    else
                    {
                        errMsg = "Group could not be deleted.";
                    }
                }
            }
        }
        /* *******************************************************
        * AddUserToGroup
        * Parameter: string userID
        ********************************************************/
        public void AddUserToGroup(SubscriberGroupVM subGroup, out string errMsg) {
            // TO DO: server side validation & client side validation
            var now = DateTime.UtcNow;
            OneListEntitiesCore db = new OneListEntitiesCore();

            SuscriberGroup sGroup = db.SuscriberGroups.Where(a => a.SuscriberGroupID == subGroup.SubscriberGroupID).FirstOrDefault();
            SuscriberGroupUser newGroupUser = new SuscriberGroupUser();
            newGroupUser.UserID = subGroup.UserID;
            newGroupUser.SuscriberGroupID = subGroup.SubscriberGroupID;
            newGroupUser.UserTypeID = DEFAULT_USER_TYPE;
            newGroupUser.ListUserStatus = DEFAULT_STATUS;
            newGroupUser.SuscriptionDate = now.ToShortDateString();
            newGroupUser.SuscriberGroup = sGroup; //Add subscriberGroup property !important

            var query = db.SuscriberGroupUsers.Add(newGroupUser);
            SuscriberGroupUser existingUser = db.SuscriberGroupUsers
                                    .Where(a =>
                                        a.SuscriberGroupID == subGroup.SubscriberGroupID &&
                                        a.UserID == subGroup.UserID
                                    ).FirstOrDefault();

            // Check if the user currently exist in the table
            if (existingUser == null)
            {
                db.SaveChanges();
                errMsg = "User Added.";
            }
            else {
                errMsg = "User Already Exist.";
            }
        }

        /* *******************************************************
        * GetGroupDetails
        * Parameter: int GroupID
        * return: SubscriberGroupVM
        ********************************************************/
        public SubscriberGroupVM GetGroupDetails(int id) {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroup groupToBeUpdated = db.SuscriberGroups
                                            .Where(a => 
                                                a.SuscriberGroupID == id
                                            ).FirstOrDefault();
            SubscriberGroupVM sg = new SubscriberGroupVM();
            sg.SubscriberGroupID = groupToBeUpdated.SuscriberGroupID;
            sg.SubscriberGroupName = groupToBeUpdated.SuscriberGroupName;
            sg.UserList = GetAllUsers();
            sg.allSubscribedUsers = GetAllSubscribedUsers(id);
            return sg;
        }
        /* *******************************************************
        * GetAllSubscribedUsers
        * Return: IEnumerable<SelectListItem>
        ********************************************************/
        public IEnumerable<SubscriberGroupUserVM> GetAllSubscribedUsers(int id)
        {
           // IEnumerable<SubscriberGroupUserVM> groupUserList;
            OneListEntitiesCore db = new OneListEntitiesCore();

            var groupUserList = db.SuscriberGroups
                            .Where(a => a.SuscriberGroupID == id)
                            .SelectMany(groups =>
                                    groups.SuscriberGroupUsers.Select(
                                        x =>
                                            new SubscriberGroupUserVM
                                            {
                                                UserID = x.UserID,
                                                ListUserStatus = x.ListUserStatus,
                                                UserTypeID = x.UserTypeID,
                                                Email = x.User.Email,
                                                FullName = x.User.FirstName + " " + x.User.LastName
                                    }));
            return groupUserList;
        }

        /* *******************************************************
        * GetAllUsers
        * Return: IEnumerable<SelectListItem>
        ********************************************************/
        public IEnumerable<SelectListItem> GetAllUsers() {
            OneListEntitiesCore db = new OneListEntitiesCore();
            var categories = db.Users
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.UserID.ToString(),
                                    Text = x.Email
                                });

            return new SelectList(categories, "Value", "Text");
        }
        /* *******************************************************
        * UpdateGroup
        * Return: bool
        ********************************************************/
        public bool UpdateGroup(SubscriberGroupVM subscriberGroup)
        {
            try {
                    OneListEntitiesCore db = new OneListEntitiesCore();
                    SuscriberGroup groupUpdated = db.SuscriberGroups
                                                .Where(a =>
                                                    a.SuscriberGroupID == subscriberGroup.SubscriberGroupID
                                                ).FirstOrDefault();
                    groupUpdated.SuscriberGroupName = subscriberGroup.SubscriberGroupName;
                    db.SaveChanges();

                    return true;
                }

                catch (Exception ex)
                {
                    return false;
                }
        }
        /* *******************************************************
        * GetSubscriberGroupUsers
        * Return: IEnumerable<SubscriberGroupUserVM>
        ********************************************************/
        public IEnumerable<SubscriberGroupUserVM> GetSubscriberGroupUsers(int id)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<SubscriberGroupUserVM> subscriberGroupUsers = db.SuscriberGroupUsers.Select(a =>
                                                        new SubscriberGroupUserVM()
                                                        { SubscriberGroupID = id,
                                                          UserID = a.UserID,
                                                          ListUserStatus = a.ListUserStatus,
                                                          UserTypeID = a.UserTypeID,
                                                          SubscriptionDate = a.SuscriptionDate
                                                        });
            return subscriberGroupUsers;
        }
        /* *******************************************************
        * DeleteSubscriber
        * Void
        ********************************************************/
        public void DeleteSubscriber(string userId, int id, out string errMsg) {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroupUser subscriberToBeDeleted =  db.SuscriberGroupUsers
                                                    .Where(s=>
                                                        s.UserID == userId && 
                                                        s.SuscriberGroupID == id
                                                    )
                                                    .FirstOrDefault();

            if (subscriberToBeDeleted != null)
            {
                db.SuscriberGroupUsers.Remove(subscriberToBeDeleted);
                db.SaveChanges();
                errMsg = "Subscriber Deleted";
            }
            else
            {
                errMsg = "Subscriber could not be deleted.";
            }
        }
        /* *******************************************************
        * Change Subscriber Type
        * Void
        ********************************************************/
        public void ChangeSubscriberType(string userId, int id, out string errMsg)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroupUser subscriberTypeToBeChanged = db.SuscriberGroupUsers
                                                    .Where(s =>
                                                        s.UserID == userId &&
                                                        s.SuscriberGroupID == id 
                                                    )
                                                    .FirstOrDefault();

            if (subscriberTypeToBeChanged != null)
            {
                subscriberTypeToBeChanged.UserTypeID = GetTypeToBeChanged(subscriberTypeToBeChanged.UserTypeID);
                db.SaveChanges();
                errMsg = "Subscriber Type Changed";
            }
            else
            {
                errMsg = "Subscriber type could not be changed.";
            }
        }

        public int GetTypeToBeChanged(int typeID) {
            if (typeID == USER_TYPE_COLLABORATOR_INDEX)
            {
                typeID = USER_TYPE_SUBSCRIBER_INDEX;
            }
            else {
                typeID = USER_TYPE_COLLABORATOR_INDEX;
            }
            return typeID;
        }
        /* *******************************************************
        * Change Subscriber Status
        * Void
        ********************************************************/
        public void ChangeSubscriberStatus(string userId, int id, out string errMsg)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroupUser subscriberStatusToBeChanged = db.SuscriberGroupUsers
                                                    .Where(s =>
                                                        s.UserID == userId &&
                                                        s.SuscriberGroupID == id
                                                    )
                                                    .FirstOrDefault();

            if (subscriberStatusToBeChanged != null)
            {
                subscriberStatusToBeChanged.ListUserStatus = GetStatusToBeChanged(subscriberStatusToBeChanged.ListUserStatus);
                db.SaveChanges();
                errMsg = "Subscriber Type Changed";
            }
            else
            {
                errMsg = "Subscriber type could not be changed.";
            }
        }
        public string GetStatusToBeChanged(string listUserStatus)
        {
            if (listUserStatus.Trim() == DEFAULT_STATUS)
            {
                listUserStatus = DISABLED_STATUS;
            }
            else
            {
                listUserStatus = DEFAULT_STATUS;
            }
            return listUserStatus;
        }
    }
}