using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi201.Entity;
using webApi201.Model;

namespace webApi201.BusinessLogic
{
    public interface IEmployeeBusiness
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(Guid id);
        Task<Employee> AddEmployee(EmployeeModel employee);
        Task<Employee> UpdateEmployee(EmployeeModel employee, Employee employeeEntity);
        Task<Employee> DeleteEmployee(Employee employee);
    }
}
