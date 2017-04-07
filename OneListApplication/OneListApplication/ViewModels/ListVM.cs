using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.ViewModels
{
    public class ListVM
    {
        public int ListID { get; set; }
        public int CreatorID { get; set; }
        [DisplayName("List Name")]
        public string ListName { get; set; }
        public IEnumerable<SelectListItem> ListType { get; set; }
        public IEnumerable<SelectListItem> ItemGroup { get; set; }
        public IEnumerable<SelectListItem> SuscriberGroup { get; set; }
        public DateTime CreationDate { get; set; }
        public int ListStatusID { get; set; }
    }
}