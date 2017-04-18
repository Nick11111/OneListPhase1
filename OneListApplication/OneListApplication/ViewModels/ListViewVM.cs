using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.ViewModels
{
    public class ListViewVM
    {
        [HiddenInput(DisplayValue = false)]
        public int ListID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string CreatorID { get; set; }

        [DisplayName("List Name")]
        public string ListName { get; set; }
        [DisplayName("List Type")]
        public string ListType { get; set; }
        [DisplayName("Item Category")]
        public string ItemCategory { get; set; }
        [DisplayName("Suscriber Group(s)")]
        public IEnumerable<SuscriberGroup> SuscriberGroup { get; set; }
        [DisplayName("List Role")]
        public string suscriberRole { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ListStatusID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ListTypeID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ItemCategoryID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int SuscribergroupID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int UserType { get; set; }
    }
}