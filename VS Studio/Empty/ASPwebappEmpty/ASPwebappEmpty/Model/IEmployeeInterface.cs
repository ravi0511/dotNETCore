using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPwebappEmpty.Model
{
    public interface IEmployeeInterface
    {
        Employee GetEmployee(int id);

        IEnumerable<Employee> GetAllData();

        Employee AddEmployee(Employee employee);

        string DeleteEmployee(int id);

        Employee UpdateEmployee(Employee employee);

    }
}
