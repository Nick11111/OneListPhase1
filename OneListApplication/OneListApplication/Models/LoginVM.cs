using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneListApplication.Models
{
  
        public class LoginVM
        {   
            [Required(ErrorMessage = "Username is required")]
            //[Required(ErrorMessage = "Email is required")]
            //[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
            //ErrorMessage = "This is not a valid email address.")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

}