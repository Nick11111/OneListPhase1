using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneListApplication.Models
{
    public class RoleVM
    {
        [Required]
        [Display(Name = "User Role")]
        public string RoleName { get; set; }
    }
}