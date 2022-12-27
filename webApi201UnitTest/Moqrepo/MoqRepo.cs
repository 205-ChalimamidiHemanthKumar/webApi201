using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webApi201.Entity;
using System.Linq;

namespace webApi201UnitTest.Moqrepo
{
    public class MoqRepo : IMoqRepo
    {
        public async Task<Employee> GetEmployee(Guid id)
        {
            try
            {
                Employee employee = data.Where(x => x.Id == id).First();
                return employee;
            }
            catch
            {
                return null;
            }
            
            
        }

        public async Task<IEnumerable<Employee>> GetSampleEmployee()
        {
            return data;
        }

        public int GetCount()
        {
            return data.Count();
        }

        public async Task<Employee> DeleteEmployee(Employee employee)
        {
             
            try
            {
                var returnData = data.Where(x => x.Id == employee.Id).First();
                data = data.Where(x => x.Id != employee.Id);
                return returnData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> SaveChanges()
        {
            return 1;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            data = data.Append(employee);
            return employee;
        }

        public IEnumerable<Employee> data = new List<Employee>
        {
            new Employee
            {
                Id = Guid.Parse("a5f31665-a3c6-4a12-8248-cd45ff493bb8"),
                Name = "Doe",
                Designation = "Manager",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = "Karnataka"
            },
            new Employee
            {
                Id = Guid.Parse("feda54a0-8e67-4993-9ca2-ea39e3923e4d"),
                Name = "Doe",
                Designation = "Manager",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = "Karnataka"
            },
            new Employee
            {
                Id = Guid.Parse("ff77d9ce-92a2-440d-8098-9ada6225dd04"),
                Name = "Doe",
                Designation = "Manager",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = "Karnataka"
            }
        };
    }
}
