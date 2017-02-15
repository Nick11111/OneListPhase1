using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OneListApplication.ViewModels
{
    public class ListVM
    {
        public int ListID { get; set; }

        public int CreatorID { get; set; }

        [DisplayName("List Name")]
        public string ListName { get; set; }

        public int ListTypeID { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        public int ListStatusID { get; set; }
    }
}