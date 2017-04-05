using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.ViewModels
{
    public class SubscriberGroupVM
    {
        [Key]
        public int SubscriberGroupID { get; set; }

        [DisplayName("Subscriber Group Title")]
        public string SubscriberGroupName { get; set; }

        public IEnumerable<SelectListItem> UserList { get; set; }

        public string UserID { get; set; }

        public IEnumerable<SubscriberGroupUserVM> subscribedUserList { get; set; }
    }
}