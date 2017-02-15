using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OneListApplication.ViewModels
{
    public class ListItemVM
    {
        public int ListID { get; set; }
        public int ItemID { get; set; }
        public bool ListItemSolved { get; set; }
        public int ListItemSolver { get; set; }

        [DisplayName("List Solved Date")]
        public DateTime ListItemSolvingDate { get; set; }

        [DisplayName("List Item Cost")]
        public decimal ListItemCost { get; set; }

        [DisplayName("List Notes")]
        public string ListItemNotes { get; set; }

    }
}