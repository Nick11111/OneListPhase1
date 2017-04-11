using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Repositories
{
    public class ListRepo
    {
        public ListVM CreateList()
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            ListVM cleanList = new ListVM(); 
            cleanList.ListType = db.ListTypes.Select(s => s);
            cleanList.ItemCategory = db.ItemCategories.Select(s => s);
            cleanList.SuscriberGroup = db.SuscriberGroups.Select(s => s);
            return cleanList;
        }
    }
}