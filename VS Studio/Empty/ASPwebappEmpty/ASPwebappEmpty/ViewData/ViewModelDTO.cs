using ASPwebappEmpty.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPwebappEmpty.ViewData
{
    public class ViewModelDTO
    {
        public string PageTitle { get; set; }

        public IEnumerable<Employee> empDetails { get; set; }

        public string? returnValue { get; set; }

        public string wwwpath { get; set; }
    }
}
