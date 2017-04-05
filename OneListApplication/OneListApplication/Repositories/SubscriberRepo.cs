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
        /* *******************************************************
        * Get all subscriber Group
        * Return: void
        ********************************************************/
        public IEnumerable<SubscriberGroupVM> GetSubscriberGroups()
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<SubscriberGroupVM> subscriberGroups = db.SuscriberGroups.Select(s => new SubscriberGroupVM() { SubscriberGroupID = s.SuscriberGroupID, SubscriberGroupName = s.SuscriberGroupName });
            return subscriberGroups;
        }
        /* *******************************************************
        * Delete subscriber Group
        * Parameter: Int subscriberGroupId
        * Return: out errMsg (string)
        ********************************************************/
        public void DeleteGroup(int subscriberGroupId, out string errMsg)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroup groupToBeUpdated = db.SuscriberGroups.Where(s => s.SuscriberGroupID == subscriberGroupId).FirstOrDefault();
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
        /* *******************************************************
        * AddUserToGroup
        * Parameter: string userID
        ********************************************************/
        public void AddUserToGroup(SubscriberGroupVM subGroup) {
            // TO DO: server side validation & client side validation
            var now = DateTime.UtcNow;
            const string DEFAULT_STATUS = "Active";
            OneListEntitiesCore db = new OneListEntitiesCore();

            SuscriberGroup sGroup = db.SuscriberGroups.Where(a => a.SuscriberGroupID == subGroup.SubscriberGroupID).FirstOrDefault();
            SuscriberGroupUser newGroupUser = new SuscriberGroupUser();
            newGroupUser.UserID = subGroup.subscribedUser.UserID;
            newGroupUser.SuscriberGroupID = subGroup.SubscriberGroupID;
            newGroupUser.UserTypeID = 2;
            newGroupUser.ListUserStatus = DEFAULT_STATUS;
            newGroupUser.SuscriptionDate = now.ToShortDateString();
            newGroupUser.SuscriberGroup = sGroup; //Add subscriberGroup property !important

            var query = db.SuscriberGroupUsers.Add(newGroupUser);

            db.SaveChanges();
        }

        /* *******************************************************
        * GetGroupDetails
        * Parameter: int GroupID
        * return: SubscriberGroupVM
        ********************************************************/
        public SubscriberGroupVM GetGroupDetails(int id) {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroup groupToBeUpdated = db.SuscriberGroups.Where(a => a.SuscriberGroupID == id).FirstOrDefault();
            SubscriberGroupVM sg = new SubscriberGroupVM();
            sg.SubscriberGroupID = groupToBeUpdated.SuscriberGroupID;
            sg.SubscriberGroupName = groupToBeUpdated.SuscriberGroupName;
            sg.UserList = GetAllUsers();

            return sg;
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
                                    Text = x.FirstName + x.LastName
                                });

            return new SelectList(categories, "Value", "Text");
        }
        /* *******************************************************
        * UpdateGroup
        * Return: bool
        ********************************************************/
        public bool UpdateGroup(SubscriberGroupVM subscriberGroup)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroup groupUpdated = db.SuscriberGroups.Where(a => a.SuscriberGroupID == subscriberGroup.SubscriberGroupID).FirstOrDefault();
            groupUpdated.SuscriberGroupName = subscriberGroup.SubscriberGroupName;

            db.SaveChanges();
            return true;
        }
        /* *******************************************************
        * GetSubscriberGroupUsers
        * Return: IEnumerable<SubscriberGroupUserVM>
        ********************************************************/
        public IEnumerable<SubscriberGroupUserVM> GetSubscriberGroupUsers(int id)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<SubscriberGroupUserVM> subscriberGroupUsers = db.SuscriberGroupUsers.Select(a => new SubscriberGroupUserVM() { SubscriberGroupID = id, UserID = a.UserID, ListUserStatus = a.ListUserStatus, UserTypeID = a.UserTypeID, SubscriptionDate = a.SuscriptionDate });
            return subscriberGroupUsers;
        }
    }
}