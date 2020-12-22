using ASPwebappEmpty.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPwebappEmpty.ViewData
{
    public class UserUpdateRoleViewModel
    {

        public string UserID { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [EmailValidation(allowedDomain: "gmail.com", ErrorMessage = "only gmail domain is allowed.")]
        public string UserEmail { get; set; }

        [Required]
        [Display(Name = "City")]
        public string UserCity { get; set; }

        public IList<string> UserRoles { get; set; }

        public List<string> UserClaims { get; set; }
    }
}
