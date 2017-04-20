using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneListApplication.ViewModels
{
    public class CouponsVM
    {
        public int CouponID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double DiscountPercentage { get; set; }
        public int RetailID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndingDate { get; set; }
        public string RetailName { get; set; }
    }
}