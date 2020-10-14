using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace ASPwebappEmpty.Model
{
    public class SQLEmployeeRepository : IEmployeeInterface
    {

        private readonly AppDbContent _appDbContent;
        private readonly IConfiguration _config;

        public SQLEmployeeRepository(AppDbContent appDbContent, IConfiguration configuration)
        {
            _appDbContent = appDbContent;
            _config = configuration;
        }
        public Employee AddEmployee(Employee employee)
        {
            _appDbContent.Employees.Add(employee);
            _appDbContent.SaveChanges();
            return employee;
        }

        public string DeleteEmployee(int id)
        {
            Employee empDetails = _appDbContent.Employees.Find(id);
            if(empDetails != null)
            {
                _appDbContent.Employees.Remove(empDetails);
                _appDbContent.SaveChanges();
                return _config["DeleteVerbiage"];
            }
            return _config["DeleteVerbiageFailed"];
        }

        public IEnumerable<Employee> GetAllData()
        {
            return _appDbContent.Employees;
        }

        public Employee GetEmployee(int id)
        {
            Employee empDetail = _appDbContent.Employees.Find(id);
            return empDetail;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            //Employee empDetail = _appDbContent.Employees.Find(employee.ID);
            //if(empDetail != null)
            //{
            //    empDetail.Name = employee.Name;
            //    empDetail.Department = employee.Department;
            //    _appDbContent.Employees.Update(empDetail);
            //    _appDbContent.SaveChanges();
            //    return empDetail;
            //}

            var empDetails = _appDbContent.Employees.Attach(employee);
            empDetails.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appDbContent.SaveChanges();
            return employee;
        }
    }
}
