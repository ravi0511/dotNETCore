using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ASPwebappEmpty.Model
{
    public class IEmployeeRepository : IEmployeeInterface
    {

        private List<Employee> _employee;
        private IConfiguration _config;

        public IEmployeeRepository(IConfiguration configuration)
        {
            _employee = new List<Employee>()
            {
                new Employee(){ ID = 1, Name = "Ravi", Department = DepartmentOptions.IT },
                new Employee(){ ID = 2, Name = "Prakas", Department = DepartmentOptions.HR },
                new Employee(){ ID = 3, Name = "Geetha", Department = DepartmentOptions.Finance }
            };
            _config = configuration;
        }
        public Employee GetEmployee(int id)
        {
            return _employee.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Employee> GetAllData()
        {
            return _employee;
        }

        public Employee AddEmployee(Employee newemployee)
        {
            Employee employee1 = new Employee();
            employee1.Name = newemployee.Name;
            employee1.Department = newemployee.Department;
            employee1.ID = _employee.Max(e => e.ID) + 1;
            _employee.Add(employee1);
            return employee1;
        }

        public string DeleteEmployee(int id)
        {
            Employee removingEmp = _employee.FirstOrDefault(e => e.ID == id);
            _employee.Remove(removingEmp);
            return _config["DeleteVerbiage"];
        }

        public Employee UpdateEmployee(Employee Changedemployee)
        {
            Employee employee1 = _employee.FirstOrDefault(e => e.ID == Changedemployee.ID);
            if(employee1 != null)
            {
                employee1.Name = Changedemployee.Name;
                employee1.Department = Changedemployee.Department;
            }
            return employee1;
        }
    }
}
