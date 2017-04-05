using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneListApplication.ViewModels
{
    public class SubscriberTypeVM
    {

        [Key]
        public int UserTypeID { get; set; }

        [DisplayName("Subscriber Type")]
        public string UserTypeName { get; set; }

    }
}