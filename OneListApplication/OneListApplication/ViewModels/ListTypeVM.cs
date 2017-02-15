using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OneListApplication.ViewModels
{
    public class ListTypeVM
    {
        public int ListTypeID { get; set; }

        [DisplayName("List Type Name")]
        public string TypeName { get; set; }
    }
}