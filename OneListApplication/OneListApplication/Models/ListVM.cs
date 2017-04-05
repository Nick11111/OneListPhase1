using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneListApplication.Models
{
    public class ListVM
    {
        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Creator ID")]
        public string CreatorID { get; set; }
        [Required]
        [Display(Name = "List Type ID")]
        public string ListTypeID { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
        [Required]
        [Display(Name = "List Status ID")]
        public string ListStatusID { get; set; }
    }
}