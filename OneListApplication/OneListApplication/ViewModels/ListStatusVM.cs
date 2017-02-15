using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OneListApplication.ViewModels
{
    public class ListStatusVM
    {
        public int ListStatusID { get; set; }

        [DisplayName("Status")]
        public string StatusName { get; set; }
    }
}