using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPwebappEmpty.Model
{
    public class Employee
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter a Value Please.")]
        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Employee Department")]
        public DepartmentOptions Department { get; set; }

        public string FileName { get; set; }
    }
}
