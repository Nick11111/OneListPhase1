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
        [HiddenInput(DisplayValue = false)]
        public int ListID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CreatorID { get; set; }

        [DisplayName("List Name")]
        public string ListName { get; set; }
        [DisplayName("List Type")]
        public IEnumerable<ListType> ListType { get; set; }
        [DisplayName("Item Category")]
        public IEnumerable<ItemCategory> ItemCategory { get; set; }
        [DisplayName("Suscriber Group(hold shift to select more than one)")]
        public IEnumerable<SuscriberGroup> SuscriberGroup { get; set; }
        [DisplayName("Items")]
        public IEnumerable<Item> items { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreationDate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ListStatusID { get; set; }
    }
}