using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPwebappEmpty.Model
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        ID = 1,
                        Name = "Ravi",
                        Department = DepartmentOptions.Finance
                    },
                    new Employee
                    {
                        ID = 2,
                        Name = "Prakas",
                        Department = DepartmentOptions.IT
                    }
                );
        }
    }
}
