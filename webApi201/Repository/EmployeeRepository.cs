using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi201.Entity;

namespace webApi201.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            try
            {
                await _dbContext.Employees.AddAsync(employee);
                return employee;
            }
            catch (Exception ex)
            {

                throw (new Exception("Error occured at EmployeeRepository AddEmployee", ex));
            }
            
        }

        public Employee DeleteEmployee(Employee employee)
        {
            try
            {
                _dbContext.Employees.Remove(employee);
                return employee;
            }
            catch (Exception ex)
            {

                throw (new Exception("Error occured at EmployeeRepository DeleteEmployee", ex));
            }
             
        }

        public async Task<Employee> GetEmployeeId(Guid id)
        {
            try
            {
                return await _dbContext.Employees.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw (new Exception("Error occured at EmployeeRepository GetEmployeeById", ex));
            }
            
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                return await _dbContext.Employees.ToListAsync();
            }
            catch (Exception ex)
            {

                throw (new Exception("Error occured at EmployeeRepository GetEmployee", ex));
            }
            
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw (new Exception("Error occured at EmployeeRepository SaveChanges", ex));
            }
            
        }

    }
}
