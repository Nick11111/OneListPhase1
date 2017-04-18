using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.ViewModels
{
    public class SubscriberGroupUserVM
    {
 
        [Key]
        public int SubscriberGroupUserID { get; set; }

        public int SubscriberGroupID { get; set; }

        public string UserID { get; set; }

        public string Email { get; set; }

        [DisplayName("Name")]
        public string FullName { get; set; }

        public string UserTypeName { get; set; }

        [DisplayName("Subscriber Type")]
        public int UserTypeID { get; set; }

        [DisplayName("Subscriber Status")]
        public string ListUserStatus { get; set; }

        [DisplayName("Subscription Date")]
        public string SubscriptionDate { get; set; }

    }
}