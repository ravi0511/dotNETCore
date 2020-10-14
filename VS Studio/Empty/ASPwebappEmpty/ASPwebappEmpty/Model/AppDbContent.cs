using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPwebappEmpty.Model
{
    public class AppDbContent : IdentityDbContext
    {
        public AppDbContent(DbContextOptions<AppDbContent> options): base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Employee>().HasData(
            //        new Employee
            //        {
            //            ID = 1, Name = "Ravi", Department = DepartmentOptions.Finance
            //        }
            //    );

            modelBuilder.Seed();
        }
    }
}
