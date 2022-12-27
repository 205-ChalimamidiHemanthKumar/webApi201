using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi201.Entity;

namespace webApi201.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeId(Guid id);
        Task<Employee> AddEmployee(Employee employee);
        Employee DeleteEmployee(Employee employee);
        Task<int> SaveChanges();
    }
}
