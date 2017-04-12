using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneListApplication.Models
{
    public class UserRoleVM
    {
        [Required]
        [Display(Name = "user email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "role name")]
        public string RoleName { get; set; }
    }
}