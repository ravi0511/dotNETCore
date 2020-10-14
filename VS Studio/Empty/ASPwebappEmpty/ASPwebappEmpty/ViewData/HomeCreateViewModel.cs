using ASPwebappEmpty.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPwebappEmpty.ViewData
{
    public class HomeCreateViewModel
    {
        //public Employee EmployeeDetail { get; set; }
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter a Value Please.")]
        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Employee Department")]
        public DepartmentOptions Department { get; set; }

        public List<IFormFile> FileName { get; set; }

        //public IFormFile FileInfo { get; set; }
    }
}
