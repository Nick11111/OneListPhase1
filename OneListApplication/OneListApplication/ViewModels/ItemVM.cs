using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneListApplication.ViewModels
{
    public class ItemVM
    {
        [Key]
        public int ItemID { get; set; }

        public int UserID { get; set; }

        [Required]
        [DisplayName("Item Name")]
        public string ItemName { get; set; }

        [DisplayName("Item Description")]
        public string ItemDescription { get; set; }

        [DisplayName("Item Category")]
        public int ItemCategory { get; set; }

    }
}