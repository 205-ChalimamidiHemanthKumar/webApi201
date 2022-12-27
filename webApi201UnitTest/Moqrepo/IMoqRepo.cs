using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webApi201.Entity;

namespace webApi201UnitTest.Moqrepo
{
    public interface IMoqRepo
    {
        Task<IEnumerable<Employee>> GetSampleEmployee();
        Task<Employee> GetEmployee(Guid id);

        Task<Employee> AddEmployee(Employee employee);

        int GetCount();

        Task<Employee> DeleteEmployee(Employee employee);
        Task<int> SaveChanges();
    }

}
