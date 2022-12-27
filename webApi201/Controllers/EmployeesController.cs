using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi201.BusinessLogic;
using webApi201.Entity;
using webApi201.Model;
using webApi201.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webApi201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeBusiness _business;
      

        public EmployeesController(IEmployeeBusiness business,ILogger<EmployeesController> logger)
        {
            _logger = logger;
            _business = business;
   
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _business.GetEmployees());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                return StatusCode(500);
            }
            
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id:Guid}",Name ="byId")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var employees = await _business.GetEmployeeById(id);
                if (employees == null) return NotFound();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
            
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeModel employee)
        {
            try
            {
                if (!ModelState.IsValid || employee == null) return BadRequest();

                var  employeeEntity = await _business.AddEmployee(employee);
                
                return CreatedAtRoute("byId", new { id = employeeEntity.Id }, employeeEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500); 
            }
            
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] EmployeeModel employee)
        {
            
            try
            {
                if (!ModelState.IsValid ||employee == null) return BadRequest();

                employee.Id = id;
                var employeeEntity = await _business.GetEmployeeById(id);
                if (employeeEntity == null) return NotFound();
                employeeEntity = await _business.UpdateEmployee(employee, employeeEntity);
                return Ok(employeeEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
            
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Employee employee = await _business.GetEmployeeById(id);
                if (employee == null) return NotFound();
                var EmployeeEntity = _business.DeleteEmployee(employee);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
            
        }
    }
}
