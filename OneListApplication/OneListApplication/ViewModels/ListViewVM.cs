using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [UIHint("ReadOnly")]
        public string ListName { get; set; }
        [DisplayName("List Type")]
        [ReadOnly(true)]
        public string ListType { get; set; }
        [DisplayName("Item Category")]
        [ReadOnly(true)]
        public string ItemCategory { get; set; }
        [DisplayName("Suscriber Group(s)")]
        [ReadOnly(true)]
        public IEnumerable<SuscriberGroup> SuscriberGroup { get; set; }
        [DisplayName("List Role")]
        [ReadOnly(true)]
        public string suscriberRole { get; set; }
        [DisplayName("List Tasks")]
        [ReadOnly(true)]
        public IEnumerable<ListItemVM> items { get; set; }
        [DisplayName("Creation Date")]
        [ReadOnly(true)]
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