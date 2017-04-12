using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneListApplication.ViewModels
{
    public class ItemCategoryVM
    {
        [Key]
        public int ItemCategoryID { get; set; }
        [DisplayName("Item Category Name")]
        public string ItemCategoryName { get; set; }

        public string UserID { get; set; }

        public IEnumerable<ItemVM> allItemsInCategory { get; set; }
    }
}