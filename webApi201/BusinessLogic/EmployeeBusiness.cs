using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi201.Entity;
using webApi201.Model;
using webApi201.Repository;

namespace webApi201.BusinessLogic
{
    public class EmployeeBusiness:IEmployeeBusiness
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeBusiness(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Employee> AddEmployee(EmployeeModel employee)
        {
            try
            {
                    var newId = new Guid();
                    employee.Id = newId;
                    Employee employeeEntity = _mapper.Map<Employee>(employee);
                    employeeEntity = await _repository.AddEmployee(employeeEntity);
                    await _repository.SaveChanges();
                    return employeeEntity;
            }
            catch (Exception ex)
            {
                throw (new Exception("error occured at Employee business AddEmployee", ex));
            }
           
        }

        public async Task<Employee> DeleteEmployee(Employee employee)
        {
            try
            {
                _repository.DeleteEmployee(employee);
                await _repository.SaveChanges();
                return employee;
            }
            catch (Exception ex)
            {

                throw (new Exception("error occured at Employee business DeleteEmployee", ex));
            }
           
        }

        public async Task<Employee> GetEmployeeById(Guid id)
        {
            try
            {
                var employee = await _repository.GetEmployeeId(id);
                return employee;
            }
            catch (Exception ex)
            {

                throw (new Exception("error occured at Employee business GetEmployeeById", ex));
            }
            
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                return await _repository.GetEmployees();
            }
            catch (Exception ex)
            {

                throw (new Exception("error occured at Employee business GetEmployees", ex));
            }
            
        }

        public async Task<Employee> UpdateEmployee(EmployeeModel employee, Employee employeeEntity)
        {
            try
            {
                var newEmployeeEntity = _mapper.Map(employee, employeeEntity);
                await _repository.SaveChanges();
                return newEmployeeEntity;
            }
            catch (Exception ex)
            {

                throw (new Exception("error occured at Employee business UpdateEmployee", ex));
            }
            
        }
    }
}
