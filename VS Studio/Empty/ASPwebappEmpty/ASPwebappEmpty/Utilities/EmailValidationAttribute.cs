using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPwebappEmpty.Utilities
{
    public class EmailValidationAttribute: ValidationAttribute
    {
        private readonly string allowedDomain;

        public EmailValidationAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value){
            string[] emailstrings = value.ToString().Split('@').ToArray();
            return emailstrings[1].ToUpper().Trim() == allowedDomain.ToUpper().Trim();
        }

    }
}
