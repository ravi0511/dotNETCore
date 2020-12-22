using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPwebappEmpty.Utilities;

namespace ASPwebappEmpty.ViewData
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        [EmailAddress]
        [Remote(action: "CheckEmail", controller:"Account")]
        [EmailValidation(allowedDomain:"gmail.com", ErrorMessage = "only gmail domain is allowed.")]
        public string UserName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
