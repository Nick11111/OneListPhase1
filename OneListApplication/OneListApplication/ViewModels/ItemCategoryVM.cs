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
        [DisplayName("Item Category ID")]
        public int ItemCategoryID { get; set; }

        [Required]
        [DisplayName("Item Category Name")]
        public string ItemCategoryName { get; set; }

    }
}